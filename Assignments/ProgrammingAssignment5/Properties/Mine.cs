using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

namespace ProgrammingAssignment5
{
	/// <summary>
	/// Mine.
	/// </summary>
	public class Mine
	{
		#region Fields

		//drawing support 
		Texture2D sprite;
		Rectangle drawRectangle;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ProgrammingAssignment5.Mine"/> class.
		/// </summary>
		public Mine (Texture2D sprite, Vector2 location)
		{
			this.sprite  = sprite;

			drawRectangle = new Rectangle () {
				Width  = sprite.Width,
				Height = sprite.Height,
				X = (int) location.X - sprite.Width  / 2,
				Y = (int) location.Y - sprite.Height / 2
			};
		}

		#endregion

		#region Properties

		public Rectangle CollisionRectangle 
		{
			get { return drawRectangle; }
		}

		#endregion

		#region Public methods

		public override bool Equals(Object obj)
		{
			Mine mine = obj as  Mine;

			if (mine != null)
			{
				return this.drawRectangle.Equals (mine.CollisionRectangle);
			}
			else 
			{
				return false;
			}
		}

		/// <summary>
		/// Draw the specified spriteBatch.
		/// </summary>
		/// <param name="spriteBatch">Sprite batch.</param>
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw (sprite, drawRectangle, Color.White);
		}
		#endregion
	}
}

