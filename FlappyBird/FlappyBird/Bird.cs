using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace FlappyBird
{
	public class Bird
	{
		//Private variables.
		private SpriteUV 	sprite;
		private TextureInfo	textureInfo;
		private float		yPositionBeforePush;
		private bool			rise;
		private bool			alive;
		
		public bool Alive { get{return alive;} set{alive = value;} }
		
		//Accessors.
		//public SpriteUV Sprite { get{return sprite;} }
		
		//Public functions.
		public Bird (Scene scene)
		{
			textureInfo  = new TextureInfo("/Application/textures/player.png");
			
			sprite = new SpriteUV();
			sprite = new SpriteUV(textureInfo);	
			sprite.Quad.S = textureInfo.TextureSizef;
			sprite.Position = new Vector2(50.0f,Director.Instance.GL.Context.GetViewport().Height*0.5f);
			//sprite.Pivot 	= new Vector2(0.5f,0.5f);
			sprite.Scale = new Vector2(0.2f);
			alive = true;
			//Add to the current scene.
			scene.AddChild(sprite);
			
		}
		
		public void Dispose()
			
		{
			textureInfo.Dispose();
		}
		
		public void Update(float deltaTime)
		{			
			
		}	
		
		public void Tapped()
		{
			if(!rise)
			{
				rise = true;
				yPositionBeforePush = sprite.Position.Y;
			}
		}
		public void goUp()
		{
			sprite.Position =  new Vector2(sprite.Position.X, sprite.Position.Y +3f);
		}
		public void goDown()
		{
			sprite.Position =  new Vector2(sprite.Position.X, sprite.Position.Y- 3f);
		}
		public void goRight()
		{
			sprite.Position =  new Vector2(sprite.Position.X+ 3f, sprite.Position.Y);
		}
		public void goLeft()
		{
			sprite.Position =  new Vector2(sprite.Position.X- 3f, sprite.Position.Y +3f);
		}
		public float getX()
		{
			return sprite.Position.X;
		}
		public float getY()
		{
			return sprite.Position.Y;
		}
		public Vector2 getScale()
		{
			return sprite.Quad.S;
		}
	
		
	}
}

