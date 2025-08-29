using Terraria.ID;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Fishing; 

/// <summary>
///     Tracks how many items (regardless of what they are) have been fished up by a player.
/// </summary>
public class StatThingsCaught : TrackedStat {
    public override string ParentPage => "PlayerFishingPage";

    public override void SetStaticDefaults() {
        statIcon = TerraTracker.GetIcon(ItemID.WoodFishingPole);
    }
}