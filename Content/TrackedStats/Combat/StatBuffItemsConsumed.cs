using Terraria.ID;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Combat; 

/// <summary>
///     Tracks how many buff-granting consumables a player has consumed.
/// </summary>
public class StatBuffItemsConsumed : TrackedStat {
    public override string ParentPage => "PlayerCombatPage";

    public override void SetStaticDefaults() {
        statIcon = TerraTracker.GetIcon(ItemID.IronskinPotion);
    }
}