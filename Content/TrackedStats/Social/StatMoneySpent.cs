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

    public override void SaveData(Player player, TagCompound tag) {
        tag[FullName] = GetCurrentStat(player).longValue;
    }

    public override void LoadData(Player player, TagCompound tag) {
        GetCurrentStat(player).longValue = LoadFromTag<long>(tag);
    }

    public override string DisplayStat(Player player) => TerraTracker.DefaultCoinsRepresentation(GetCurrentStat(player).longValue);

    private bool PlayerSpentMoney(On_Player.orig_BuyItem orig, Player self, long price, int customCurrency) {
        bool returnValue = orig(self, price, customCurrency);

        if (returnValue) {
            GetCurrentStat(self).longValue += price;
        }

        return returnValue;
    }
}