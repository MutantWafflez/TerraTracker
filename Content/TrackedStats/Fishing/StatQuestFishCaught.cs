using Terraria.ID;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Fishing; 

/// <summary>
///     Tracks how many quest fish a player has fished up.
/// </summary>
internal class StatQuestFishCaught : TrackedStat {
    public override string ParentPage => "PlayerFishingPage";

    public override void SetStaticDefaults() {
        statIcon = TerraTracker.GetIcon(ItemID.Dirtfish);
    }
}