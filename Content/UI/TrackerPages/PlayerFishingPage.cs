using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ID;
using TerraTracker.Content.UI.Elements;

namespace TerraTracker.Content.UI.TrackerPages {
    /// <summary>
    /// Page that tracks fishing related data for a player.
    /// </summary>
    public class PlayerFishingPage : UITrackerPage {
        public override Asset<Texture2D> PageIcon => TerraTracker.GetIcon(ItemID.Bass);
    }
}