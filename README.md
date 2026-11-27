# VanillaMapMod（缺失收集物增强版）

**中文（默认）** | [English](./README.en.md)

本仓库 fork 自 [syyePhenomenol/HollowKnight.VanillaMapMod](https://github.com/syyePhenomenol/HollowKnight.VanillaMapMod)。

VanillaMapMod 会在《空洞骑士》的地图上增加物品位置图钉，可用于任意原版存档。本 fork 在保留上游功能的基础上，新增了一个专门用于查漏补缺的 **Missing Collectibles（缺失收集物）** 模式。

## 本 fork 新增：只显示没拿到的血与魂容器碎片

切换到 **Missing Collectibles** 模式后：

- 自动展开完整地图；
- 隐藏游戏原版图钉；
- 只显示当前存档中**尚未获取**的面具碎片（Mask Shards）和容器碎片（Vessel Fragments）；
- 已经拿到的碎片不会显示；
- 拾取或购买后，图钉会根据当前游戏状态更新；
- **不会修改或写入存档**，只读取游戏已经加载到内存中的存档状态。

除了地图上直接拾取的碎片，本 fork 还单独处理了上游按“商店/奖励地点”分类、因此无法仅靠 Mask/Vessel 分类准确标出的特殊奖励：

- Sly：4 个面具碎片、2 个容器碎片；
- Grubfather（虫爷爷）：面具碎片奖励；
- Seer（先知）：700 Essence 的容器碎片、1500 Essence 的面具碎片。

因此 Missing Collectibles 模式覆盖完整的 **16 个面具碎片 + 9 个容器碎片**。

> `Customize Pins` 仍然有效。如果只想看其中一种，可以关闭 `Mask Shards` 或 `Vessel Fragments`。

## 依赖

请确保以下 Mod 已安装并保持更新：

- [MapChanger](https://github.com/syyePhenomenol/MapChanger)
- [MagicUI](https://github.com/BadMagic100/HollowKnight.MagicUI)
- [ConnectionMetadataInjector](https://github.com/BadMagic100/ConnectionMetadataInjector)

可选兼容：

- [AdditionalMaps](https://github.com/SFGrenade/AdditionalMaps)：为白色宫殿和神居地图提供额外地图支持。

## 界面语言

暂停菜单和 `Customize Pins` 分类支持中英文双语：当《空洞骑士》当前语言为中文（简体/繁体等 `ZH*` 语言代码）时自动显示中文；其他语言默认显示英文。无需额外安装语言文件，`language.json` 仍可作为可选自定义覆盖。

## 快速使用

进入存档后：

- `Ctrl + M`：启用 / 禁用地图 Mod；
- `Ctrl + T`：切换地图模式；
- 也可以在暂停菜单里点击 **Mode** 循环切换模式。

目前有三种模式：

1. **Normal**：在你已经探索并绘制的原版地图上增加图钉；
2. **Full Map**：无视当前探索进度，显示完整地图和所有启用的物品图钉；
3. **Missing Collectibles**：显示完整地图，但只保留当前存档尚未获取的面具碎片与容器碎片。

### 暂停菜单

- **Map Mod Enabled/Disabled**：启用 / 禁用 Mod；
- **Mode**：切换地图模式；
- **Mod Pins**：开关新增图钉；
- **Vanilla Pins**：开关游戏原版图钉；
- **Pin Shape**：切换图钉形状；
- **Pin Size**：切换图钉大小；
- **Customize Pins**：单独开关各类物品；
- **Fast Map Update**：Normal 模式下持有羽毛笔时，进入房间立即更新地图；
- **UI Scale**：调整 UI 文字和按钮缩放。

## 安装

可以使用支持 Hollow Knight Modding API 的 Mod 管理器安装依赖和本 Mod。

手动安装：

1. 下载构建好的 `VanillaMapMod.zip`；
2. 解压；
3. 将 `VanillaMapMod` 文件夹复制到 `.../hollow_knight_Data/Managed/Mods`；
4. 确保依赖 Mod 已安装。

## 截图

<details>
<summary>点击展开</summary>

<img src="./ReadmeAssets/Screenshots/WorldMap.jpg" alt="World Map">
<img src="./ReadmeAssets/Screenshots/Pause.JPG" alt="Pause Menu">

</details>

## 实现说明

上游 VanillaMapMod 已经通过 MapChanger 的 `Tracker.HasClearedLocation` 判断普通地图物品是否已经获取。本 fork 没有重新实现一套存档解密器，而是继续使用游戏运行时的 `PlayerData`、`sceneData` 与 MapChanger Tracker。

特殊奖励使用对应的游戏状态字段进行判断，因此不需要读取、解密或改写硬盘上的 `userN.dat`。

## 上游与许可证

原项目：

- [syyePhenomenol/HollowKnight.VanillaMapMod](https://github.com/syyePhenomenol/HollowKnight.VanillaMapMod)

本仓库继续遵循原项目的许可证，详见 [LICENSE](./LICENSE)。
