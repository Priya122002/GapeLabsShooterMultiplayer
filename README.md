1) Unity Version

Unity 6000.2.12f1

2) Networking Library Used

Photon PUN 2

Lightweight and quick to set up

Ideal for small multiplayer prototypes

Room-based matchmaking without backend login

3) Unity Addressables (Async Loading)

Addressables are used to asynchronously load gameplay prefabs at runtime.

Arena floor is loaded asynchronously using Unity Addressables.

Projectile prefab is also loaded asynchronously as an Addressable via object pooling.

4) Unity Asset Bundle

Asset Bundles are used to download content from the cloud at runtime.

An Asset Bundle hosted on GitHub is downloaded at runtime.

The downloaded bundle is used to spawn a yellow cube in the scene.

5) Asset Bundle URL

https://github.com/Priya122002/GapeLabsShooterMultiplayer/raw/refs/heads/master/Assets/AssetBundles/demo_bundle

6) Optimizations Implemented

Disabled movement and input updates on remote players.

Reduced network sync frequency by avoiding per-frame updates.

Used object pooling for projectiles.

Loaded heavy assets asynchronously using Addressables.

Loaded additional content at runtime using Asset Bundles.

Applied mobile texture compression (ETC/ASTC).

Used lightweight UI and visuals for better mobile performance.

7) Error Handling Summary

Network, Addressables, and Asset Bundle failures show a simple retry popup allowing the user to retry the failed operation.

8) Setup: How to Run 2 Clients

Run one client in the Unity Editor and a second client via Build & Run or another Editor instance to test multiplayer across two windows.

9) Where AI Helped

Helped debug Asset Bundle issues where URP materials were not loading correctly on runtime-instantiated objects.

Assisted with structuring Photon multiplayer flow and optimization decisions.

Provided guidance for Addressables and async loading patterns.
