using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ExampleMod.Gores
{
    public class ExampleGore : ModGore
    {
        public override bool DrawBehind(Gore gore)
        {
            return base.DrawBehind(gore);
        }
        public override Color? GetAlpha(Gore gore, Color lightColor)
        {
            return base.GetAlpha(gore, lightColor);
        }
        public override void OnSpawn(Gore gore)
        {
            base.OnSpawn(gore);
        }
        public override bool Update(Gore gore)
        {
            return base.Update(gore);
        }
    }
}