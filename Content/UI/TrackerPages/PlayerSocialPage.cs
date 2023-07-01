using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ID;
using TerraTracker.Content.UI.Elements;

namespace TerraTracker.Content.UI.TrackerPages {
    /// <summary>
    /// Page that displays data related to NPC socializing and vendor interactions.
    /// </summary>
    public class PlayerSocialPage : UITrackerPage {
        public override Asset<Texture2D> PageIcon => TerraTracker.GetIcon("Terraria/Images/NPC_Head_" + NPCHeadID.Guide);
    }
}