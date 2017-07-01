using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

namespace ProgrammingAssignment5
{
	/// <summary>
	/// Teddy bear.
	/// </summary>
	public class TeddyBear
	{
		#region Fields

		Texture2D sprite;
		Rectangle drawRectangle;

		Vector2 velocity;

		Random rand = new Random ();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ProgrammingAssignment5.TeddyBear"/> class.
		/// </summary>
		/// <param name="sprite">Sprite.</param>
		/// <param name="location">Location.</param>
		public TeddyBear (Texture2D sprite, int x, int y)
		{
			this.sprite = sprite;

			drawRectangle = new Rectangle () 
			{
				Height = sprite.Height,
				Width = sprite.Width,
				X = x,
				Y = y
			};
			GetRandomTeddyVelocity ();
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the collision rectangle.
		/// </summary>
		/// <value>The collision rectangle.</value>
		public Rectangle CollisionRectangle 
		{
			get { return drawRectangle; }
		}

		/// <summary>
		/// Gets the x location of the teddy.
		/// </summary>
		/// <value>The x.</value>
		public int X
		{
			get { return drawRectangle.X; }
		}

		/// <summary>
		/// Gets the y location of.
		/// </summary>
		/// <value>The y.</value>
		public int Y
		{
			get { return drawRectangle.Y; }
		}

		#endregion

		#region Public methods 

		/// <summary>
		/// Update the TeddyBear.
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public void Update(GameTime gameTime)
		{  
			drawRectangle.X += (int) (velocity.X * gameTime.ElapsedGameTime.Milliseconds);
			drawRectangle.Y += (int) (velocity.Y * gameTime.ElapsedGameTime.Milliseconds);
			this.BounceLeftRight();
			this.BounceTopBottom();
		}

		/// <summary>
		/// Draw the TeddyBear.
		/// </summary>
		/// <param name="spriteBatch">Sprite batch.</param>
		public void Draw(SpriteBatch spriteBatch) 
		{
			spriteBatch.Draw (sprite, drawRectangle, Color.White);
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="ProgrammingAssignment5.TeddyBear"/>.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="ProgrammingAssignment5.TeddyBear"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
		/// <see cref="ProgrammingAssignment5.TeddyBear"/>; otherwise, <c>false</c>.</returns>
		public override bool Equals(Object obj)
		{
			TeddyBear other = obj as TeddyBear;

			if (other != null)
			{
				return this.drawRectangle.Equals (other.CollisionRectangle);
			}
			else 
			{
				return false;
			}
		}


		#endregion

		#region Private methods

		/// <summary>
		/// Gets the random teddy velocity.
		/// </summary>
		private void GetRandomTeddyVelocity () 
		{
			float xVelocity = (float)rand.NextDouble () - 0.5f;
			float yVelocity = (float)rand.NextDouble () - 0.5f;

			velocity = new Vector2 () 
			{
				X = xVelocity,
				Y = yVelocity
			};
		}

		/// <summary>
		/// Bounces the teddy off left or right border.
		/// </summary>
		private void BounceLeftRight () 
		{
			// change teddy direction if on border of window 
			if (drawRectangle.X <= 0) 
			{
				drawRectangle.X = 0;
				velocity.X *= -1;
			} 
			else if (drawRectangle.Right >= GameConstants.WindowWidth)
			{
				drawRectangle.X = GameConstants.WindowWidth - sprite.Width;
				velocity.X *= -1;
			}
		}

		/// <summary>
		/// Bounces the teddy off top or bottom border.
		/// </summary>
		private void BounceTopBottom () 
		{
			if (drawRectangle.Y <= 0)
			{
				drawRectangle.Y = 0;
				velocity.Y *= -1;
			} 
			else if (drawRectangle.Bottom >= GameConstants.WindowHeight) 
			{
				drawRectangle.Y = GameConstants.WindowHeight - sprite.Height;
				velocity.Y *= -1;
			}	
		}

		#endregion
	}


}

