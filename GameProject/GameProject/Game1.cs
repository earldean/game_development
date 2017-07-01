#region Using Statements
using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

#endregion

namespace GameProject
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		// game objects. Using inheritance would make this
		// easier, but inheritance isn't a GDD 1200 topic
		Burger burger;
		List<TeddyBear> bears = new List<TeddyBear>();
		static List<Projectile> projectiles = new List<Projectile>();
		List<Explosion> explosions = new List<Explosion>();

		// projectile and explosion sprites. Saved so they don't have to
		// be loaded every time projectiles or explosions are created
		static Texture2D frenchFriesSprite;
		static Texture2D teddyBearProjectileSprite;
		static Texture2D explosionSpriteStrip;

		// scoring support
		int score = 0;
		string scoreString = GameConstants.ScorePrefix + 0;

		// health support
		string healthString = GameConstants.HealthPrefix +
			GameConstants.BurgerInitialHealth;
		bool burgerDead = false;

		// text display support
		SpriteFont font;

		// sound effects
		SoundEffect burgerDamage;
		SoundEffect burgerDeath;
		SoundEffect burgerShot;
		SoundEffect explosion;
		SoundEffect teddyBounce;
		SoundEffect teddyShot;

		public Game1 ()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";	            

			// set resolution
			graphics.PreferredBackBufferWidth = GameConstants.WindowWidth;
			graphics.PreferredBackBufferHeight = GameConstants.WindowHeight;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
			RandomNumberGenerator.Initialize();
			IsMouseVisible = true;

			base.Initialize ();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch (GraphicsDevice);

			// load audio content

			// load sprite font

			// load projectile and explosion sprites
			frenchFriesSprite = Content.Load<Texture2D>(@"Graphics\frenchfries");
			teddyBearProjectileSprite = Content.Load<Texture2D>(@"Graphics\teddybearprojectile");

			// add burger
			int burgerX = GameConstants.WindowWidth / 2;
			int burgerY = GameConstants.WindowHeight - (GameConstants.WindowHeight / 8);
			burger = new Burger (Content, @"Graphics\burger", burgerX, burgerY, null);

			// add TeddyBear
			SpawnBear ();
			

			// set initial health and score strings
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
			#if !__IOS__
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
			    Keyboard.GetState ().IsKeyDown (Keys.Escape)) {
				Exit ();
			}
			#endif

			// get current KeyBoard state and update burger
			KeyboardState keyBoardState = Keyboard.GetState ();
			MouseState mouse = Mouse.GetState ();
			burger.Update (gameTime, mouse, keyBoardState);

			// update other game objects
			foreach (TeddyBear bear in bears)
			{
				bear.Update(gameTime);
			}
			foreach (Projectile projectile in projectiles)
			{
				projectile.Update(gameTime);
			}
			foreach (Explosion explosion in explosions)
			{
				explosion.Update(gameTime);
			}

			// check and resolve collisions between teddy bears

			// check and resolve collisions between burger and teddy bears

			// check and resolve collisions between burger and projectiles

			// check and resolve collisions between teddy bears and projectiles

			// clean out inactive teddy bears and add new ones as necessary

			// clean out inactive projectiles

			// clean out finished explosions

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.CornflowerBlue);

			spriteBatch.Begin();

			// draw game objects
			burger.Draw(spriteBatch);
			foreach (TeddyBear bear in bears)
			{
				bear.Draw(spriteBatch);
			}
			foreach (Projectile projectile in projectiles)
			{
				projectile.Draw(spriteBatch);
			}
			foreach (Explosion explosion in explosions)
			{
				explosion.Draw(spriteBatch);
			}

			// draw score and health

			spriteBatch.End();

			base.Draw(gameTime);
		}

		#region Public methods

		/// <summary>
		/// Gets the projectile sprite for the given projectile type
		/// </summary>
		/// <param name="type">the projectile type</param>
		/// <returns>the projectile sprite for the type</returns>
		public static Texture2D GetProjectileSprite(ProjectileType type)
		{
			// replace with code to return correct projectile sprite based on projectile type
			if (type.Equals (ProjectileType.FrenchFries)) 
			{
				return frenchFriesSprite;
			} 
			else 
			{
				return teddyBearProjectileSprite; 
			}
		}

		/// <summary>
		/// Adds the given projectile to the game
		/// </summary>
		/// <param name="projectile">the projectile to add</param>
		public static void AddProjectile(Projectile projectile)
		{
			projectiles.Add (projectile);
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Spawns a new teddy bear at a random location
		/// </summary>
		private void SpawnBear()
		{
			// generate random location
			int x = GetRandomLocation (GameConstants.SpawnBorderSize, 
				graphics.PreferredBackBufferWidth - 2 * GameConstants.SpawnBorderSize);
			
			int y = GetRandomLocation (GameConstants.SpawnBorderSize, 
				graphics.PreferredBackBufferHeight - 2 * GameConstants.SpawnBorderSize);

			// generate random speed for velocity 
			float speed = RandomNumberGenerator.NextFloat (GameConstants.BearSpeedRange) + GameConstants.MinBearSpeed;

			// generate random angle for velocity
			// get random degree in radions from 0 PI then convert to degrees 
			float angle = RandomNumberGenerator.NextFloat (2 * (float)Math.PI);

			// generate random velocity
			Vector2 velocity = new Vector2 ((float) (Math.Cos(angle) * speed), 
				(float) (Math.Sin(angle) * speed));

			// create new bear
			TeddyBear bear = new TeddyBear (Content, @"Graphics\teddybearprojectile", x, y, velocity, null, null);
			bears.Add (bear);

			// make sure we don't spawn into a collision
		}

		/// <summary>
		/// Gets a random location using the given min and range
		/// </summary>
		/// <param name="min">the minimum</param>
		/// <param name="range">the range</param>
		/// <returns>the random location</returns>
		private int GetRandomLocation(int min, int range)
		{
			return min + RandomNumberGenerator.Next(range);
		}

		/// <summary>
		/// Gets a list of collision rectangles for all the objects in the game world
		/// </summary>
		/// <returns>the list of collision rectangles</returns>
		private List<Rectangle> GetCollisionRectangles()
		{
			List<Rectangle> collisionRectangles = new List<Rectangle>();
			collisionRectangles.Add(burger.CollisionRectangle);
			foreach (TeddyBear bear in bears)
			{
				collisionRectangles.Add(bear.CollisionRectangle);
			}
			foreach (Projectile projectile in projectiles)
			{
				collisionRectangles.Add(projectile.CollisionRectangle);
			}
			foreach (Explosion explosion in explosions)
			{
				collisionRectangles.Add(explosion.CollisionRectangle);
			}
			return collisionRectangles;
		}

		/// <summary>
		/// Checks to see if the burger has just been killed
		/// </summary>
		private void CheckBurgerKill()
		{

		}

		#endregion
	}
}
