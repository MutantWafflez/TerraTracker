using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using TerraTracker.Content.UI.Elements;

namespace TerraTracker.Content.UI.TrackerPages {
    /// <summary>
    /// Page that displays all combat related data for a player.
    /// </summary>
    public class PlayerCombatPage : UITrackerPage {
        public override Asset<Texture2D> PageIcon => TerraTracker.GetIcon("Terraria/Images/Extra_11");
    }
}