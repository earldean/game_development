using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

namespace ProgrammingAssignment5
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		// teddy support 
		private Texture2D teddySprite;
		private List<TeddyBear> teddys = new List<TeddyBear> ();
		private int teddySpawnTimer = 0;
		private int teddySpawnDelay;

		// mine support 
		private Texture2D mineSprite;
		private List<Mine> mines = new List<Mine> ();

		// explosion support 
		private Texture2D explosionSprite;
		private List<Explosion> explosions = new List<Explosion> ();

		// click processing
		private bool leftClickStarted   = false;
		private bool leftButtonReleased = true;

		// random number support 
		private Random rand = new Random ();

		public Game1 ()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";

			graphics.PreferredBackBufferWidth  = GameConstants.WindowWidth;
			graphics.PreferredBackBufferHeight = GameConstants.WindowHeight;

			this.IsMouseVisible = true;
			teddySpawnDelay = GetRandomTeddySpwanDelay ();
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
			// TODO: Add your initialization logic here        
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

			//TODO: use this.Content to load your game content here 
			mineSprite  = Content.Load<Texture2D> (@"Graphics\mine");
			teddySprite = Content.Load<Texture2D> (@"Graphics\teddybear");
			explosionSprite = Content.Load<Texture2D> (@"Graphics\explosion");
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
			#if !__IOS__ &&  !__TVOS__
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState ().IsKeyDown (Keys.Escape))
				Exit ();
			#endif

			teddySpawnTimer += gameTime.ElapsedGameTime.Milliseconds;

			if (teddySpawnTimer >= teddySpawnDelay)
			{
				teddySpawnDelay = GetRandomTeddySpwanDelay ();
				teddySpawnTimer = 0;
				SpawnTeddy ();
			}

			// update the teddys 
			foreach (TeddyBear teddy in teddys)
			{
				teddy.Update (gameTime);
			}
            
			// TODO: Add your update logic here
			MouseState mouse = Mouse.GetState ();

			// add mine to window if left mouse button is clicked
			if (mouse.LeftButton == ButtonState.Pressed && leftButtonReleased)
			{
				leftButtonReleased = false;
				leftClickStarted   = true;
			}
			else if (mouse.LeftButton == ButtonState.Released && leftClickStarted) 
			{
				leftButtonReleased = true;
				leftClickStarted   = false;

				// create location for a mine
				Vector2 location = new Vector2 
				{
					X = mouse.X,
					Y = mouse.Y
				};

				// add a mine to List of mines
				mines.Add (new Mine (mineSprite, location));
			}

			// check for collision of teddys and mines 
			this.TeddyCollision ();

			// if explosion has already been drawn the removes it from explosion list
			for (int i = 0; i < explosions.Count; i++)
			{
				if (explosions [i].IsDrawn) 
				{
					explosions.Remove (explosions [i]);
				}
			}
			base.Update (gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.CornflowerBlue);
            
			//TODO: Add your drawing code here
			spriteBatch.Begin ();

			// draw all the mines
			foreach (Mine mine in mines) 
			{
				mine.Draw (spriteBatch);
			}

			// draw all the teddys
			foreach (TeddyBear teddy in teddys)
			{
				teddy.Draw (spriteBatch);
			}

			// draw all theexplosions
			foreach (Explosion e in explosions)
			{
				e.Draw (spriteBatch);
			}
			spriteBatch.End ();
            
			base.Draw (gameTime);
		}

		/// <summary>
		/// Spawns a teddy in a random location 
		/// </summary>
		private void SpawnTeddy ()
		{
			// create padding for spawing teddy on border of window frame
			int xBuffer = GameConstants.WindowWidth - (teddySprite.Width + GameConstants.TeddyBorderBuffer)
			              + GameConstants.TeddyBorderBuffer;
			int yBuffer = GameConstants.WindowHeight - (teddySprite.Height + GameConstants.TeddyBorderBuffer)
			              + GameConstants.TeddyBorderBuffer;

			// get random x and y to spawn teddy in window
			int x = rand.Next (xBuffer);
			int y = rand.Next (yBuffer);

			teddys.Add (new TeddyBear (teddySprite, x, y));
		}

		private void spawnExplosion (int x, int y)
		{
			explosions.Add (new Explosion (explosionSprite, x, y));
		}

		private void TeddyCollision () 
		{
			for (int i = 0; i < teddys.Count; i++)
			{
				for (int j = 0; j < mines.Count; j++)
				{
					if (teddys[i].CollisionRectangle.Intersects(mines[j].CollisionRectangle))
					{
						spawnExplosion (teddys [i].X, teddys [i].Y);
						teddys.RemoveAt (i);
						mines.RemoveAt  (j);
						break;
					}
				}
			}
		}

		/// <summary>
		/// Gets the random teddy spwan delay.
		/// </summary>
		/// <returns>The random teddy spwan delay.</returns>
		private int GetRandomTeddySpwanDelay ()
		{
			return rand.Next (GameConstants.MaxTeddySapwnTime) 
				+ GameConstants.MinTeddySpawnTime;
		}
	}
}

