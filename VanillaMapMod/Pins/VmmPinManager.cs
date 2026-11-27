using System;
using System.Collections.Generic;
using System.Linq;
using ConnectionMetadataInjector.Util;
using MapChanger;
using MapChanger.Defs;
using MapChanger.MonoBehaviours;
using UnityEngine;
using VanillaMapMod.Pins;

namespace VanillaMapMod;

internal static class VmmPinManager
{
    private const float OFFSETZ_BASE = -1.4f;
    private const float OFFSETZ_RANGE = 0.4f;

    internal static Dictionary<string, VmmPin> Pins { get; private set; } = [];

    internal static Dictionary<PoolGroup, VmmPinGroup> PinGroups { get; private set; } = [];

    internal static void MakePins(GameObject goMap)
    {
        VanillaMapMod.Instance.LogDebug("Make Pins");

        Pins = [];
        PinGroups = [];

        foreach (
            var poolGroup in Enum.GetValues(typeof(PoolGroup))
                .Cast<PoolGroup>()
                .Where(poolGroup => poolGroup is not PoolGroup.Other)
        )
        {
            var pinGroup = Utils.MakeMonoBehaviour<VmmPinGroup>(goMap, poolGroup.FriendlyName());
            pinGroup.Initialize(poolGroup);

            PinGroups[poolGroup] = pinGroup;
        }

        var vanillaLocations = Finder.GetAllVanillaLocations();

        foreach (var mld in vanillaLocations.Values)
        {
            var pin = Utils.MakeMonoBehaviour<VmmPin>(goMap, mld.Name);
            pin.Initialize(mld, PinGroups[SubcategoryFinder.GetLocationPoolGroup(mld.Name)]);

            Pins[mld.Name] = pin;
        }

        MakeMissingCollectiblePins(goMap, vanillaLocations);

        IEnumerable<MapObject> pinsSorted = Pins
            .Values.OrderBy(mapObj => mapObj.transform.position.x)
            .ThenBy(mapObj => mapObj.transform.position.y);

        for (var i = 0; i < pinsSorted.Count(); i++)
        {
            var transform = pinsSorted.ElementAt(i).transform;
            transform.localPosition = new(
                transform.localPosition.x,
                transform.localPosition.y,
                OFFSETZ_BASE + ((float)i / Pins.Count() * OFFSETZ_RANGE)
            );
        }
    }

    internal static string GetPoolGroupCounter(PoolGroup poolGroup)
    {
        string text;

        IEnumerable<VmmPin> pins = PinGroups[poolGroup].Children.OfType<VmmPin>();

        if (ModeManager.CurrentMode() is not MissingCollectiblesMode)
        {
            pins = pins.Where(pin => !pin.MissingCollectiblesOnly);
        }

        var pinList = pins.ToList();

        if (IsPersistent(poolGroup))
        {
            text = "";
        }
        else
        {
            text = pinList.Count(pin => pin.IsCleared()).ToString() + " / ";
        }

        return text + pinList.Count.ToString();
    }

    private static void MakeMissingCollectiblePins(
        GameObject goMap,
        Dictionary<string, MapLocationDef> vanillaLocations
    )
    {
        AddMissingCollectiblePin(
            goMap,
            vanillaLocations,
            "Sly",
            "Missing_Mask_Shard-Sly_1",
            PoolGroup.MaskShards,
            () => !PlayerData.instance.GetBool("slyShellFrag1"),
            -0.45f,
            -0.35f
        );
        AddMissingCollectiblePin(
            goMap,
            vanillaLocations,
            "Sly",
            "Missing_Mask_Shard-Sly_2",
            PoolGroup.MaskShards,
            () => !PlayerData.instance.GetBool("slyShellFrag2"),
            0.45f,
            -0.35f
        );
        AddMissingCollectiblePin(
            goMap,
            vanillaLocations,
            "Sly",
            "Missing_Vessel_Fragment-Sly_1",
            PoolGroup.VesselFragments,
            () => !PlayerData.instance.GetBool("slyVesselFrag1"),
            0f,
            -0.75f
        );

        AddMissingCollectiblePin(
            goMap,
            vanillaLocations,
            "Sly_(Key)",
            "Missing_Mask_Shard-Sly_3",
            PoolGroup.MaskShards,
            () => !PlayerData.instance.GetBool("slyShellFrag3"),
            -0.45f,
            0.35f
        );
        AddMissingCollectiblePin(
            goMap,
            vanillaLocations,
            "Sly_(Key)",
            "Missing_Mask_Shard-Sly_4",
            PoolGroup.MaskShards,
            () => !PlayerData.instance.GetBool("slyShellFrag4"),
            0.45f,
            0.35f
        );
        AddMissingCollectiblePin(
            goMap,
            vanillaLocations,
            "Sly_(Key)",
            "Missing_Vessel_Fragment-Sly_2",
            PoolGroup.VesselFragments,
            () => !PlayerData.instance.GetBool("slyVesselFrag2"),
            0f,
            0.75f
        );

        AddMissingCollectiblePin(
            goMap,
            vanillaLocations,
            "Grubfather",
            "Missing_Mask_Shard-Grubfather",
            PoolGroup.MaskShards,
            () => !Tracker.HasSceneData("Heart Piece", "Crossroads_38")
        );

        AddMissingCollectiblePin(
            goMap,
            vanillaLocations,
            "Seer",
            "Missing_Vessel_Fragment-Seer",
            PoolGroup.VesselFragments,
            () => !PlayerData.instance.GetBool("dreamReward5"),
            -0.35f,
            0f
        );
        AddMissingCollectiblePin(
            goMap,
            vanillaLocations,
            "Seer",
            "Missing_Mask_Shard-Seer",
            PoolGroup.MaskShards,
            () => !PlayerData.instance.GetBool("dreamReward7"),
            0.35f,
            0f
        );
    }

    private static void AddMissingCollectiblePin(
        GameObject goMap,
        Dictionary<string, MapLocationDef> vanillaLocations,
        string sourceLocation,
        string pinName,
        PoolGroup poolGroup,
        Func<bool> isMissing,
        float offsetX = 0f,
        float offsetY = 0f
    )
    {
        if (!vanillaLocations.TryGetValue(sourceLocation, out var source))
        {
            VanillaMapMod.Instance.LogWarn($"Missing source location for collectible pin: {sourceLocation}");
            return;
        }

        var mld = source with
        {
            Name = pinName,
            MapLocations = source
                .MapLocations.Select(location =>
                    location with
                    {
                        X = location.X + offsetX,
                        Y = location.Y + offsetY,
                    }
                )
                .ToArray(),
        };

        var pin = Utils.MakeMonoBehaviour<VmmPin>(goMap, pinName);
        pin.Initialize(mld, PinGroups[poolGroup], isMissing, missingCollectiblesOnly: true);

        Pins[pinName] = pin;
    }

    private static bool IsPersistent(PoolGroup poolGroup)
    {
        return poolGroup is PoolGroup.LifebloodCocoons or PoolGroup.SoulTotems or PoolGroup.LoreTablets;
    }
}
