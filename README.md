# Aetherwing VR
Aetherwing VR is a serene, anime-style VR experience where players embody a mythical creature with dragon-like wings and explore a magical world of floating islands, glowing skies, and enchanted nature. Accompanied by a cute flying dragon companion, they soar through a surreal, Genshin Impact-inspired landscape filled with warmth, wonder, and freedom. The game offers an emotionally uplifting journey through flight, connection, and visual beauty.

## Features

**Core features:**

- Gesture-Based Flying: Players start flying by opening their arms (angle + distance).
- Smooth Gliding & Directional Flight: Forward flight aligned with head direction using Rigidbody + Lerp.
- Dynamic Animation Transitions:  Idle ↔ Walking ↔ Flying states based on player movement and ground check.
- First-Person Full-Body View: Character aligned with XR Rig to see your own body from the inside. [Responsive camera matching 1:1 head movement.]
- Post-Processing VFX: Global volume activates while flying for immersive visual feedback.
- Ocean Respawn System: When falling into the ocean, the player respawns at a random island spawn point.
- Island Collision & Ground Check:Box Colliders used on islands to prevent falling through.
- Background Music: Ambient soundtrack starts with the game and plays continuously.
- Genshin Impact-inspired environment: floating islands, magical skies designed using Blender.

**Stretch goals:**

- Flying AI Companion: a cute dragon that follows the player in the sky
- Different flight modes (e.g., gliding, flapping wings)
- Ambient music and sound effects for immersive atmosphere
- Optional experimental hand-tracking control
- Light environmental interactivity (e.g., wind gusts)
- Climbing mechanic (similar to *Rush-*style climbing sections)

## Tech Stack

- Unity 6.0.0 LTS
- Meta Quest 3 SDK
- Meta XR All-in-One SDK
- XR Interaction Toolkit (compatible version)
- Vignette screen effect package (Unity Post-Processing )

## Resources/Inspiration (optional)

- [Rush - Meta Store](https://www.meta.com/en-gb/experiences/rush/1810693125705825)
- [Zenith MMO](https://zenithmmo.com/)
- [Genshin Impact's](https://www.youtube.com/watch?v=ybVC2UnlU9M): Environmental style
- [*Split Fiction - Kitten Side Story](https://www.youtube.com/watch?v=unATxBJzexg) :*  for flight flow
- [Population: One:](https://www.meta.com/en-gb/experiences/population-one/2564158073609422/?srsltid=AfmBOorz7FYRi3cBzJXvCCZpF0Hlst8xHQyAynlCKz280z8t3Ho11bzo) flying mechanic reference
