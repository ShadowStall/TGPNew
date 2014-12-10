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
		
		private SpriteUV sprite;
		
		private TextureInfo textureInfo;
		
		private bool active;
		public bool Active { get{return active;} set{active = value;} }
		private Bounds2 pickupBounds ;
		private Timer time;
		private Random rand;
		private AudioManager audio;
		public Pickups (Scene scene)
		{
			time =  new Timer();
			textureInfo  = new TextureInfo("/Application/textures/Pickups/LifePickup.png");
			sprite	 		= new SpriteUV();
			sprite 			= new SpriteUV(textureInfo);	
			sprite.Quad.S 	= textureInfo.TextureSizef;
			active = false;
			sprite.Scale = new Vector2(1.0f);
			pickupBounds = new Bounds2();
			sprite.Position = new Vector2(-300.0f, -300.0f);// spawn the first ones off screen
			rand = new Random();
			audio = new AudioManager();
			scene.AddChild(sprite);
		}
		public Bounds2 GetBounds()
		{
			sprite.GetContentWorldBounds(ref pickupBounds);
			
			return this.pickupBounds;
		}
		public void CheckCollision(Bird bird, Scene scene)
		{
			if(GetBounds().Overlaps(bird.GetBirdBounds()))
			{
				sprite.Position = new Vector2(-300f, -300f);
				bird.AddToLifeCounter(10);
				audio.PlayLifePickup();
				//play life + sound
			}
		
		}
		public void Dispose()
		{
			textureInfo.Dispose();
		}
		public void Update()
		{
			if(sprite != null)
			{
				sprite.Position = new Vector2(sprite.Position.X - 1.5f, sprite.Position.Y);
			}
		}
		public void SpawnLife()
		{
			if (time.Seconds()>= 5.0)
			{
				sprite.Position = new Vector2((float)rand.Next(1, 930), (float)rand.Next(1, 500));
				time.Reset();			//back to 0
				time = new Timer();		//Starts the timer again 
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

