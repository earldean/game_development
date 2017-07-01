using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject
{
	/// <summary>
	/// A burger
	/// </summary>
	public class Burger
	{
		#region Fields

		// graphic and drawing info
		Texture2D sprite;
		Rectangle drawRectangle;

		// burger stats
		int health = 100;

		// click processing 
		bool leftClickStarted = false;
		bool leftButtonReleased = true;

		// shooting support
		bool canShoot = true;
		int elapsedCooldownMilliseconds = 0;

		// sound effect
		SoundEffect shootSound;

		#endregion

		#region Constructors

		/// <summary>
		///  Constructs a burger
		/// </summary>
		/// <param name="contentManager">the content manager for loading content</param>
		/// <param name="spriteName">the sprite name</param>
		/// <param name="x">the x location of the center of the burger</param>
		/// <param name="y">the y location of the center of the burger</param>
		/// <param name="shootSound">the sound the burger plays when shooting</param>
		public Burger(ContentManager contentManager, string spriteName, int x, int y,
			SoundEffect shootSound)
		{
			LoadContent(contentManager, spriteName, x, y);
			this.shootSound = shootSound;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the collision rectangle for the burger
		/// </summary>
		public Rectangle CollisionRectangle
		{
			get { return drawRectangle; }
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Updates the burger's location based on mouse. Also fires 
		/// french fries as appropriate
		/// </summary>
		/// <param name="gameTime">game time</param>
		/// <param name="mouse">the current state of the mouse</param>
		public void Update(GameTime gameTime, MouseState mouse, KeyboardState keyState)
		{
			// burger should only respond to input if it still has health
			if (health > 0)
			{
				// check key board input to move the burger 
				if (keyState.IsKeyDown(Keys.Left)) 
				{
					moveLeft ();
				} 
				else if (keyState.IsKeyDown(Keys.Right)) 
				{
					moveRight ();
				} 
				else if (keyState.IsKeyDown(Keys.Up)) 
				{
					moveUp ();
				} 
				else if (keyState.IsKeyDown(Keys.Down)) 
				{
					moveDown ();	
				}

				// allow user to hold down left mouse button and keep firing at a constant rate
				if (!canShoot)
				{
					elapsedCooldownMilliseconds += gameTime.ElapsedGameTime.Milliseconds;

					if (elapsedCooldownMilliseconds >= GameConstants.BurgerTotalCooldownMilliseconds) 
					{
						this.addFryProjectile ();
						elapsedCooldownMilliseconds = 0;
					}
				}

				// check if left mouse button is pressed and create projectile
				if (mouse.LeftButton == ButtonState.Pressed && leftButtonReleased) 
				{
					this.addFryProjectile ();
					canShoot = false;

					leftClickStarted = true;
					leftButtonReleased = false;
				} 
				else if (mouse.LeftButton == ButtonState.Released) 
				{
					// if left mouse button is released the user can fire again imedeatly 
					leftButtonReleased = true;
					canShoot = true;
				}
			}

			// clamp burger in window

			// update shooting allowed
			// timer concept (for animations) introduced in Chapter 7

			// shoot if appropriate

		}

		/// <summary>
		/// Draws the burger
		/// </summary>
		/// <param name="spriteBatch">the sprite batch to use</param>
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw (sprite, drawRectangle, Color.White);
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Loads the content for the burger
		/// </summary>
		/// <param name="contentManager">the content manager to use</param>
		/// <param name="spriteName">the name of the sprite for the burger</param>
		/// <param name="x">the x location of the center of the burger</param>
		/// <param name="y">the y location of the center of the burger</param>
		private void LoadContent(ContentManager contentManager, string spriteName,
			int x, int y)
		{
			// load content and set remainder of draw rectangle
			sprite = contentManager.Load<Texture2D>(spriteName);
			drawRectangle = new Rectangle(x - sprite.Width / 2,
				y - sprite.Height / 2, sprite.Width,
				sprite.Height);
		}

		/// <summary>
		/// Adds the frie projectile.
		/// </summary>
		private void addFryProjectile() 
		{
			Projectile fry = new Projectile (ProjectileType.FrenchFries,
				Game1.GetProjectileSprite (ProjectileType.FrenchFries), 
				drawRectangle.Center.X,
				drawRectangle.Center.Y - GameConstants.FrenchFriesProjectileOffset, 
				GameConstants.FrenchFriesProjectileSpeed);

			Game1.AddProjectile (fry);
		}

		/// <summary>
		/// Moves burger the left.
		/// </summary>
		private void moveLeft () 
		{
			if (drawRectangle.Left - 2 <= 0) 
			{
				drawRectangle.X = 1;
			} 
			else 
			{
				drawRectangle.X -= 2;
			}
		}

		/// <summary>
		/// Moves burger the right.
		/// </summary>
		private void moveRight () 
		{
			if (drawRectangle.Right + 2 >= GameConstants.WindowWidth)
			{
				drawRectangle.X = GameConstants.WindowWidth - (drawRectangle.Width + 1); 
			} 
			else 
			{
				drawRectangle.X += 2;
			}
		}

		/// <summary>
		/// Moves the burger up.
		/// </summary>
		private void moveUp () 
		{
			if (drawRectangle.Top - 2 <= 0) 
			{
				drawRectangle.Y = 1;
			}
			else 
			{
				drawRectangle.Y -= 2;
			}
		}

		/// <summary>
		/// Moves the burger down.
		/// </summary>
		private void moveDown () 
		{
			if (drawRectangle.Bottom + 2 >= GameConstants.WindowHeight - 1) 
			{
				drawRectangle.Y = GameConstants.WindowHeight - (drawRectangle.Height + 1);
			}
			else 
			{
				drawRectangle.Y += 2;
			}
		}

		#endregion
	}
}
