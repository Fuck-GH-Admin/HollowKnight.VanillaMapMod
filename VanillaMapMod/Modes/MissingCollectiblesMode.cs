using MapChanger;
using MapChanger.MonoBehaviours;

namespace VanillaMapMod;

internal class MissingCollectiblesMode : MapMode
{
    public override string Mod => nameof(VanillaMapMod);
    public override string ModeName => "Missing Collectibles";
    public override bool ForceHasMap => true;
    public override bool ForceHasQuill => true;
    public override bool? VanillaPins => false;
    public override bool ImmediateMapUpdate => true;
    public override bool FullMap => true;

    public override bool? NextAreaNameActiveOverride(NextAreaName nextAreaName)
    {
        return true;
    }

    public override bool? NextAreaArrowActiveOverride(NextAreaArrow nextAreaArrow)
    {
        return true;
    }
}
