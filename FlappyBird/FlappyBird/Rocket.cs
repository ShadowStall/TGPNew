using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using System.Collections.Generic;
namespace FlappyBird
{
	public class Rocket
	{
		private SpriteUV sprite;
		
		private TextureInfo textureInfo;
		
		private bool active;
		private bool increment = false;
		public bool Active { get{return active;} set{active = value;} }
		private bool alive = true;
 		private SpriteUV explodingsprite;
		private Bounds2 rocketBounds;
		
		public Rocket (Scene scene)
		{
			textureInfo  = new TextureInfo("/Application/textures/rocket.png");
			sprite	 		= new SpriteUV();
			sprite 			= new SpriteUV(textureInfo);	
			sprite.Quad.S 	= textureInfo.TextureSizef;
			active = false;
			sprite.Scale = new Vector2(0.5f);
			rocketBounds = new Bounds2();
			scene.AddChild(sprite);
		}
		public bool getIncrement()
		{
			return this.increment;
		}
		public void Dispose()
		{
			textureInfo.Dispose();
		}
		public Bounds2 GetBounds()
		{
			sprite.GetContentWorldBounds(ref rocketBounds);
			
			return this.rocketBounds;
		}
		public void Fire(Vector2 shipPosition)
		{
			active = true;
			sprite.Position = shipPosition;
			// Audio for rocket launching
		}
		public void Update()
		{
			if (active)
			{	
				sprite.Position = new Vector2(sprite.Position.X + 10.5f, sprite.Position.Y);
			}
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
		public float getX()
		{
			return sprite.Position.X;
		}
		public float getY()
		{
			return sprite.Position.Y;
		}
		public void detonateAsteroid()
 		{
			sprite.Position = new Vector2(- 100, -100);

 		}
		public void setAlive(bool peanut)
		{
			this.alive = peanut;
		}
		public bool getAlive()
		{
			return this.alive;
		}
		
	}
}

