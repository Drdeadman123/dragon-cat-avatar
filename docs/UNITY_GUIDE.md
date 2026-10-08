# Dragon-Cat Avatar — Blender to Unity to VRChat Guide

Your files (in this folder):
- **full_character.fbx** — the whole avatar: body, outfit, ears, horns, wings, tail + full armature. This is the one you upload.
- **ears.fbx / horns.fbx / wings.fbx / tail.fbx** — the four accessories on their own (each includes the armature). Spares, in case you ever want them separately.
- **avatar_parts.blend** — the Blender source scene, if you want to tweak anything.
- **textures/membrane-starry.png** — the starry wing texture (already embedded in the FBX too).

> **Everything below needs your PC** (Windows or Mac). VRChat SDK only runs in the Unity editor on a desktop — there's no phone/web way to do this part.

## What you have

A stylized anime dragon-cat, ~1.55 m tall, ~7,500 triangles (very light — VRChat allows up to 70k for "Excellent"). One armature called **AvatarRig** with standard bone names, so Unity recognizes it as a humanoid:

- Hips, Spine, Chest, Neck, Head
- Shoulder, UpperArm, LowerArm, Hand (Left and Right)
- UpperLeg, LowerLeg, Foot (Left and Right)
- Extras (Unity ignores these for the humanoid map, VRChat keeps them working): **Tail1–Tail6** (tail sway), **WingRoot/Mid/Tip** (Left/Right, for wing flapping), **Ear.L / Ear.R**.

The ears and horns are rigidly attached to the Head bone. Everything else is weight-painted and deforms.

## Step 1 — Unity setup (PC)

1. Install **Unity Hub**, then install Unity **2022.3 LTS** (the version VRChat currently uses — check the VRChat docs "SDK3" page; if it says a different 2022.3.x, match it).
2. In Unity Hub, create a new project with the **VRChat SDK3 - Avatars** template. If you don't see it: create a normal 3D project, then import the SDK from the VRChat website (Download → SDK3 Avatars `.unitypackage`, double-click to import).
3. Sign in to the SDK with your VRChat account (SDK panel → Authentication).

## Step 2 — Import the avatar (PC)

1. Drag **full_character.fbx** into your project's Assets folder.
2. Click it, and in the Inspector:
   - **Rig** tab → Animation Type: **Humanoid** → Apply. Unity should map every bone automatically (names are standard). Open "Configure" and check: all required bones green, no red entries. The extra bones (Tail, Wings, Ears) go under "not used" — that's fine.
   - **Materials** tab → keep as-is (materials are simple Principled/Standard colors; the wing membrane uses the starry texture).
3. Drag the model from Assets into your scene Hierarchy.
4. With it selected: right-click in Hierarchy → **VRChat SDK → Setup Avatar for VRChat** (or use the "VRC Avatar Descriptor" component via Add Component). This adds the descriptor.
5. In the descriptor: set **View Position** (the little white sphere — drag it between the eyes, roughly at eye level), and pick a thumbnail image if you want.
6. Optional but nice: **PhysBones** (for tail/ear jiggle). Add Component → "VRC Phys Bone" on the avatar, then drag **Tail1** (or Ear.L) into its Root Transform. Keep the default settings at first — you can tune sway later.

## Step 3 — Upload (PC)

1. Open **VRChat SDK → Show Control Panel**, go to the **Builder** tab.
2. Give it a name, check the boxes (you own the content — you commissioned it), click **Build & Publish**.
3. It'll ask for a thumbnail — take the auto screenshot or supply one.
4. Wait for the upload, then find it in VRChat under Avatars.

## If something looks wrong

- **Pink/missing materials**: select the FBX → Materials tab → "Extract Materials" and re-link, or just re-assign colors in the material slots (they're plain colors).
- **Wings look inside-out**: the membrane is double-sided; if it renders dark from one side, that's the texture — it's subtle by design.
- **Tail doesn't sway**: that's what PhysBones (step 2.6) is for — without it the tail just follows the Hips bone.
- **Arms bend weirdly in VR**: the rig was pose-tested (arms raised, crouch) and deforms cleanly; if VRChat's tracking looks off, re-check the humanoid bone mapping in the Rig → Configure panel.

## Honest notes on quality

This is a clean stylized build, not a VRoid/HoneySelect-level model: the face is simple (gold eyes, small nose/mouth, split black/white fur done as two materials), hands are mitten-style with claws, and there's no facial rig (no blinking/jaw bones). It'll read great in VRChat at normal distances. If you later want a fancier face, the **avatar_parts.blend** source is yours to edit or hand to an artist.
