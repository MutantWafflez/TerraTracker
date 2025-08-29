using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ID;
using TerraTracker.Content.UI.Elements;

namespace TerraTracker.Content.UI.TrackerPages; 

/// <summary>
///     Page that displays all crafting related data for a player.
/// </summary>
public class PlayerCraftingPage : UITrackerPage {
    public override Asset<Texture2D> PageIcon => TerraTracker.GetIcon(ItemID.WorkBench);
}