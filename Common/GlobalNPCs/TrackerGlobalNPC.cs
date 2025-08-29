using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TerraTracker.Common.ModTypes;
using TerraTracker.Content.TrackedStats.Fishing;
using TerraTracker.Content.TrackedStats.Social;

namespace TerraTracker.Common.GlobalNPCs; 

/// <summary>
///     GlobalNPC that handles npc-related data tracking.
/// </summary>
public class TrackerGlobalNPC : GlobalNPC {
    public override void OnSpawn(NPC npc, IEntitySource source) {
        // Hard-code since Duke doesn't use the FishedOut entity source
        if (
            (npc.type != NPCID.DukeFishron || source is not EntitySource_BossSpawn { Target: Player dukePlayer } || dukePlayer.whoAmI != Main.myPlayer)
            && (source is not EntitySource_FishedOut { Fisher: Player fishPlayer } || fishPlayer.whoAmI != Main.myPlayer)
        ) {
            return;
        }

        TrackedStat.AddUInt<StatEnemiesCaught>();
        TrackedStat.AddUInt<StatThingsCaught>();
    }

    public override void GetChat(NPC npc, ref string chat) {
        // Method is only called when first chatting with an NPC, thus:
        TrackedStat.AddUInt<StatNPCChats>();
    }
}