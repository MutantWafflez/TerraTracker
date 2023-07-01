using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraTracker {
    public class TerraTracker : Mod {
        /// <summary>
        /// Icon path to the custom stat icons for this mod.
        /// </summary>
        public const string StatIconPath = nameof(TerraTracker) + "/Assets/Sprites/StatIcons/";

        public static TerraTracker Instance {
            get;
            private set;
        }

        public TerraTracker() {
            Instance = this;
        }

        /// <summary>
        /// Short-hand for immediate loading of the specified assetPath.
        /// </summary>
        public static Asset<Texture2D> GetIcon(string assetPath) => ModContent.Request<Texture2D>(assetPath, AssetRequestMode.ImmediateLoad);

        /// <summary>
        /// Short-hand for immediate loading of a specified item id texture.
        /// </summary>
        public static Asset<Texture2D> GetIcon(int itemID) => ModContent.Request<Texture2D>("Terraria/Images/Item_" + itemID, AssetRequestMode.ImmediateLoad);

        /// <summary>
        /// Returns the default string representation of the passed in object for this mod, assuming the object an integer.
        /// </summary>
        public static string DefaultIntegerRepresentation<T>(T number)
            where T : IFormattable => number.ToString("N0", null);

        /// <summary>
        /// Returns the default string representation of the passed in object for this mod, assuming the object has fractional
        /// parts (AKA decimal values).
        /// </summary>
        public static string DefaultDecimalRepresentation<T>(T decimalNumber)
            where T : IFormattable => decimalNumber.ToString("N", null);

        /// <summary>
        /// Converts the amount of ticks into the timer representation (HH:MM:SS)
        /// </summary>
        public static string TicksToTimeString(uint ticks) => new TimeSpan(TimeSpan.TicksPerSecond * (ticks / 60)).ToString();

        /// <summary>
        /// Returns the default string representation of the coin number passed in.
        /// </summary>
        /// <param name="moneyCount"></param>
        /// <returns></returns>
        public static string DefaultCoinsRepresentation(long moneyCount) {
            int[] moneySplit = Utils.CoinsSplit(moneyCount);

            return $"{moneySplit[3]}[i:{ItemID.PlatinumCoin}]{moneySplit[2]}[i:{ItemID.GoldCoin}]{moneySplit[1]}[i:{ItemID.SilverCoin}]{moneySplit[0]}[i:{ItemID.CopperCoin}]";
        }
    }
}