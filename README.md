# dragon-cat-avatar

Corbyn's dragon-cat hybrid VR avatar character — built for VRChat.

## What's here

- `models/full_character.fbx` — the whole avatar: body, outfit, ears, horns, wings, tail + full 33-bone armature. This is the file you upload to VRChat.
- `models/parts/` — ears, horns, wings, tail as separate FBX files (spares).
- `blender/avatar_parts.blend` — the Blender source scene, for tweaks.
- `textures/membrane-starry.png` — starry wing membrane texture (also embedded in the FBX).
- `art/` — design reference art.
- `docs/UNITY_GUIDE.md` — step-by-step: Unity setup, import, VRChat SDK upload.

## Specs

- Stylized anime humanoid, ~1.55 m tall, ~7,500 triangles
- Split black/white facial fur, gold eyes, navy/white outfit
- Cat ears, ridged dragon horns, starry-membrane wings (3-bone chains), 6-bone tail chain
- 33-bone armature with Unity humanoid naming (Hips, Spine, Chest, Neck, Head, arms, legs + Tail1-6, WingRoot/Mid/Tip L/R, Ear.L/R)
- Pose-tested: raised arms, crouch, tail sway, wing flap
- Face is simple/stylized — no facial rig (no blinking/jaw bones)

## Quick start

1. Install the VRChat Creator Companion and let it install Unity 2022.3.22f1 (the exact version VRChat requires)
2. Import `models/full_character.fbx`, set Rig to Humanoid
3. Add the VRC Avatar Descriptor, set the view position
4. Build & Publish from the VRChat SDK control panel
