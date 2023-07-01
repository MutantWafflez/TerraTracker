using Terraria.ID;
using Terraria.ModLoader.IO;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Movement {
    /// <summary>
    /// Tracks the highest speed a player has ever achieved.
    /// </summary>
    public class StatMaxSpeed : TrackedStat {
        public override string ParentPage => "PlayerMovementPage";

        public override void SetStaticDefaults() {
            statIcon = TerraTracker.GetIcon(ItemID.LightningBoots);
        }

        public override void SaveData(TagCompound tag) {
            tag[FullName] = theStat.floatStat;
        }

        public override void LoadData(TagCompound tag) {
            theStat.floatStat = LoadFromTag<float>(tag);
        }

        public override string DisplayStat() => TerraTracker.DefaultDecimalRepresentation(theStat.floatStat * 60f / 16f);
    }
}