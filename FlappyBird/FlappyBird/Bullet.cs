using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using System.Collections.Generic;

namespace FlappyBird
{
	public class Bullet
	{
		
		private SpriteUV sprite;
		
		private TextureInfo textureInfo;
		
		private bool active;
		
		public bool Active { get{return active;} set{active = value;} }
		
		
		
		public Bullet (Scene scene)
		{
			textureInfo  = new TextureInfo("/Application/textures/bullet.png");
			sprite	 		= new SpriteUV();
			sprite 			= new SpriteUV(textureInfo);	
			sprite.Quad.S 	= textureInfo.TextureSizef;
			active = false;
			sprite.Scale = new Vector2(0.2f);
			scene.AddChild(sprite);
		}

		public void Dispose()
		{
			textureInfo.Dispose();
		}
		public void Fire(Vector2 shipPosition)
		{
			active = true;
			sprite.Position = shipPosition;
			
		}
		public void Update()
		{
		//	if (active)
		//	{
		//		for(int i=0; i<50; i++)
		//		{
		//			sprite.Position = new Vector2(sprite.Position.X + 0.5f, sprite.Position.Y);
		//		}
		//	}
			if (active)
			{	
				sprite.Position = new Vector2(sprite.Position.X + 10.5f, sprite.Position.Y);
			}

		}
		public float getX()
		{
			return sprite.Position.X;
		}
		public float getY()
		{
			return sprite.Position.Y;
		}
	}
}

