﻿using Terraria.ID;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Movement {
    /// <summary>
    /// Tracks how many times a player has jumped.
    /// </summary>
    public class StatJumps : TrackedStat {
        public override string ParentPage => "PlayerMovementPage";

        public override void SetStaticDefaults() {
            statIcon = TerraTracker.GetIcon(ItemID.FrogLeg);
        }
    }
}