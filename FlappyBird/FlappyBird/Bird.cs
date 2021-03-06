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
		private float yPositionBeforePush;
		private bool rise;
		private bool alive;
		private Bounds2 birdBounds;
		public bool Alive { get{return alive;} set{alive = value;} }
		//Life 
		private float speed = 3.5f;
		private int lifeCounter = 100;
		private bool check = true;
		private TimeManager time = null; 
		private AudioManager audio;
		public Bird (Scene scene)
		{
			audio = new AudioManager();
			textureInfo  = new TextureInfo("/Application/textures/player2.png");
			sprite = new SpriteUV();
			
			sprite = new SpriteUV(textureInfo);	
			
			sprite.Quad.S = textureInfo.TextureSizef;
			
			sprite.Position = new Vector2(50.0f,Director.Instance.GL.Context.GetViewport().Height*0.5f);
			alive = true;
			scene.AddChild(sprite);
		}
		public Bounds2 GetBirdBounds()
		{
			sprite.GetContentWorldBounds(ref birdBounds);
			
			return this.birdBounds;
		}
		public int getlifeCounter()
		{
			return this.lifeCounter;
		}
		public void CheckCollision(Asteroid [] asteroidArray)
		{	
			for(int i = 0; i<asteroidArray.Length; i++)
			{
				if(GetBirdBounds().Overlaps(asteroidArray[i].GetBounds()))
				{
					lifeCounter -=10;
					check = false;
					audio.PlayShipHitSound();
					asteroidArray[i].detonateAsteroid();
				}
	
			}
			if(lifeCounter == 0)
			{
				alive = false;
			}
		}
		public void AddToLifeCounter(int howMuchtoAdd)
		{
			if (lifeCounter >= 10)
			{
				this.lifeCounter += howMuchtoAdd;
			}
		}
		public void RestartCheckCollision()
		{
			this.check = true;
		}
		public void Dispose()
		{
			textureInfo.Dispose();
		}		
		public void Update(float deltaTime)
		{			
			ScreenCollision();
			
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
			sprite.Position =  new Vector2(sprite.Position.X, sprite.Position.Y +speed);
		}
		public void goDown()
		{
			sprite.Position =  new Vector2(sprite.Position.X, sprite.Position.Y- speed);
		}
		public void goRight()
		{
			sprite.Position =  new Vector2(sprite.Position.X+ speed, sprite.Position.Y);
		}
		public void goLeft()
		{
			sprite.Position =  new Vector2(sprite.Position.X- speed, sprite.Position.Y);
		}
		public float getX()
		{
			return sprite.Position.X;
		}
		public float getY()
		{
			return sprite.Position.Y;
		}
		public void ScreenCollision ()
		{
			if(sprite.Position.X < -15 )
			{
				sprite.Position = new Vector2 (-15 , sprite.Position.Y);
			}
			if(sprite.Position.Y < -7 )
			{
				sprite.Position = new Vector2 (sprite.Position.X, -7);
			}
			if(sprite.Position.X + 200 >Director.Instance.GL.Context.GetViewport().Width)
			{
				sprite.Position = new Vector2(Director.Instance.GL.Context.GetViewport().Width-200, sprite.Position.Y);
			}
			if(sprite.Position.Y +113 > Director.Instance.GL.Context.GetViewport().Height)
			{
				sprite.Position = new Vector2(sprite.Position.X, Director.Instance.GL.Context.GetViewport().Height-113);
			}
	
		}
	}
}

