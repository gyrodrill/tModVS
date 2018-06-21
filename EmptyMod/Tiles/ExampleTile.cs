using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace ExampleMod.Tiles
{
    internal sealed class ExampleTile : GlobalTile
    {
        public override int[] AdjTiles(int type)
        {
            return base.AdjTiles(type);
        }
        public override void AnimateTile()
        {
            base.AnimateTile();
        }
        public override bool Autoload(ref string name)
        {
            return base.Autoload(ref name);
        }
        public override bool AutoSelect(int i, int j, int type, Item item)
        {
            return base.AutoSelect(i, j, type, item);
        }
        public override bool CanExplode(int i, int j, int type)
        {
            return base.CanExplode(i, j, type);
        }
        public override bool CanKillTile(int i, int j, int type, ref bool blockDamaged)
        {
            return base.CanKillTile(i, j, type, ref blockDamaged);
        }
        public override bool CanPlace(int i, int j, int type)
        {
            return base.CanPlace(i, j, type);
        }
        public override void ChangeWaterfallStyle(int type, ref int style)
        {
            base.ChangeWaterfallStyle(type, ref style);
        }
        public override bool CreateDust(int i, int j, int type, ref int dustType)
        {
            return base.CreateDust(i, j, type, ref dustType);
        }
        public override bool Dangersense(int i, int j, int type, Player player)
        {
            return base.Dangersense(i, j, type, player);
        }
        public override void DrawEffects(int i, int j, int type, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            base.DrawEffects(i, j, type, spriteBatch, ref drawColor, ref nextSpecialDrawIndex);
        }
        public override bool Drop(int i, int j, int type)
        {
            return base.Drop(i, j, type);
        }
        public override void DropCritterChance(int i, int j, int type, ref int wormChance, ref int grassHopperChance, ref int jungleGrubChance)
        {
            base.DropCritterChance(i, j, type, ref wormChance, ref grassHopperChance, ref jungleGrubChance);
        }
        public override void FloorVisuals(int type, Player player)
        {
            base.FloorVisuals(type, player);
        }
        public override void HitWire(int i, int j, int type)
        {
            base.HitWire(i, j, type);
        }
        public override bool KillSound(int i, int j, int type)
        {
            return base.KillSound(i, j, type);
        }
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            base.KillTile(i, j, type, ref fail, ref effectOnly, ref noItem);
        }
        public override void ModifyLight(int i, int j, int type, ref float r, ref float g, ref float b)
        {
            base.ModifyLight(i, j, type, ref r, ref g, ref b);
        }
        public override void MouseOver(int i, int j, int type)
        {
            base.MouseOver(i, j, type);
        }
        public override void MouseOverFar(int i, int j, int type)
        {
            base.MouseOverFar(i, j, type);
        }
        public override void NearbyEffects(int i, int j, int type, bool closer)
        {
            base.NearbyEffects(i, j, type, closer);
        }
        public override void NumDust(int i, int j, int type, bool fail, ref int num)
        {
            base.NumDust(i, j, type, fail, ref num);
        }
        public override void PlaceInWorld(int i, int j, Item item)
        {
            base.PlaceInWorld(i, j, item);
        }
        public override void PostDraw(int i, int j, int type, SpriteBatch spriteBatch)
        {
            base.PostDraw(i, j, type, spriteBatch);
        }
        public override bool PreDraw(int i, int j, int type, SpriteBatch spriteBatch)
        {
            return base.PreDraw(i, j, type, spriteBatch);
        }
        public override bool PreHitWire(int i, int j, int type)
        {
            return base.PreHitWire(i, j, type);
        }
        public override void RandomUpdate(int i, int j, int type)
        {
            base.RandomUpdate(i, j, type);
        }
        public override void RightClick(int i, int j, int type)
        {
            base.RightClick(i, j, type);
        }
        public override int SaplingGrowthType(int type, ref int style)
        {
            return base.SaplingGrowthType(type, ref style);
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
        public override void SetSpriteEffects(int i, int j, int type, ref SpriteEffects spriteEffects)
        {
            base.SetSpriteEffects(i, j, type, ref spriteEffects);
        }
        public override bool Slope(int i, int j, int type)
        {
            return base.Slope(i, j, type);
        }
        public override void SpecialDraw(int i, int j, int type, SpriteBatch spriteBatch)
        {
            base.SpecialDraw(i, j, type, spriteBatch);
        }
        public override bool TileFrame(int i, int j, int type, ref bool resetFrame, ref bool noBreak)
        {
            return base.TileFrame(i, j, type, ref resetFrame, ref noBreak);
        }
    }
}
