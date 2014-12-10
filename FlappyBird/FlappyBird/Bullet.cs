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
		private bool increment = false;
		public bool Active { get{return active;} set{active = value;} }
		private Bounds2 bulletBounds ;

		
		public Bullet (Scene scene)
		{
			textureInfo  = new TextureInfo("/Application/textures/bullet2.png");
			sprite	 		= new SpriteUV();
			sprite 			= new SpriteUV(textureInfo);	
			sprite.Quad.S 	= textureInfo.TextureSizef;
			active = false;
			sprite.Scale = new Vector2(0.2f);
			//Collision Detection
			bulletBounds = new Bounds2();
			
			
			scene.AddChild(sprite);
		}
		public Bounds2 GetBounds()
		{
			sprite.GetContentWorldBounds(ref bulletBounds);
			
			return this.bulletBounds;
		}
		public void CheckCollision(Asteroid [] asteroidArray, Scene scene)
		{
			for(int i = 0; i<asteroidArray.Length; i++)
			{
				
				if(GetBounds().Overlaps(asteroidArray[i].GetBounds()))
				{
					asteroidArray[i].detonateAsteroid();
					asteroidArray[i].setAlive(false);
					increment = true;
					return;
				}
				else
				{
					increment = false;
				}
				
			}
		}
		public bool getIncrement()
		{
			return this.increment;
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

