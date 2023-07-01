using Terraria.ID;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Combat {
    /// <summary>
    /// Tracks how much damage the player has blocked due to their defense.
    /// </summary>
    public class StatDamageBlocked : TrackedStat {
        public override string ParentPage => "PlayerCombatPage";

        /// <summary>
        /// How much damage the player has received before defense is accounted for.
        /// </summary>
        /// <remarks>
        /// Subtracted from the damage actually taken to determined the damage blocked.
        /// </remarks>
        public uint preDefenseDamage;

        public override void SetStaticDefaults() {
            statIcon = TerraTracker.GetIcon(ItemID.CobaltShield);
        }
    }
}