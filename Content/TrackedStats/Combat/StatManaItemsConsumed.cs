using Terraria.ID;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Combat {
    /// <summary>
    /// Tracks how many mana-refilling consumables a player has consumed.
    /// </summary>
    public class StatManaItemsConsumed : TrackedStat {
        public override string ParentPage => "PlayerCombatPage";

        public override void SetStaticDefaults() {
            statIcon = TerraTracker.GetIcon(ItemID.SuperManaPotion);
        }
    }
}