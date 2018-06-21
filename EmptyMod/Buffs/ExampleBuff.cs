using System;
using Terraria;
using Terraria.ModLoader;

namespace ExampleMod.Buffs
{
    public class ExampleBuff : ModBuff
	{
		public override bool Autoload(ref string name, ref string texture)
		{
		    base.Autoload(ref name, ref texture);
			throw new NotImplementedException("加载时的代码");
	    }
	    public override void SetDefaults()
	    {
	        DisplayName.SetDefault("Buff名");
	        Description.SetDefault("描述");
	    }
        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            base.ModifyBuffTip(ref tip, ref rare);
            throw new NotImplementedException("加载时的代码");
        }
        public override bool ReApply(Player player, int time, int buffIndex)
        {
            base.ReApply(player, time, buffIndex);
            throw new NotImplementedException("在玩家拥有Buff后再次获得");
        }
        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            base.ReApply(npc, time, buffIndex);
            throw new NotImplementedException("在NPC拥有Buff后再次获得");
        }
		public override void Update(NPC npc, ref int buffIndex)
		{
		    base.Update(npc, ref buffIndex);
            throw new NotImplementedException("影响NPC的代码");
        }
	    public override void Update(Player player, ref int buffIndex)
	    {
            base.Update(player, ref buffIndex);
	        throw new NotImplementedException("影响玩家的代码");
        }
    }
}
