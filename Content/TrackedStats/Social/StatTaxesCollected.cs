using Terraria.ID;
using Terraria.ModLoader.IO;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Social {
    /// <summary>
    /// Tracks how much money a player has collected from the tax collector.
    /// </summary>
    public class StatTaxesCollected : TrackedStat {
        public override string ParentPage => "PlayerSocialPage";

        public override void SetStaticDefaults() {
            statIcon = TerraTracker.GetIcon("Terraria/Images/NPC_Head_" + NPCHeadID.TaxCollector);
        }

        public override void SaveData(TagCompound tag) {
            tag[FullName] = theStat.longStat;
        }

        public override void LoadData(TagCompound tag) {
            theStat.longStat = LoadFromTag<long>(tag);
        }

        public override string DisplayStat() => TerraTracker.DefaultCoinsRepresentation(theStat.longStat);
    }
}