using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TerraTracker.Common.ModTypes;
using TerraTracker.Content.TrackedStats.Combat;
using TerraTracker.Content.TrackedStats.Crafting;
using TerraTracker.Content.TrackedStats.Fishing;
using TerraTracker.Content.TrackedStats.Social;

namespace TerraTracker.Common.GlobalItems {
    /// <summary>
    /// Global Item that handles item statistic tracking.
    /// </summary>
    public class TrackerGlobalItem : GlobalItem {
        public override bool? UseItem(Item item, Player player) {
            if (Main.myPlayer != player.whoAmI) {
                return null;
            }

            if (player.ItemTimeIsZero) {
                if (item.healLife > 0) {
                    TrackedStat.AddUInt<StatHealingItemsConsumed>();
                }

                if (item.healMana > 0) {
                    TrackedStat.AddUInt<StatManaItemsConsumed>();
                }

                if (item.buffType > 0) {
                    if (ItemID.Sets.IsFood[item.type]) {
                        TrackedStat.AddUInt<StatFoodsConsumed>();
                    }

                    TrackedStat.AddUInt<StatBuffItemsConsumed>();
                }

                return true;
            }

            return null;
        }

        public override void OnCreate(Item item, ItemCreationContext context) {
            if (context is RecipeCreationContext recipeContext) {
                Item createItem = recipeContext.recipe.createItem;

                TrackedStat.AddUInt<StatCraftCount>((uint)createItem.stack);

                StatMostCrafted mostCraftedStat = ModContent.GetInstance<StatMostCrafted>();
                string craftKey = createItem.ModItem is null ? createItem.type.ToString() : createItem.ModItem.FullName;
                if (!mostCraftedStat.craftCounts.TryAdd(craftKey, (uint)createItem.stack)) {
                    mostCraftedStat.craftCounts[craftKey] += (uint)createItem.stack;
                }

                if (createItem.buffTime > 0) {
                    TrackedStat.AddUInt<StatBuffCraftCount>((uint)createItem.stack);
                }
            }
        }

        public override void OnSpawn(Item item, IEntitySource source) {
            if (item.IsACoin && source is EntitySource_Gift { Entity: NPC { type: NPCID.TaxCollector } npc } && Main.LocalPlayer.talkNPC == npc.type) {
                long value = 0;
                switch (item.type) {
                    case ItemID.PlatinumCoin:
                        value += Item.platinum;
                        break;
                    case ItemID.GoldCoin:
                        value += Item.gold;
                        break;
                    case ItemID.SilverCoin:
                        value += Item.silver;
                        break;
                    default:
                        value += Item.copper;
                        break;
                }
                value *= item.stack;

                TrackedStat.AddLong<StatTaxesCollected>(value);
            }
        }

        public override void CaughtFishStack(int type, ref int stack) {
            Item caughtItem = new(type);

            TrackedStat.AddUInt<StatThingsCaught>();

            if (ItemID.Sets.IsFishingCrate[caughtItem.type] || ItemID.Sets.IsFishingCrateHardmode[caughtItem.type]) {
                TrackedStat.AddUInt<StatCratesCaught>();
            }

            if (caughtItem.rare == ItemRarityID.Gray) {
                TrackedStat.AddUInt<StatTrashCaught>();
            }

            if (caughtItem.questItem) {
                TrackedStat.AddUInt<StatQuestFishCaught>();
            }
        }

        public override void PostReforge(Item item) {
            TrackedStat.AddUInt<StatReforges>();
        }
    }
}