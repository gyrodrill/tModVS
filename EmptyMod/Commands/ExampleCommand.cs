using System;
using Terraria.ModLoader;

namespace ExampleMod.Commands
{
    public class ExampleCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;
	    public override string Command => throw new NotImplementedException("命令名");
	    public override string Usage => throw new NotImplementedException("用法");
	    public override string Description => throw new NotImplementedException("介绍");
	    public override void Action(CommandCaller caller, string input, string[] args)
	    {
		    throw new NotImplementedException("实际代码");
        }
        public override bool Autoload(ref string name)
        {
            base.Autoload(ref name);
            throw new NotImplementedException("加载时代码");
        }
    }
}