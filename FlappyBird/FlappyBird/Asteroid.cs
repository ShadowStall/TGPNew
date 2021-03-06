using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using System.Collections.Generic;

namespace FlappyBird
{

	public class Asteroid
	{
			
		private SpriteUV sprite;
		private TextureInfo textureInfo = new TextureInfo("/Application/textures/newImprovedAsteroid.png");
	    private TextureInfo explodeInfo = new TextureInfo("/Application/textures/explosion.png");
		private bool disposeOf = false;
		private Random rand; 
		private float movementSpeed;
		private bool onScreen = false;
		private int lowY, highY;
		private int asteroidAmount;
		private Bounds2 asteroidbounds;
		private bool alive = true;
		private AudioManager audio;
		public Asteroid (Scene scene)
		{
			audio = new AudioManager();
			//sprite	 		= new SpriteUV();
			sprite 			= new SpriteUV(textureInfo);	
			sprite.Quad.S 	= textureInfo.TextureSizef;
			movementSpeed = 8.5f;
			lowY = 10;
			highY = 500;
			asteroidAmount = 10;
			sprite.Position = new Vector2(-300.0f, -300.0f);// spawn the first ones off screen
			scene.AddChild(sprite);
		}
		public void setAlive(bool peanut)
		{
			this.alive = peanut;
		}
		public bool getAlive()
		{
			return this.alive;
		}
		public Bounds2 GetBounds()
		{
			sprite.GetContentWorldBounds(ref asteroidbounds);
			return this.asteroidbounds;
		}
		public void Dispose()
		{
			textureInfo.Dispose();
		}
		public void Update()
		{
			sprite.Position = new Vector2(sprite.Position.X - movementSpeed, sprite.Position.Y);
			//wsprite.Rotate(0.2f);
		}
		public float getX()
		{
			return sprite.Position.X;
		}
		public float getY()
		{
			return sprite.Position.Y;
		}
		public bool GetDisposeOf()
		{
			return this.disposeOf;
		}
		public bool SetDisposeOf(bool value)
		{
			return this.disposeOf = value;
		}
		public void SpawnAsteroid(int x, int y)
		{	
			sprite.Position = new Vector2(x+950, y);
			setAlive(true);
		}
		public void setOnScreen(bool ok)
		{
			this.onScreen = ok;
		}
		public bool getOnScreen()
		{
			return this.onScreen;
		}
		public bool isVisible()
		{
			if(sprite.Position.X < 0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		public void detonateAsteroid()
		{
			audio.PlayAsteroidHitSound();
			sprite.Position = new Vector2(- 100, -100);
			setAlive(false);
		}
	}
}

