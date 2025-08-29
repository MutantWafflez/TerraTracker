using Terraria.ID;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Fishing; 

/// <summary>
///     Tracks how many enemies a player has fished up.
/// </summary>
public class StatEnemiesCaught : TrackedStat {
    public override string ParentPage => "PlayerFishingPage";

    public override void SetStaticDefaults() {
        statIcon = TerraTracker.GetIcon(ItemID.Skull);
    }
}