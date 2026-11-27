# VanillaMapMod — Missing Collectibles fork

[中文](./README.md) | **English**

This repository is forked from [syyePhenomenol/HollowKnight.VanillaMapMod](https://github.com/syyePhenomenol/HollowKnight.VanillaMapMod).

VanillaMapMod adds item-location pins to the Hollow Knight map and works with vanilla save files. This fork keeps the upstream behavior and adds a dedicated **Missing Collectibles** mode for completion cleanup.

## New in this fork: show only missing mask/vessel fragments

When **Missing Collectibles** mode is active, the mod:

- forces the full map to be visible;
- hides vanilla map pins;
- shows only Mask Shards and Vessel Fragments that are still missing from the current save;
- hides a pin as soon as its collectible has been obtained;
- updates from the live in-game save state;
- **never writes to or modifies the save file**.

The upstream location categories do not classify every reward as a Mask Shard or Vessel Fragment. This fork therefore adds explicit tracking for the special reward locations:

- Sly: 4 Mask Shards and 2 Vessel Fragments;
- Grubfather: Mask Shard reward;
- Seer: Vessel Fragment at 700 Essence and Mask Shard at 1500 Essence.

That gives complete coverage of all **16 Mask Shards + 9 Vessel Fragments**.

> `Customize Pins` still applies. Disable either `Mask Shards` or `Vessel Fragments` if you only want to track one category.

## Dependencies

Make sure the following mods are installed and up to date:

- [MapChanger](https://github.com/syyePhenomenol/MapChanger)
- [MagicUI](https://github.com/BadMagic100/HollowKnight.MagicUI)
- [ConnectionMetadataInjector](https://github.com/BadMagic100/ConnectionMetadataInjector)

Optional and compatible:

- [AdditionalMaps](https://github.com/SFGrenade/AdditionalMaps): adds map support for White Palace and Godhome.

## Quick start

After loading a save:

- `Ctrl + M`: enable / disable the map mod;
- `Ctrl + T`: cycle map modes;
- or use the **Mode** button in the pause menu.

There are now three modes:

1. **Normal**: adds pins to the parts of the vanilla map you have mapped;
2. **Full Map**: shows the complete map and all enabled item pins regardless of progression;
3. **Missing Collectibles**: shows the complete map but only keeps missing Mask Shards and Vessel Fragments.

### Pause menu

- **Map Mod Enabled/Disabled**: toggle the mod;
- **Mode**: cycle map modes;
- **Mod Pins**: toggle added pins;
- **Vanilla Pins**: toggle vanilla game pins;
- **Pin Shape**: change pin shape;
- **Pin Size**: change pin size;
- **Customize Pins**: toggle individual item pools;
- **Fast Map Update**: in Normal mode, update the map immediately when entering a room while owning the Quill;
- **UI Scale**: change UI text/button scale.

## Installation

Install this mod and its dependencies with a Hollow Knight Modding API compatible mod manager.

Manual installation:

1. Download a built `VanillaMapMod.zip`;
2. extract it;
3. copy the `VanillaMapMod` folder to `.../hollow_knight_Data/Managed/Mods`;
4. make sure all dependencies are installed.

## Screenshots

<details>
<summary>Click to expand</summary>

<img src="./ReadmeAssets/Screenshots/WorldMap.jpg" alt="World Map">
<img src="./ReadmeAssets/Screenshots/Pause.JPG" alt="Pause Menu">

</details>

## Implementation notes

Upstream VanillaMapMod already uses MapChanger's `Tracker.HasClearedLocation` to determine whether ordinary map pickups have been collected. This fork keeps that behavior instead of adding a separate save-file decryptor.

Special rewards are tracked from the game’s live `PlayerData`, `sceneData`, and MapChanger Tracker state. The mod does not need to decrypt or rewrite `userN.dat`.

## Upstream and license

Original project:

- [syyePhenomenol/HollowKnight.VanillaMapMod](https://github.com/syyePhenomenol/HollowKnight.VanillaMapMod)

This fork remains under the original project's license. See [LICENSE](./LICENSE).
