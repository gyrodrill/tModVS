using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;

namespace ExampleMod.Items
{
    public class ExampleItem : ModItem
	{
        public override string Texture => base.Texture;
        public override bool CloneNewInstances => base.CloneNewInstances;
        public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("This is a modded item.");
		}
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = 100;
			item.rare = 1;
		}
        public override bool Autoload(ref string name)
        {
            return base.Autoload(ref name);
        }
        public override ModItem NewInstance(Item itemClone)
        {
            return base.NewInstance(itemClone);
        }
        public override void AutoDefaults()
        {
            base.AutoDefaults();
        }
        public override void AutoStaticDefaults()
        {
            base.AutoStaticDefaults();
        }
        public override int ChoosePrefix(UnifiedRandom rand)
        {
            return base.ChoosePrefix(rand);
        }
        public override bool CanUseItem(Player player)
        {
            return base.CanUseItem(player);
        }
        public override void UseStyle(Player player)
        {
            base.UseStyle(player);
        }
        public override void HoldStyle(Player player)
        {
            base.HoldStyle(player);
        }
        public override void HoldItem(Player player)
        {
            base.HoldItem(player);
        }
        public override float UseTimeMultiplier(Player player)
        {
            return base.UseTimeMultiplier(player);
        }
        public override float MeleeSpeedMultiplier(Player player)
        {
            return base.MeleeSpeedMultiplier(player);
        }
        public override void GetWeaponDamage(Player player, ref int damage)
        {
            base.GetWeaponDamage(player, ref damage);
        }
        public override void GetWeaponKnockback(Player player, ref float knockback)
        {
            base.GetWeaponKnockback(player, ref knockback);
        }
        public override void GetWeaponCrit(Player player, ref int crit)
        {
            base.GetWeaponCrit(player, ref crit);
        }
        public override void PickAmmo(Player player, ref int type, ref float speed, ref int damage, ref float knockback)
        {
            base.PickAmmo(player, ref type, ref speed, ref damage, ref knockback);
        }
        public override bool ConsumeAmmo(Player player)
        {
            return base.ConsumeAmmo(player);
        }
        public override void OnConsumeAmmo(Player player)
        {
            base.OnConsumeAmmo(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            base.UseItemHitbox(player, ref hitbox, ref noHitbox);
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            base.MeleeEffects(player, hitbox);
        }
        public override bool? CanHitNPC(Player player, NPC target)
        {
            return base.CanHitNPC(player, target);
        }
        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            base.ModifyHitNPC(player, target, ref damage, ref knockBack, ref crit);
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            base.OnHitNPC(player, target, damage, knockBack, crit);
        }
        public override bool CanHitPvp(Player player, Player target)
        {
            return base.CanHitPvp(player, target);
        }
        public override void ModifyHitPvp(Player player, Player target, ref int damage, ref bool crit)
        {
            base.ModifyHitPvp(player, target, ref damage, ref crit);
        }
        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            base.OnHitPvp(player, target, damage, crit);
        }
        public override bool UseItem(Player player)
        {
            return base.UseItem(player);
        }
        public override bool ConsumeItem(Player player)
        {
            return base.ConsumeItem(player);
        }
        public override void OnConsumeItem(Player player)
        {
            base.OnConsumeItem(player);
        }
        public override bool UseItemFrame(Player player)
        {
            return base.UseItemFrame(player);
        }
        public override bool HoldItemFrame(Player player)
        {
            return base.HoldItemFrame(player);
        }
        public override bool AltFunctionUse(Player player)
        {
            return base.AltFunctionUse(player);
        }
        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
        }
        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
        }
        public override void UpdateVanity(Player player, EquipType type)
        {
            base.UpdateVanity(player, type);
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return base.IsArmorSet(head, body, legs);
        }
        public override void UpdateArmorSet(Player player)
        {
            base.UpdateArmorSet(player);
        }
        public override bool IsVanitySet(int head, int body, int legs)
        {
            return base.IsVanitySet(head, body, legs);
        }
        public override void PreUpdateVanitySet(Player player)
        {
            base.PreUpdateVanitySet(player);
        }
        public override void UpdateVanitySet(Player player)
        {
            base.UpdateVanitySet(player);
        }
        public override void ArmorSetShadows(Player player)
        {
            base.ArmorSetShadows(player);
        }
        public override void SetMatch(bool male, ref int equipSlot, ref bool robes)
        {
            base.SetMatch(male, ref equipSlot, ref robes);
        }
        public override bool CanRightClick()
        {
            return base.CanRightClick();
        }
        public override void RightClick(Player player)
        {
            base.RightClick(player);
        }
        public override void OpenBossBag(Player player)
        {
            base.OpenBossBag(player);
        }
        public override bool ReforgePrice(ref int reforgePrice, ref bool canApplyDiscount)
        {
            return base.ReforgePrice(ref reforgePrice, ref canApplyDiscount);
        }
        public override bool NewPreReforge()
        {
            return base.NewPreReforge();
        }
        public override void PreReforge()
        {
            base.PreReforge();
        }
        public override void PostReforge()
        {
            base.PostReforge();
        }
        public override void DrawHands(ref bool drawHands, ref bool drawArms)
        {
            base.DrawHands(ref drawHands, ref drawArms);
        }
        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            base.DrawHair(ref drawHair, ref drawAltHair);
        }
        public override bool DrawHead()
        {
            return base.DrawHead();
        }
        public override bool DrawBody()
        {
            return base.DrawBody();
        }
        public override bool DrawLegs()
        {
            return base.DrawLegs();
        }
        public override void DrawArmorColor(Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor)
        {
            base.DrawArmorColor(drawPlayer, shadow, ref color, ref glowMask, ref glowMaskColor);
        }
        public override void ArmorArmGlowMask(Player drawPlayer, float shadow, ref int glowMask, ref Color color)
        {
            base.ArmorArmGlowMask(drawPlayer, shadow, ref glowMask, ref color);
        }
        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            base.VerticalWingSpeeds(player, ref ascentWhenFalling, ref ascentWhenRising, ref maxCanAscendMultiplier, ref maxAscentMultiplier, ref constantAscend);
        }
        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            base.HorizontalWingSpeeds(player, ref speed, ref acceleration);
        }
        public override bool WingUpdate(Player player, bool inUse)
        {
            return base.WingUpdate(player, inUse);
        }
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            base.Update(ref gravity, ref maxFallSpeed);
        }
        public override void PostUpdate()
        {
            base.PostUpdate();
        }
        public override void GrabRange(Player player, ref int grabRange)
        {
            base.GrabRange(player, ref grabRange);
        }
        public override bool GrabStyle(Player player)
        {
            return base.GrabStyle(player);
        }
        public override bool CanPickup(Player player)
        {
            return base.CanPickup(player);
        }
        public override bool OnPickup(Player player)
        {
            return base.OnPickup(player);
        }
        public override bool ItemSpace(Player player)
        {
            return base.ItemSpace(player);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return base.GetAlpha(lightColor);
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            return base.PreDrawInWorld(spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            base.PostDrawInWorld(spriteBatch, lightColor, alphaColor, rotation, scale, whoAmI);
        }
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            base.PostDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }
        public override Vector2? HoldoutOffset()
        {
            return base.HoldoutOffset();
        }
        public override Vector2? HoldoutOrigin()
        {
            return base.HoldoutOrigin();
        }
        public override bool CanEquipAccessory(Player player, int slot)
        {
            return base.CanEquipAccessory(player, slot);
        }
        public override void ExtractinatorUse(ref int resultType, ref int resultStack)
        {
            base.ExtractinatorUse(ref resultType, ref resultStack);
        }
        public override void AutoLightSelect(ref bool dryTorch, ref bool wetTorch, ref bool glowstick)
        {
            base.AutoLightSelect(ref dryTorch, ref wetTorch, ref glowstick);
        }
        public override void CaughtFishStack(ref int stack)
        {
            base.CaughtFishStack(ref stack);
        }
        public override bool IsQuestFish()
        {
            return base.IsQuestFish();
        }
        public override bool IsAnglerQuestAvailable()
        {
            return base.IsAnglerQuestAvailable();
        }
        public override void AnglerQuestChat(ref string description, ref string catchLocation)
        {
            base.AnglerQuestChat(ref description, ref catchLocation);
        }
        public override TagCompound Save()
        {
            return base.Save();
        }
        public override void Load(TagCompound tag)
        {
            base.Load(tag);
        }
        public override void LoadLegacy(BinaryReader reader)
        {
            base.LoadLegacy(reader);
        }
        public override void NetSend(BinaryWriter writer)
        {
            base.NetSend(writer);
        }
        public override void NetRecieve(BinaryReader reader)
        {
            base.NetRecieve(reader);
        }
        public override void OnCraft(Recipe recipe)
        {
            base.OnCraft(recipe);
        }
        public override bool PreDrawTooltip(ReadOnlyCollection<TooltipLine> lines, ref int x, ref int y)
        {
            return base.PreDrawTooltip(lines, ref x, ref y);
        }
        public override void PostDrawTooltip(ReadOnlyCollection<DrawableTooltipLine> lines)
        {
            base.PostDrawTooltip(lines);
        }
        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            return base.PreDrawTooltipLine(line, ref yOffset);
        }
        public override void PostDrawTooltipLine(DrawableTooltipLine line)
        {
            base.PostDrawTooltipLine(line);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(tooltips);
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
        }
    }
}
