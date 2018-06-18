using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ExampleMod.Mounts
{
    public class ExampleMount : ModMountData
    {
        public override void AimAbility(Player player, Vector2 mousePosition)
        {
            base.AimAbility(player, mousePosition);
        }
        public override bool Autoload(ref string name, ref string texture, IDictionary<MountTextureType, string> extraTextures)
        {
            return base.Autoload(ref name, ref texture, extraTextures);
        }
        public override void JumpHeight(ref int jumpHeight, float xVelocity)
        {
            base.JumpHeight(ref jumpHeight, xVelocity);
        }
        public override void JumpSpeed(ref float jumpSeed, float xVelocity)
        {
            base.JumpSpeed(ref jumpSeed, xVelocity);
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
        public override void UpdateEffects(Player player)
        {
            base.UpdateEffects(player);
        }
        public override bool UpdateFrame(Player mountedPlayer, int state, Vector2 velocity)
        {
            return base.UpdateFrame(mountedPlayer, state, velocity);
        }
        public override void UseAbility(Player player, Vector2 mousePosition, bool toggleOn)
        {
            base.UseAbility(player, mousePosition, toggleOn);
        }
    }
}