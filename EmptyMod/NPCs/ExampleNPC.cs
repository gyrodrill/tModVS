using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ExampleMod.NPCs
{
    public class ExampleNPC : GlobalNPC
    {
        public override bool InstancePerEntity => base.InstancePerEntity;
        public override bool CloneNewInstances => base.CloneNewInstances;
        public override void AI(NPC npc)
        {
            base.AI(npc);
        }
        public override bool Autoload(ref string name)
        {
            return base.Autoload(ref name);
        }
        public override void BossHeadRotation(NPC npc, ref float rotation)
        {
            base.BossHeadRotation(npc, ref rotation);
        }
        public override void BossHeadSlot(NPC npc, ref int index)
        {
            base.BossHeadSlot(npc, ref index);
        }
        public override void BossHeadSpriteEffects(NPC npc, ref SpriteEffects spriteEffects)
        {
            base.BossHeadSpriteEffects(npc, ref spriteEffects);
        }
        public override void BuffTownNPC(ref float damageMult, ref int defense)
        {
            base.BuffTownNPC(ref damageMult, ref defense);
        }
        public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
        {
            return base.CanBeHitByItem(npc, player, item);
        }
        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
        {
            return base.CanBeHitByProjectile(npc, projectile);
        }
        public override bool? CanHitNPC(NPC npc, NPC target)
        {
            return base.CanHitNPC(npc, target);
        }
        public override bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot)
        {
            return base.CanHitPlayer(npc, target, ref cooldownSlot);
        }
        public override bool CheckActive(NPC npc)
        {
            return base.CheckActive(npc);
        }
        public override bool CheckDead(NPC npc)
        {
            return base.CheckDead(npc);
        }
        public override GlobalNPC Clone()
        {
            return base.Clone();
        }
        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            base.DrawEffects(npc, ref drawColor);
        }
        public override bool? DrawHealthBar(NPC npc, byte hbPosition, ref float scale, ref Vector2 position)
        {
            return base.DrawHealthBar(npc, hbPosition, ref scale, ref position);
        }
        public override void DrawTownAttackGun(NPC npc, ref float scale, ref int item, ref int closeness)
        {
            base.DrawTownAttackGun(npc, ref scale, ref item, ref closeness);
        }
        public override void DrawTownAttackSwing(NPC npc, ref Texture2D item, ref int itemSize, ref float scale, ref Vector2 offset)
        {
            base.DrawTownAttackSwing(npc, ref item, ref itemSize, ref scale, ref offset);
        }
        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            base.EditSpawnPool(pool, spawnInfo);
        }
        public override void EditSpawnRange(Player player, ref int spawnRangeX, ref int spawnRangeY, ref int safeRangeX, ref int safeRangeY)
        {
            base.EditSpawnRange(player, ref spawnRangeX, ref spawnRangeY, ref safeRangeX, ref safeRangeY);
        }
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            base.EditSpawnRate(player, ref spawnRate, ref maxSpawns);
        }
        public override void FindFrame(NPC npc, int frameHeight)
        {
            base.FindFrame(npc, frameHeight);
        }
        public override Color? GetAlpha(NPC npc, Color drawColor)
        {
            return base.GetAlpha(npc, drawColor);
        }
        public override void GetChat(NPC npc, ref string chat)
        {
            base.GetChat(npc, ref chat);
        }
        public override void HitEffect(NPC npc, int hitDirection, double damage)
        {
            base.HitEffect(npc, hitDirection, damage);
        }
        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            base.ModifyHitByItem(npc, player, item, ref damage, ref knockback, ref crit);
        }
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            base.ModifyHitByProjectile(npc, projectile, ref damage, ref knockback, ref crit, ref hitDirection);
        }
        public override void ModifyHitNPC(NPC npc, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            base.ModifyHitNPC(npc, target, ref damage, ref knockback, ref crit);
        }
        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
        {
            base.ModifyHitPlayer(npc, target, ref damage, ref crit);
        }
        public override GlobalNPC NewInstance(NPC npc)
        {
            return base.NewInstance(npc);
        }
        public override void NPCLoot(NPC npc)
        {
            base.NPCLoot(npc);
        }
        public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
        {
            base.OnHitByItem(npc, player, item, damage, knockback, crit);
        }
        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            base.OnHitByProjectile(npc, projectile, damage, knockback, crit);
        }
        public override void OnHitNPC(NPC npc, NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(npc, target, damage, knockback, crit);
        }
        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            base.OnHitPlayer(npc, target, damage, crit);
        }
        public override void PostAI(NPC npc)
        {
            base.PostAI(npc);
        }
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            base.PostDraw(npc, spriteBatch, drawColor);
        }
        public override bool PreAI(NPC npc)
        {
            return base.PreAI(npc);
        }
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            return base.PreDraw(npc, spriteBatch, drawColor);
        }
        public override bool PreNPCLoot(NPC npc)
        {
            return base.PreNPCLoot(npc);
        }
        public override void ResetEffects(NPC npc)
        {
            base.ResetEffects(npc);
        }
        public override void ScaleExpertStats(NPC npc, int numPlayers, float bossLifeScale)
        {
            base.ScaleExpertStats(npc, numPlayers, bossLifeScale);
        }
        public override void SetDefaults(NPC npc)
        {
            base.SetDefaults(npc);
        }
        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            base.SetupShop(type, shop, ref nextSlot);
        }
        public override void SetupTravelShop(int[] shop, ref int nextSlot)
        {
            base.SetupTravelShop(shop, ref nextSlot);
        }
        public override void SpawnNPC(int npc, int tileX, int tileY)
        {
            base.SpawnNPC(npc, tileX, tileY);
        }
        public override bool SpecialNPCLoot(NPC npc)
        {
            return base.SpecialNPCLoot(npc);
        }
        public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            return base.StrikeNPC(npc, ref damage, defense, ref knockback, hitDirection, ref crit);
        }
        public override void TownNPCAttackCooldown(NPC npc, ref int cooldown, ref int randExtraCooldown)
        {
            base.TownNPCAttackCooldown(npc, ref cooldown, ref randExtraCooldown);
        }
        public override void TownNPCAttackMagic(NPC npc, ref float auraLightMultiplier)
        {
            base.TownNPCAttackMagic(npc, ref auraLightMultiplier);
        }
        public override void TownNPCAttackProj(NPC npc, ref int projType, ref int attackDelay)
        {
            base.TownNPCAttackProj(npc, ref projType, ref attackDelay);
        }
        public override void TownNPCAttackProjSpeed(NPC npc, ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            base.TownNPCAttackProjSpeed(npc, ref multiplier, ref gravityCorrection, ref randomOffset);
        }
        public override void TownNPCAttackShoot(NPC npc, ref bool inBetweenShots)
        {
            base.TownNPCAttackShoot(npc, ref inBetweenShots);
        }
        public override void TownNPCAttackStrength(NPC npc, ref int damage, ref float knockback)
        {
            base.TownNPCAttackStrength(npc, ref damage, ref knockback);
        }
        public override void TownNPCAttackSwing(NPC npc, ref int itemWidth, ref int itemHeight)
        {
            base.TownNPCAttackSwing(npc, ref itemWidth, ref itemHeight);
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            base.UpdateLifeRegen(npc, ref damage);
        }
    }
}
