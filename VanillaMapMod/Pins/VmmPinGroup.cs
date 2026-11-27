using ConnectionMetadataInjector.Util;
using MapChanger;
using MapChanger.MonoBehaviours;

namespace VanillaMapMod;

internal class VmmPinGroup : MapObject
{
    internal PoolGroup PoolGroup { get; private set; }

    internal void Initialize(PoolGroup poolGroup)
    {
        base.Initialize();

        ActiveModifiers.Add(PoolSettingOn);

        PoolGroup = poolGroup;
        MapObjectUpdater.Add(this);
    }

    private bool PoolSettingOn()
    {
        if (ModeManager.CurrentMode() is MissingCollectiblesMode)
        {
            return (PoolGroup is PoolGroup.MaskShards or PoolGroup.VesselFragments)
                && VanillaMapMod.LS.GetPoolGroupSetting(PoolGroup);
        }

        return VanillaMapMod.LS.GetPoolGroupSetting(PoolGroup);
    }
}
