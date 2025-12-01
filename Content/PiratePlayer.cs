using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace Pirate.Content
{
    public class PiratePlayer : ModPlayer
    {

        // Pirate Damage
        public float pirateDamage = 0f;

        //Accessories

        //Big Feather
        public bool dashFeather = false;

        public enum DashType
        {
            feather = 0,
            invalid = -1
        }

        public DashType dashType = DashType.feather;

        public const int dashDown = 0;
        public const int dashUp = 1;
        public const int dashRight = 2;
        public const int dashLeft = 3;

        public int dashCooldown = 45;
        public int dashDuration = 25;

        public float dashVelocity = 7.5f;

        public int dashDir = -1;

        public bool dashAccessoryEquipped = false;
        public int dashDelay = 0;
        public int dashTimer = 0;

        public override void ResetEffects()
        {
            //Pirate Damage reset
            pirateDamage = 0f;

            // BigFeather
            Player.pickSpeed = 0.8f;

            dashFeather = false;
            dashAccessoryEquipped = false;

            if (Player.controlRight && Player.releaseRight && Player.doubleTapCardinalTimer[dashRight] < 15)
            {
                dashDir = dashRight;
            }
            else if (Player.controlLeft && Player.releaseLeft && Player.doubleTapCardinalTimer[dashLeft] < 15)
            {
                dashDir = dashLeft;
            }
            else
            {
                dashDir = -1;
            }
        }

        public override void PreUpdateMovement()
        {
            if (canUseDash() && dashDir != -1 && dashDelay == 0)
            {
                Vector2 newVelocity = Player.velocity;

                switch (dashDir)
                {
                    case dashLeft when Player.velocity.X > -dashVelocity:
                    case dashRight when Player.velocity.X < dashVelocity:
                        {
                            float dashDirection = dashDir == dashRight ? 1 : -1;
                            newVelocity.X = dashDirection * dashVelocity;
                            break;
                        }
                    default:
                        return;
                }

                dashDelay = dashCooldown;
                dashTimer = dashDelay;
                Player.velocity = newVelocity;
                SoundEngine.PlaySound(SoundID.DD2_MonkStaffSwing, Player.position);
            }

            if (dashDelay > 0)
                dashDelay--;

            if (dashDelay > dashCooldown - 10)
            {
                switch (dashType)
                {
                    case DashType.feather:
                        int dust = Dust.NewDust(Player.position, Player.width, Player.height, DustID.Cloud, 0, 0, default, default, Main.rand.NextFloat(0.75f, 1.25f));
                        Main.dust[dust].velocity.Y = 0;
                        Main.dust[dust].velocity.X = Player.direction * -1;
                        break;
                }
            }

            if (dashTimer > 0)
            {
                Player.eocDash = dashTimer;
                dashTimer--;
            }
        }

        private bool canUseDash()
        {
            return dashAccessoryEquipped
                && Player.dashType == 0
                && !Player.setSolar
                && dashDir != -1
                && dashDelay == 0
                && !Player.mount.Active;
        }
    }
}