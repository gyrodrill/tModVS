using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace ExampleMod.Walls
{
    public class ExampleWall : ModWall
    {
        public override void AnimateWall(ref byte frame, ref byte frameCounter)
        {
            base.AnimateWall(ref frame, ref frameCounter);
        }
        public override bool Autoload(ref string name, ref string texture)
        {
            return base.Autoload(ref name, ref texture);
        }
        public override bool CanExplode(int i, int j)
        {
            return base.CanExplode(i, j);
        }
        public override bool CreateDust(int i, int j, ref int type)
        {
            return base.CreateDust(i, j, ref type);
        }
        public override bool Drop(int i, int j, ref int type)
        {
            return base.Drop(i, j, ref type);
        }
        public override ushort GetMapOption(int i, int j)
        {
            return base.GetMapOption(i, j);
        }
        public override bool KillSound(int i, int j)
        {
            return base.KillSound(i, j);
        }
        public override void KillWall(int i, int j, ref bool fail)
        {
            base.KillWall(i, j, ref fail);
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            base.ModifyLight(i, j, ref r, ref g, ref b);
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            base.NumDust(i, j, fail, ref num);
        }
        public override void PlaceInWorld(int i, int j, Item item)
        {
            base.PlaceInWorld(i, j, item);
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            base.PostDraw(i, j, spriteBatch);
        }
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            return base.PreDraw(i, j, spriteBatch);
        }
        public override void RandomUpdate(int i, int j)
        {
            base.RandomUpdate(i, j);
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
    }
}