using Terraria.ID;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Combat {
    /// <summary>
    /// Tracks how many ammo items have been consumed by a player.
    /// </summary>
    public class StatAmmoConsumed : TrackedStat {
        public override string ParentPage => "PlayerCombatPage";

        public override void SetStaticDefaults() {
            statIcon = TerraTracker.GetIcon(ItemID.EndlessMusketPouch);
        }
    }
}