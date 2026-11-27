using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using MapChanger;

namespace VanillaMapMod;

internal static class Localization
{
    private static readonly Dictionary<string, string> _zh = new()
    {
        ["Mod\nEnabled"] = "模组\n已启用",
        ["Mod Pins:\n"] = "模组图钉：\n",
        ["Vanilla Pins"] = "原版图钉",
        ["Customize\nPins"] = "自定义\n图钉",
        ["Mode"] = "模式",
        ["Normal"] = "普通",
        ["Full Map"] = "完整地图",
        ["Missing Collectibles"] = "缺失收集物",
        ["Pin Shape"] = "图钉形状",
        ["Pin Size"] = "图钉大小",
        ["Fast Map\nUpdate"] = "快速地图\n更新",
        ["UI Scale"] = "界面缩放",
        ["On"] = "开",
        ["Off"] = "关",
        ["Custom"] = "自定义",

        ["Tiny"] = "极小",
        ["Small"] = "小",
        ["Medium"] = "中",
        ["Large"] = "大",
        ["Huge"] = "极大",

        ["Circle"] = "圆形",
        ["Diamond"] = "菱形",
        ["Square"] = "方形",
        ["Pentagon"] = "五边形",
        ["Hexagon"] = "六边形",
        ["No Border"] = "无边框",

        ["Dreamers"] = "梦之守护者",
        ["Skills"] = "能力",
        ["Charms"] = "护符",
        ["Keys"] = "钥匙",
        ["Mask Shards"] = "面具碎片",
        ["Vessel Fragments"] = "容器碎片",
        ["Charm Notches"] = "护符槽",
        ["Pale Ore"] = "苍白矿石",
        ["Geo Chests"] = "吉欧宝箱",
        ["Rancid Eggs"] = "腐臭蛋",
        ["Relics"] = "遗物",
        ["Whispering Roots"] = "低语之根",
        ["Boss Essence"] = "Boss 精华",
        ["Grubs"] = "幼虫",
        ["Mimics"] = "拟态幼虫",
        ["Maps"] = "地图",
        ["Stags"] = "鹿角虫站",
        ["Lifeblood Cocoons"] = "生命血茧",
        ["Grimmkin Flames"] = "格林之火",
        ["Journal Entries"] = "猎人日志",
        ["Geo Rocks"] = "吉欧矿",
        ["Boss Geo"] = "Boss 吉欧",
        ["Soul Totems"] = "灵魂图腾",
        ["Lore Tablets"] = "石碑",
        ["Shops"] = "商店",
    };

    private static Dictionary<string, string> _externalLocalization;

    internal static void Load()
    {
        try
        {
            _externalLocalization = JsonUtil.DeserializeFromExternalFile<Dictionary<string, string>>(
                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "language.json")
            );
        }
        catch (Exception)
        {
            VanillaMapMod.Instance.LogDebug("No valid \"language.json\" file found; using built-in localization.");
        }

        // Always register our localizer. The built-in Chinese translation must work
        // even when no external language.json is installed.
        MapChanger.Localization.AddLocalizer(Localize);
    }

    internal static string Localize(string text)
    {
        if (_externalLocalization is not null && _externalLocalization.TryGetValue(text, out var externalResult))
        {
            return externalResult;
        }

        return IsChinese() && _zh.TryGetValue(text, out var result) ? result : null;
    }

    private static bool IsChinese()
    {
        try
        {
            var languageCode = global::Language.Language.CurrentLanguage().ToString();
            return languageCode.Equals("ZH", StringComparison.OrdinalIgnoreCase)
                || languageCode.StartsWith("ZH_", StringComparison.OrdinalIgnoreCase);
        }
        catch (Exception)
        {
            return false;
        }
    }
}
