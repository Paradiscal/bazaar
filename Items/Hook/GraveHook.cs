﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Bazaar.Items.Hook
{
	class GraveHook : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Grave Magnet");
		}
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ZombieMetal", 15);
                      recipe.AddTile(16);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.AmethystHook);
			item.shootSpeed = 50f;
			item.shoot = mod.ProjectileType("GraveHookProjectile");
		}
	}
	class GraveHookProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("${ProjectileName.GemHookAmethyst}");
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.GemHookAmethyst);
		}
		public override float GrappleRange()
		{
			return 100f;
		}

		public override void NumGrappleHooks(Player player, ref int numHooks)
		{
			numHooks = 1;
		}

		public override void GrappleRetreatSpeed(Player player, ref float speed)
		{
			speed = 10f;
		}

		public override void GrapplePullSpeed(Player player, ref float speed)
		{
			speed = 15;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 playerCenter = Main.player[projectile.owner].MountedCenter;
			Vector2 center = projectile.Center;
			Vector2 distToProj = playerCenter - projectile.Center;
			float projRotation = distToProj.ToRotation() - 1.57f;
			float distance = distToProj.Length();
			while (distance > 30f && !float.IsNaN(distance))
			{
				distToProj.Normalize();             
				distToProj *= 24f;               
				center += distToProj;                 
				distToProj = playerCenter - center;    
				distance = distToProj.Length();
				Color drawColor = lightColor;

				spriteBatch.Draw(mod.GetTexture("Items/Hook/GraveHookChain"), new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y),
					new Rectangle(0, 0, Main.chain30Texture.Width, Main.chain30Texture.Height), drawColor, projRotation,
					new Vector2(Main.chain30Texture.Width * 0.5f, Main.chain30Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
			}
			return true;
		}
	}
}
