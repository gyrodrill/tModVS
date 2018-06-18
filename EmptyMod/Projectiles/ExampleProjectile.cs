using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using System.Linq;

namespace ExampleMod.Projectiles
{
    public class ExampleProjectile : ModProjectile
    {
        public override string Texture => base.Texture;
        public override bool CloneNewInstances => base.CloneNewInstances;
        public override void AI()
        {
            base.AI();
        }
        public override bool Autoload(ref string name)
        {
            return base.Autoload(ref name);
        }
        public override void AutoStaticDefaults()
        {
            base.AutoStaticDefaults();
        }
        public override bool? CanCutTiles()
        {
            return base.CanCutTiles();
        }
        public override bool CanDamage()
        {
            return base.CanDamage();
        }
        public override bool? CanHitNPC(NPC target)
        {
            return base.CanHitNPC(target);
        }
        public override bool CanHitPlayer(Player target)
        {
            return base.CanHitPlayer(target);
        }
        public override bool CanHitPvp(Player target)
        {
            return base.CanHitPvp(target);
        }
        public override bool? CanUseGrapple(Player player)
        {
            return base.CanUseGrapple(player);
        }
        public override ModProjectile Clone()
        {
            return base.Clone();
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return base.Colliding(projHitbox, targetHitbox);
        }
        public override void CutTiles()
        {
            base.CutTiles();
        }
        public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
        {
            base.DrawBehind(index, drawCacheProjsBehindNPCsAndTiles, drawCacheProjsBehindNPCs, drawCacheProjsBehindProjectiles, drawCacheProjsOverWiresUI);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return base.GetAlpha(lightColor);
        }
        public override void GrapplePullSpeed(Player player, ref float speed)
        {
            base.GrapplePullSpeed(player, ref speed);
        }
        public override float GrappleRange()
        {
            return base.GrappleRange();
        }
        public override void GrappleRetreatSpeed(Player player, ref float speed)
        {
            base.GrappleRetreatSpeed(player, ref speed);
        }
        public override void Kill(int timeLeft)
        {
            base.Kill(timeLeft);
        }
        public override bool MinionContactDamage()
        {
            return base.MinionContactDamage();
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            base.ModifyDamageHitbox(ref hitbox);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            base.ModifyHitPlayer(target, ref damage, ref crit);
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            base.ModifyHitPvp(target, ref damage, ref crit);
        }
        public override ModProjectile NewInstance(Projectile projectileClone)
        {
            return base.NewInstance(projectileClone);
        }
        public override void NumGrappleHooks(Player player, ref int numHooks)
        {
            base.NumGrappleHooks(player, ref numHooks);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            base.OnHitPlayer(target, damage, crit);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            base.OnHitPvp(target, damage, crit);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return base.OnTileCollide(oldVelocity);
        }
        public override void PostAI()
        {
            base.PostAI();
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            base.PostDraw(spriteBatch, lightColor);
        }
        public override bool PreAI()
        {
            return base.PreAI();
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            return base.PreDraw(spriteBatch, lightColor);
        }
        public override bool PreDrawExtras(SpriteBatch spriteBatch)
        {
            return base.PreDrawExtras(spriteBatch);
        }
        public override bool PreKill(int timeLeft)
        {
            return base.PreKill(timeLeft);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override bool ShouldUpdatePosition()
        {
            return base.ShouldUpdatePosition();
        }
        public override bool? SingleGrappleHook(Player player)
        {
            return base.SingleGrappleHook(player);
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }
        public override void UseGrapple(Player player, ref int type)
        {
            base.UseGrapple(player, ref type);
        }
    }
}
