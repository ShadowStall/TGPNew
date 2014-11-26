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
		
		public bool Active { get{return active;} set{active = value;} }
		private Bounds2 rocketBounds ;
		
		
		public Rocket (Scene scene)
		{
			textureInfo  = new TextureInfo("/Application/textures/rocket.png");
			sprite	 		= new SpriteUV();
			sprite 			= new SpriteUV(textureInfo);	
			sprite.Quad.S 	= textureInfo.TextureSizef;
			active = false;
			sprite.Scale = new Vector2(0.5f);
					//Collision Detection
			rocketBounds = new Bounds2();
			
			scene.AddChild(sprite);
		}
		public Bounds2 GetBounds()
		{
			sprite.GetContentWorldBounds(ref rocketBounds);
			
			return this.rocketBounds;
		}
			
		public void CheckCollision(Asteroid [] asteroidArray, Scene scene)
		{
			for(int i = 0; i<asteroidArray.Length; i++)
			{
				//Console.WriteLine(asteroidArray[i].GetBounds());
				if(GetBounds().Overlaps(asteroidArray[i].GetBounds()))
				{
					Console.WriteLine("Collision Asteroid");
					//Change asteroid sprite
					asteroidArray[i].detonateAsteroid();
					asteroidArray[i].setAlive(false);
					
				}
			}
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

