﻿using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TerraTracker.Common.ModTypes;

namespace TerraTracker.Content.TrackedStats.Crafting {
    /// <summary>
    /// Tracks the item that a player has crafted the most of.
    /// </summary>
    public class StatMostCrafted : TrackedStat {
        /// <summary>
        /// Localization that denoted nothing is crafted currently (so there is no max yet).
        /// </summary>
        public static ModTranslation nothingCrafted;

        public override string ParentPage => "PlayerCraftingPage";

        /// <summary>
        /// Dictionary that holds all crafting counts for any item, vanilla or modded.
        /// Vanilla is saved with its ID, and modded items are saved with their mod name
        /// and internal name.
        /// </summary>
        public Dictionary<string, uint> craftCounts;

        public override void SetStaticDefaults() {
            statIcon = TerraTracker.GetIcon(ItemID.IronHammer);
            nothingCrafted = LocalizationLoader.GetOrCreateTranslation(Mod, "Misc.NothingCrafted");
        }

        public override void InitializeExtraData() {
            craftCounts = new Dictionary<string, uint>();
        }

        public override void SaveData(TagCompound tag) {
            TagCompound dictCompound = new();
            foreach (KeyValuePair<string, uint> dictVar in craftCounts) {
                dictCompound[dictVar.Key] = dictVar.Value;
            }

            tag[FullName] = dictCompound;
        }

        public override void LoadData(TagCompound tag) {
            foreach (KeyValuePair<string, object> pair in tag.GetCompound(FullName)) {
                craftCounts[pair.Key] = (uint)Convert.ChangeType(pair.Value, typeof(uint))!;
            }
        }

        public override string DisplayStat() {
            if (!craftCounts.Any()) {
                return nothingCrafted.GetTranslation(Language.ActiveCulture);
            }

            KeyValuePair<string, uint> maxPair = craftCounts.MaxBy(pair => pair.Value);
            if (int.TryParse(maxPair.Key, out int itemID)) {
                return $"[i:{itemID}] " + "(" + maxPair.Value + ")";
            }

            return $"[i:{(ModLoader.HasMod(maxPair.Key[..maxPair.Key.IndexOf('/')]) && ModContent.TryFind(maxPair.Key, out ModItem modItem)
                ? modItem.Type
                : ModContent.Find<ModItem>("ModLoader/UnloadedItem").Type)}] "
                   + "(" + TerraTracker.DefaultIntegerRepresentation(maxPair.Value) + ")";
        }
    }
}