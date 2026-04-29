# AR Elemental Survival

An augmented reality survival game built with Unity and AR Foundation. Defend yourself against enemies that grow stronger over time by switching between elemental projectiles — all played on real-world surfaces detected by your device.

---

## Prerequisites

- **Unity** 2022.3 LTS or newer
- **AR Foundation** 5.x+
- **ARCore XR Plugin** (Android) and/or **ARKit XR Plugin** (iOS)
- **XR Simulation** package (for editor testing)
- **Target Device**: ARCore-compatible Android or ARKit-compatible iOS device
- Android Build Support and/or iOS Build Support installed via Unity Hub

---

## Project Setup

1. Clone the repository and open it in Unity Hub
2. Open **Window → Package Manager** and confirm AR Foundation, ARCore/ARKit, and XR Simulation packages are installed
3. Go to **File → Build Settings**, switch to **Android** or **iOS**
4. Under **Player Settings → XR Plug-in Management**, enable:
   - ✅ ARCore (Android) / ARKit (iOS)
   - ✅ XR Simulation (Standalone — for editor testing)
5. Open the main scene and enter Play Mode or build to device

---

## Game Rules

1. **Scan** — The game detects real-world surfaces using AR plane detection. Point your device at a flat surface.
2. **Start** — Once the environment is ready, press Start.
3. **Survive** — Enemies spawn and move toward you. Shoot them with elemental projectiles.
4. **Difficulty** — Every minute, enemy speed increases.
5. **Game Over** — If an enemy reaches you, you lose a life (max three lives). When you reach zero lives, the game ends. You can restart with the same plane or rescan.

### Elemental Projectiles

Switch between three types during gameplay:

- 🔥 Fire - 1x to Fire enemies | 2x to Nature enemies | .5x to Water enemies
- 💧 Water - 1x to Water enemies | 2x to Fire Enemies | .5 to Nature enemies
- 🌿 Nature - 1x to Nature Enemies | 2x to Water enemies | .5 to Fire enemies

The selected element is visually highlighted in the UI by its corresponding color.

### Controls

- **Projectile selection** — Tap element buttons on-screen
- **Pause/Resume** — Pause button during gameplay

---

## AR Integration

- **AR Plane Manager** — Detects real-world surfaces for gameplay placement and enemy spawning.
- **AR Raycasting** — Raycasts against detected planes on screen tap to determine where projectiles are fired toward.
- **AR Camera** — The device camera serves as the player's perspective; enemies move relative to your real-world position.

---

## Testing with XR Simulation

You can test the full game loop without a physical device:

1. **Edit → Project Settings → XR Plug-in Management → Standalone** → Enable ✅ XR Simulation
2. **Window → XR → AR Foundation → XR Environment** → Select a simulated environment
3. Press **Play** in the Editor
4. Navigate with:
   - **WASD** — Move
   - **Right-click + Mouse** — Look around
   - **Q/E** — Up/Down

Simulated environments include trackable planes for testing AR raycasting and plane detection.

---

## Build & Deploy

### Android

- Minimum API Level: **24+**
- Scripting Backend: **IL2CPP**
- Target Architecture: **ARM64**
- Enable USB debugging on device → **Build and Run**

### iOS

- Minimum iOS Version: **12.0+**
- Architecture: **ARM64**
- Build → Open Xcode project → Set signing team → Deploy

---

## Troubleshooting

| Issue | Solution |
|-------|----------|
| Planes not detected | Ensure good lighting; move device slowly over surfaces |
| XR Simulation not working | Verify it's enabled under Standalone in XR Plug-in Management |
| Enemies not speeding up | Set `SpeedAmountIncreasage` in Difficulty Manager to a non-zero value in the Inspector |
| Build fails on Android | Ensure min API 24, IL2CPP, ARM64 |
