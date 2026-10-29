# Don't Turn Around! — Classroom Art Kit (v1)

Flat cartoon placeholder art for the v1 demo: a fixed 2D front view from the player's seat,
designed for a **1280 × 720** screen.

```
png/1x/         16 sprites at design size (1 px = 1 design px)
png/2x/         the same sprites at double resolution, for sharp text on HD screens
preview/        preview_sheet.png + the six teacher poses as rendered in Unity
credits.txt     source and licence
```

The 1x and 2x files have the same names and contents; only the resolution differs.

## Layers (back to front)

Coordinates are in 1280 × 720 design pixels, origin at the **top-left**, y pointing down.
"Top-left" is where the image's top-left corner goes; "anchor" is the point used as the sprite pivot.

| Order | File | 1x size | Top-left | Anchor in image → screen point | Notes |
|---|---|---|---|---|---|
| 0 | classroom_background.png | 1280×720 | (0, 0) | centre → (640, 360) | Wall, blank blackboard, clock, window, corkboard |
| 1 | *(engine text)* | — | see below | — | Chalk writing on the board |
| 10 | teacher_*.png (6 poses) | 240×370 | (540, 96) | feet (120, 362) → (660, 458) | Show one pose at a time |
| 11 | squeak_marker.png | 160×80 | (690, 100) | centre → (770, 140) | Rotate 8° (tilts up to the right). Real turn only |
| 20 | classmates_row.png | 1280×200 | (0, 360) | centre → (640, 460) | Row of 8 classmates, gap in the middle |
| 30 | player_desk.png | 1280×215 | (0, 505) | centre → (640, 612.5) | Desk edge at y ≈ 520; includes the pencil |
| 31 | textbook.png | 290×160 | (110, 535) | centre → (255, 615) | Separate so it can be raised for READ! later |
| 40 | chips_open.png | 150×196 | (565, 474) | centre → (640, 572) | v1 bag (already open) |
| 40 | chips_closed.png | 150×196 | (565, 474) | centre → (640, 572) | v2 bag (unwrap with E) |
| 42 | crumbs.png | 100×50 | (590, 475) | centre → (640, 500) | Flash briefly on each bite |
| 50 | hand_left.png | 150×150 | (473, 580) | centre → (548, 655) | |
| 50 | hand_right.png | 150×150 | (657, 580) | centre → (732, 655) | |

**Hiding the snack:** slide the bag straight down until its centre reaches y = 820 (below the
desk edge, off screen) in 0.15 s, then slide it back the same way.

**Board text:** plain sans-serif in chalk colour `#F5F2E8` at 92% opacity, left-aligned:

| Text | Top-left | Line height |
|---|---|---|
| Lesson 3: Fractions | (360, 112) | 30 |
| 1/2 + 1/4 = 3/4 | (368, 170) | 26 |
| 2/3 - 1/6 = ? | (368, 220) | 26 |
| HW: p.42 | (760, 262) | 24 |

## Teacher poses

| File | When to show it |
|---|---|
| teacher_writing_a.png / teacher_writing_b.png | Writing on the board. Alternate every 0.35 s for the writing animation |
| teacher_fake_tell.png | Fake turn: scratches her head. No squeak |
| teacher_turning.png | Real turn, together with squeak_marker.png and the shoe sound |
| teacher_facing_squint.png | Facing the class during the 0.3 s grace window |
| teacher_facing.png | Facing the class after the grace window |

## Using it in Unity

- **Whole scene:** import `Classroom_Scene.unitypackage`, which comes with this kit, and open
  `Assets/Scenes/Classroom.unity`. Requires Unity 6000.3 with URP and the 2D Renderer.
  It includes `TeacherPoseView` (call `SetPose(...)`) and the builder that regenerates the art
  (menu **Tools → Don't Turn Around → Build Classroom Scene**).
- **PNGs only:** set Texture Type to *Sprite (2D and UI)*, Pixels Per Unit **100** for `1x`
  or **200** for `2x`, then set the pivot to the anchor in the table above. An orthographic
  camera with size **3.6** then shows exactly 1280 × 720 design pixels.

## Changing the art

Every file can be replaced with real art of the same name and size; the layout above stays the
same. Recolouring is easiest by regenerating: edit the colour constants at the top of
`ClassroomSceneBuilder.cs` and run the menu item again.
