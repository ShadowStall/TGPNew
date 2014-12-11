using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using System.Collections.Generic;

namespace FlappyBird
{
	public class Pickups
	{
		private SpriteUV lifeSprite;	
		private TextureInfo textureInfo;
		private bool active;
		public bool Active { get{return active;} set{active = value;} }
		private Bounds2 lifePickupBounds ;
		private Timer lifeTime;
		private Random randLife;
		private AudioManager audio;
		
		private SpriteUV rocketSprite;
		private TextureInfo rocketTextureInfo;
		private Bounds2 rocketPickupBounds;
		private Timer rocketTime;
		private Random randRocket;
		
		private Timer timeToGoLife;
		
		public Pickups (Scene scene)
		{
			//Life Pickup
			textureInfo  = new TextureInfo("/Application/textures/Pickups/LifePickup.png");
			lifeSprite = new SpriteUV(textureInfo);	
			lifeSprite.Quad.S = textureInfo.TextureSizef;
			lifeSprite.Scale = new Vector2(1.0f);
			lifePickupBounds = new Bounds2();
			lifeSprite.Position = new Vector2(-300.0f, -300.0f);// spawn the first ones off screen
			lifeTime =  new Timer();
			
			// rocket pickup 
			rocketTextureInfo = new TextureInfo("/Application/textures/Pickups/RocketPickup.png");
			rocketSprite = new SpriteUV(rocketTextureInfo);
			rocketSprite.Quad.S = rocketTextureInfo.TextureSizef;
			rocketSprite.Scale = new Vector2(0.8f);
			rocketPickupBounds = new Bounds2();
			rocketSprite.Position = new Vector2(-200f, -200f);
			rocketTime = new Timer();
			
			active = false;
			randLife = new Random();
			randRocket = new Random();
			audio = new AudioManager();
			scene.AddChild(rocketSprite);
			scene.AddChild(lifeSprite);
		}
		public Bounds2 GetLifePickupBounds()
		{
			lifeSprite.GetContentWorldBounds(ref lifePickupBounds);
			
			return this.lifePickupBounds;
		}
		public Bounds2 GetRocketPickupBounds()
		{
			rocketSprite.GetContentWorldBounds(ref rocketPickupBounds);
			
			return this.rocketPickupBounds;
		}
		public void CheckCollision_lifePickup(Bird bird, Scene scene)
		{
			if(GetLifePickupBounds().Overlaps(bird.GetBirdBounds()))
			{
				lifeSprite.Position = new Vector2(-300f, -300f);
				bird.AddToLifeCounter(10);
				audio.PlayLifePickup();
				//play life + sound
			}
		}
		public bool CheckCollision_rocketPickup(Bird bird, Scene scene)
		{
			if(GetRocketPickupBounds().Overlaps(bird.GetBirdBounds()))
			{
				rocketSprite.Position = new Vector2(-300f, -300f);
				
				// play sound
				return true;
			}
			return false;
		}
		public void Dispose()
		{
			textureInfo.Dispose();
		}
		public void UpdateLifePickup()
		{
			if(rocketSprite != null)
			{
				rocketSprite.Position = new Vector2(lifeSprite.Position.X - 1.5f, lifeSprite.Position.Y);
			}
		}
		public void UpdateRocketPickup()
		{
			if(lifeSprite != null)
			{
				lifeSprite.Position = new Vector2(lifeSprite.Position.X - 1.5f, lifeSprite.Position.Y);
			}
		}
		public void SpawnLife()
		{
			if (lifeTime.Seconds()>= 30.0)
			{
				lifeSprite.Position = new Vector2((float)randLife.Next(1, 930), (float)randLife.Next(1, 500));
				if(timeToGoLife != null)
				{
					timeToGoLife.Reset();
				}
				lifeTime.Reset();			//back to 0
				lifeTime = new Timer();		//Starts the timer again
				
			}
		}
		public void SpawnRockets()
		{
			if (rocketTime.Seconds() >= 20.5)
			{
				rocketSprite.Position = new Vector2((float)randRocket.Next(1, 930), (float)randRocket.Next(1, 500));
				rocketTime.Reset();			//back to 0
				rocketTime = new Timer();		//Starts the timer again 
			}
		}
		public float getX()
		{
			return lifeSprite.Position.X;
		}
		public float getY()
		{
			return lifeSprite.Position.Y;
		}
		
		
	}
}

