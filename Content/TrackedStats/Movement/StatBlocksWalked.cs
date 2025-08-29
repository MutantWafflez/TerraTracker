using Terraria.ID;
using Terraria.ModLoader.IO;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Movement; 

/// <summary>
///     Tracks how many blocks a player has moved while touching the ground.
/// </summary>
public class StatBlocksWalked : TrackedStat {
    public override string ParentPage => "PlayerMovementPage";

    public override void SetStaticDefaults() {
        statIcon = TerraTracker.GetIcon(ItemID.HermesBoots);
    }

    public override void SaveData(TagCompound tag) {
        tag[FullName] = theStat.floatStat;
    }

    public override void LoadData(TagCompound tag) {
        theStat.floatStat = LoadFromTag<float>(tag);
    }

    public override string DisplayStat() => TerraTracker.DefaultDecimalRepresentation(theStat.floatStat);
}