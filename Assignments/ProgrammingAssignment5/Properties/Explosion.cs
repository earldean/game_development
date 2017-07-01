using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProgrammingAssignment5
{
	public class Explosion
	{
		#region Fields

		Texture2D sprite;
		Rectangle drawRectangle;

		bool isDrawn = false;

		#endregion

		#region Constructors
	
		/// <summary>
		/// Initializes a new instance of the <see cref="ProgrammingAssignment5.Explosion"/> class.
		/// </summary>
		/// <param name="sprite">Sprite.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public Explosion (Texture2D sprite, int x, int y)
		{
			this.sprite = sprite;

			drawRectangle = new Rectangle () 
			{
				Width  = sprite.Width,
				Height = sprite.Height,
				X = x,
				Y = y
			};
		}

		#region Properties

		public bool IsDrawn
		{
			get { return isDrawn; }
		}

		#endregion

		#endregion

		#region Public methods

		/// <summary>
		/// Draw the specified spriteBatch.
		/// </summary>
		/// <param name="spriteBatch">Sprite batch.</param>
		public void Draw(SpriteBatch spriteBatch) 
		{
			spriteBatch.Draw (sprite, drawRectangle, Color.White);
			isDrawn = true;
		}

		#endregion
	}
}

