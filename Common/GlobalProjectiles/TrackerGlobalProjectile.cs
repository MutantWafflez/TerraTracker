using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using TerraTracker.Common.ModTypes;
using TerraTracker.Content.TrackedStats.Combat;

namespace TerraTracker.Common.GlobalProjectiles {
    /// <summary>
    /// GlobalProjectile that handles projectile related tracking.
    /// </summary>
    public class TrackerGlobalProjectile : GlobalProjectile {
        public override void OnSpawn(Projectile projectile, IEntitySource source) {
            if (source is EntitySource_ItemUse { Entity: Player player } && player.whoAmI == Main.myPlayer) {
                TrackedStat.AddUInt<StatProjsFired>();
            }
        }
    }
}