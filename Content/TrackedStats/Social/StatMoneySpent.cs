using Terraria;
using Terraria.ModLoader.IO;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Social;

/// <summary>
///     Tracks how much money a player has spent.
/// </summary>
public class StatMoneySpent : TrackedStat {
    public override string ParentPage => "PlayerSocialPage";

    public override void Load() {
        base.Load();

        On_Player.BuyItem += PlayerSpentMoney;
    }

    public override void SetStaticDefaults() {
        statIcon = TerraTracker.GetIcon(TerraTracker.StatIconPath + "MoneySpent");
    }

    public override void SaveData(TagCompound tag) {
        tag[FullName] = theStat.longStat;
    }

    public override void LoadData(TagCompound tag) {
        theStat.longStat = LoadFromTag<long>(tag);
    }

    public override string DisplayStat() => TerraTracker.DefaultCoinsRepresentation(theStat.longStat);

    private bool PlayerSpentMoney(On_Player.orig_BuyItem orig, Player self, long price, int customCurrency) {
        bool returnValue = orig(self, price, customCurrency);

        if (returnValue) {
            theStat.longStat += price;
        }

        return returnValue;
    }
}