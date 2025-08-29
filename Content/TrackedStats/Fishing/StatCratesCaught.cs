using Terraria.ID;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Fishing; 

/// <summary>
///     Tracks how many crates a player has fished up.
/// </summary>
public class StatCratesCaught : TrackedStat {
    public override string ParentPage => "PlayerFishingPage";

    public override void SetStaticDefaults() {
        statIcon = TerraTracker.GetIcon(ItemID.WoodenCrate);
    }
}