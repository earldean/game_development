using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProgrammingAssignment4
{
	/// <summary>
	/// A pickup
	/// </summary>
	public class Pickup
	{
		#region Fields

		// drawing support
		Texture2D sprite;
		Rectangle drawRectangle;
		Vector2 spriteCenter;

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="sprite">sprite for the pickup</param>
		/// <param name="location">location of the center of the pickup</param>
		public Pickup(Texture2D sprite, Vector2 location)
		{
			this.sprite = sprite;

			spriteCenter = new Vector2 () 
			{ 
				X = location.X - sprite.Width  / 2, 
				Y = location.Y - sprite.Height / 2 
			};

			// STUDENTS: set draw rectangle so pickup is centered on location
			drawRectangle = new Rectangle();

			drawRectangle.X = (int) spriteCenter.X;
			drawRectangle.Y = (int) spriteCenter.Y;

			drawRectangle.Width  = sprite.Width;
			drawRectangle.Height = sprite.Height;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the collision rectangle for the pickup
		/// </summary>
		public Rectangle CollisionRectangle
		{
			get { return drawRectangle; }
		}

		/// <summary>
		/// Gets the center location of sprite as a Vector2 
		/// </summary>
		/// <value>The center.</value>
		public Vector2 Center 
		{
			get { return spriteCenter; }
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Draws the pickup
		/// </summary>
		/// <param name="spriteBatch">sprite batch</param>
		public void Draw(SpriteBatch spriteBatch)
		{
			// STUDENTS: use the sprite batch to draw the pickup
			spriteBatch.Draw (sprite, drawRectangle, Color.White);
		}

		#endregion
	}
}
