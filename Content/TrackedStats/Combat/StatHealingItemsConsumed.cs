using Terraria.ID;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Combat; 

/// <summary>
///     Tracks how many health-refilling consumables a player has consumed.
/// </summary>
public class StatHealingItemsConsumed : TrackedStat {
    public override string ParentPage => "PlayerCombatPage";

    public override void SetStaticDefaults() {
        statIcon = TerraTracker.GetIcon(ItemID.SuperHealingPotion);
    }
}