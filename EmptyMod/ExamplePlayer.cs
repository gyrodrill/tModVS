using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ExampleMod
{
    public class ExamplePlayer : ModPlayer
    {
        public override bool CloneNewInstances => base.CloneNewInstances;
        public override void AnglerQuestReward(float rareMultiplier, List<Item> rewardItems)
        {
            base.AnglerQuestReward(rareMultiplier, rewardItems);
        }
        public override bool Autoload(ref string name)
        {
            return base.Autoload(ref name);
        }
        public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
        {
            return base.CanBeHitByNPC(npc, ref cooldownSlot);
        }
        public override bool CanBeHitByProjectile(Projectile proj)
        {
            return base.CanBeHitByProjectile(proj);
        }
        public override bool? CanHitNPC(Item item, NPC target)
        {
            return base.CanHitNPC(item, target);
        }
        public override bool? CanHitNPCWithProj(Projectile proj, NPC target)
        {
            return base.CanHitNPCWithProj(proj, target);
        }
        public override bool CanHitPvp(Item item, Player target)
        {
            return base.CanHitPvp(item, target);
        }
        public override bool CanHitPvpWithProj(Projectile proj, Player target)
        {
            return base.CanHitPvpWithProj(proj, target);
        }
        public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
        {
            base.CatchFish(fishingRod, bait, power, liquidType, poolSize, worldLayer, questFish, ref caughtType, ref junk);
        }
        public override void clientClone(ModPlayer clientClone)
        {
            base.clientClone(clientClone);
        }
        public override bool ConsumeAmmo(Item weapon, Item ammo)
        {
            return base.ConsumeAmmo(weapon, ammo);
        }
        public override void CopyCustomBiomesTo(Player other)
        {
            base.CopyCustomBiomesTo(other);
        }
        public override bool CustomBiomesMatch(Player other)
        {
            return base.CustomBiomesMatch(other);
        }
        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            base.DrawEffects(drawInfo, ref r, ref g, ref b, ref a, ref fullBright);
        }
        public override void FrameEffects()
        {
            base.FrameEffects();
        }
        public override void GetDyeTraderReward(List<int> rewardPool)
        {
            base.GetDyeTraderReward(rewardPool);
        }
        public override void GetFishingLevel(Item fishingRod, Item bait, ref int fishingLevel)
        {
            base.GetFishingLevel(fishingRod, bait, ref fishingLevel);
        }
        public override Texture2D GetMapBackgroundImage()
        {
            return base.GetMapBackgroundImage();
        }
        public override void GetWeaponCrit(Item item, ref int crit)
        {
            base.GetWeaponCrit(item, ref crit);
        }
        public override void GetWeaponDamage(Item item, ref int damage)
        {
            base.GetWeaponDamage(item, ref damage);
        }
        public override void GetWeaponKnockback(Item item, ref float knockback)
        {
            base.GetWeaponKnockback(item, ref knockback);
        }
        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            base.Hurt(pvp, quiet, damage, hitDirection, crit);
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            base.Kill(damage, hitDirection, pvp, damageSource);
        }
        public override void Load(TagCompound tag)
        {
            base.Load(tag);
        }
        public override void LoadLegacy(BinaryReader reader)
        {
            base.LoadLegacy(reader);
        }
        public override void MeleeEffects(Item item, Rectangle hitbox)
        {
            base.MeleeEffects(item, hitbox);
        }
        public override float MeleeSpeedMultiplier(Item item)
        {
            return base.MeleeSpeedMultiplier(item);
        }
        public override void ModifyDrawHeadLayers(List<PlayerHeadLayer> layers)
        {
            base.ModifyDrawHeadLayers(layers);
        }
        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
        {
            base.ModifyDrawInfo(ref drawInfo);
        }
        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            base.ModifyDrawLayers(layers);
        }
        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            base.ModifyHitByNPC(npc, ref damage, ref crit);
        }
        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            base.ModifyHitByProjectile(proj, ref damage, ref crit);
        }
        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            base.ModifyHitNPC(item, target, ref damage, ref knockback, ref crit);
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            base.ModifyHitNPCWithProj(proj, target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
        public override void ModifyHitPvp(Item item, Player target, ref int damage, ref bool crit)
        {
            base.ModifyHitPvp(item, target, ref damage, ref crit);
        }
        public override void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit)
        {
            base.ModifyHitPvpWithProj(proj, target, ref damage, ref crit);
        }
        public override void ModifyScreenPosition()
        {
            base.ModifyScreenPosition();
        }
        public override void ModifyZoom(ref float zoom)
        {
            base.ModifyZoom(ref zoom);
        }
        public override void NaturalLifeRegen(ref float regen)
        {
            base.NaturalLifeRegen(ref regen);
        }
        public override void OnConsumeAmmo(Item weapon, Item ammo)
        {
            base.OnConsumeAmmo(weapon, ammo);
        }
        public override void OnEnterWorld(Player player)
        {
            base.OnEnterWorld(player);
        }
        public override void OnHitAnything(float x, float y, Entity victim)
        {
            base.OnHitAnything(x, y, victim);
        }
        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            base.OnHitByNPC(npc, damage, crit);
        }
        public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
            base.OnHitByProjectile(proj, damage, crit);
        }
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(item, target, damage, knockback, crit);
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPCWithProj(proj, target, damage, knockback, crit);
        }
        public override void OnHitPvp(Item item, Player target, int damage, bool crit)
        {
            base.OnHitPvp(item, target, damage, crit);
        }
        public override void OnHitPvpWithProj(Projectile proj, Player target, int damage, bool crit)
        {
            base.OnHitPvpWithProj(proj, target, damage, crit);
        }
        public override void OnRespawn(Player player)
        {
            base.OnRespawn(player);
        }
        public override void PlayerConnect(Player player)
        {
            base.PlayerConnect(player);
        }
        public override void PlayerDisconnect(Player player)
        {
            base.PlayerDisconnect(player);
        }
        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            base.PostHurt(pvp, quiet, damage, hitDirection, crit);
        }
        public override void PostItemCheck()
        {
            base.PostItemCheck();
        }
        public override void PostSavePlayer()
        {
            base.PostSavePlayer();
        }
        public override void PostUpdate()
        {
            base.PostUpdate();
        }
        public override void PostUpdateBuffs()
        {
            base.PostUpdateBuffs();
        }
        public override void PostUpdateEquips()
        {
            base.PostUpdateEquips();
        }
        public override void PostUpdateMiscEffects()
        {
            base.PostUpdateMiscEffects();
        }
        public override void PostUpdateRunSpeeds()
        {
            base.PostUpdateRunSpeeds();
        }
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }
        public override bool PreItemCheck()
        {
            return base.PreItemCheck();
        }
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
        }
        public override void PreSaveCustomData()
        {
            base.PreSaveCustomData();
        }
        public override void PreSavePlayer()
        {
            base.PreSavePlayer();
        }
        public override void PreUpdate()
        {
            base.PreUpdate();
        }
        public override void PreUpdateBuffs()
        {
            base.PreUpdateBuffs();
        }
        public override void PreUpdateMovement()
        {
            base.PreUpdateMovement();
        }
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            base.ProcessTriggers(triggersSet);
        }
        public override void ReceiveCustomBiomes(BinaryReader reader)
        {
            base.ReceiveCustomBiomes(reader);
        }
        public override void ResetEffects()
        {
            base.ResetEffects();
        }
        public override TagCompound Save()
        {
            return base.Save();
        }
        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            base.SendClientChanges(clientPlayer);
        }
        public override void SendCustomBiomes(BinaryWriter writer)
        {
            base.SendCustomBiomes(writer);
        }
        public override void SetControls()
        {
            base.SetControls();
        }
        public override void SetupStartInventory(IList<Item> items)
        {
            base.SetupStartInventory(items);
        }
        public override bool ShiftClickSlot(Item[] inventory, int context, int slot)
        {
            return base.ShiftClickSlot(inventory, context, slot);
        }
        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            return base.Shoot(item, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            base.SyncPlayer(toWho, fromWho, newPlayer);
        }
        public override void UpdateBadLifeRegen()
        {
            base.UpdateBadLifeRegen();
        }
        public override void UpdateBiomes()
        {
            base.UpdateBiomes();
        }
        public override void UpdateBiomeVisuals()
        {
            base.UpdateBiomeVisuals();
        }
        public override void UpdateDead()
        {
            base.UpdateDead();
        }
        public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff)
        {
            base.UpdateEquips(ref wallSpeedBuff, ref tileSpeedBuff, ref tileRangeBuff);
        }
        public override void UpdateLifeRegen()
        {
            base.UpdateLifeRegen();
        }
        public override void UpdateVanityAccessories()
        {
            base.UpdateVanityAccessories();
        }
        public override float UseTimeMultiplier(Item item)
        {
            return base.UseTimeMultiplier(item);
        }
    }
}
