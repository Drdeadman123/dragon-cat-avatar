#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Dynamics.PhysBone.Components;

/// <summary>
/// Import and scene setup helpers for the Dragon-Cat avatar.
/// Place this file under Assets/Editor in a Unity 2022.3 project with VRChat SDK3 installed.
/// </summary>
public sealed class DragonCatAvatarSetup : AssetPostprocessor
{
    private const string AvatarModelPath = "models/full_character.fbx";
    private const string AvatarMenuPath = "Tools/Dragon-Cat Avatar/Setup Selected Avatar";

    private void OnPreprocessModel()
    {
        if (!IsAvatarModel(assetPath)) return;

        var importer = (ModelImporter)assetImporter;
        importer.animationType = ModelImporterAnimationType.Human;
        importer.avatarSetup = ModelImporterAvatarSetup.CreateFromThisModel;
        importer.materialImportMode = ModelImporterMaterialImportMode.ImportStandard;
        importer.materialLocation = ModelImporterMaterialLocation.InPrefab;
    }

    private static bool IsAvatarModel(string path)
    {
        return path.Replace('\\', '/').EndsWith("/" + AvatarModelPath, StringComparison.OrdinalIgnoreCase)
            || string.Equals(path.Replace('\\', '/'), AvatarModelPath, StringComparison.OrdinalIgnoreCase);
    }

    [MenuItem(AvatarMenuPath, true)]
    private static bool ValidateSetupSelectedAvatar()
    {
        return Selection.activeGameObject != null;
    }

    [MenuItem(AvatarMenuPath)]
    private static void SetupSelectedAvatar()
    {
        var root = Selection.activeGameObject;
        if (root == null)
        {
            EditorUtility.DisplayDialog("Dragon-Cat Avatar", "Select the imported avatar root in the Hierarchy first.", "OK");
            return;
        }

        Undo.RegisterFullObjectHierarchyUndo(root, "Set up Dragon-Cat VRChat avatar");
        var descriptor = root.GetComponent<VRCAvatarDescriptor>();
        if (descriptor == null) descriptor = Undo.AddComponent<VRCAvatarDescriptor>(root);

        var head = FindTransform(root.transform, "Head");
        if (head != null)
        {
            // Approximate eye-level view point from the documented Head bone. Adjust in the
            // Avatar Descriptor if this particular model's imported axes place the face differently.
            descriptor.ViewPosition = root.transform.InverseTransformPoint(
                head.TransformPoint(new Vector3(0f, 0.075f, 0.08f)));
        }
        else
        {
            Debug.LogWarning("Dragon-Cat setup: couldn't find a Head bone; set the Avatar Descriptor view position manually.", root);
        }

        AddPhysBone(root, "Tail1");
        AddPhysBone(root, "Ear.L");
        AddPhysBone(root, "Ear.R");
        EditorUtility.SetDirty(descriptor);
        Debug.Log("Dragon-Cat VRChat setup complete. Review the view position and PhysBone settings before upload.", root);
    }

    private static void AddPhysBone(GameObject avatarRoot, string boneName)
    {
        var bone = FindTransform(avatarRoot.transform, boneName);
        if (bone == null)
        {
            Debug.LogWarning("Dragon-Cat setup: couldn't find bone '" + boneName + "'; skipped its PhysBone.", avatarRoot);
            return;
        }

        var physBone = bone.GetComponent<VRCPhysBone>();
        if (physBone == null) physBone = Undo.AddComponent<VRCPhysBone>(bone.gameObject);
        physBone.rootTransform = bone;
        EditorUtility.SetDirty(physBone);
    }

    private static Transform FindTransform(Transform parent, string targetName)
    {
        if (parent.name == targetName) return parent;
        for (var i = 0; i < parent.childCount; i++)
        {
            var found = FindTransform(parent.GetChild(i), targetName);
            if (found != null) return found;
        }
        return null;
    }
}
#endif
