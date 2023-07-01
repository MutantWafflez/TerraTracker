using Terraria.ModLoader.IO;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Social {
    /// <summary>
    /// Tracks how much money a player has earned by selling items to NPCs.
    /// </summary>
    public class StatMoneySold : TrackedStat {
        public override string ParentPage => "PlayerSocialPage";

        public override void SetStaticDefaults() {
            statIcon = TerraTracker.GetIcon(TerraTracker.StatIconPath + "MoneySold");
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