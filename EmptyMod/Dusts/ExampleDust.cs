using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ExampleMod.Dusts
{
    public class ExampleDust : ModDust
    {
        public override bool Autoload(ref string name, ref string texture)
        {

            throw new NotImplementedException("加载时代码");
        }
        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            throw new NotImplementedException("透明度");
        }
        public override bool MidUpdate(Dust dust)
        {
            throw new NotImplementedException("额外行动");
        }
        public override void OnSpawn(Dust dust)
        {
            throw new NotImplementedException("刷新时代码");
        }
        public override void SetDefaults()
        {
            throw new NotImplementedException("初始化");
        }
        public override bool Update(Dust dust)
        {
            throw new NotImplementedException("更新时代码");
        }
    }
}