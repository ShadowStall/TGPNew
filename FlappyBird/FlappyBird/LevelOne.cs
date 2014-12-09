using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.UI;

namespace FlappyBird
{
	public class LevelOne : Sce.PlayStation.HighLevel.GameEngine2D.Scene
	{	
		private Sce.PlayStation.HighLevel.UI.Scene uiScene;
		private Sce.PlayStation.HighLevel.UI.Label rocketCountLabel;
		private Bird player;
		private Background background;
		private int rocketAmount = 10;
		private bool TriangleDown = false;
		private bool CrossDown = false;
		
		//Handles projectiles
		private List <Bullet> bulletList;
		private List <Rocket> rocketList;
		//HUD
		private Hud _hudSymbolBullets;
		//Testing asteroid manager
		private AsteroidManager asteroidManager;
		//Audio Manager
		private AudioManager audio;
		//Life Counter
		private SpriteUV spriteLifeBack;
		private TextureInfo	textureInfoBackLife;
		
		private SpriteUV spriteLifeRed;
		private TextureInfo	textureInfoRedLife;
		//Score counter 
		private Sce.PlayStation.HighLevel.UI.Label ScoreCountLabel;
		private ScoreHelper score;
		private int Score = 0;
		
		public LevelOne()
		{
			Initialize();
		}
		public void Initialize ()
		{
			//gameScene = new Sce.PlayStation.HighLevel.GameEngine2D.Scene();
			var screenSize = Director.Instance.GL.Context.GetViewport();
			score = new ScoreHelper();
			Sce.PlayStation.HighLevel.UI.Scene scene = new Sce.PlayStation.HighLevel.UI.Scene();	
			audio = new AudioManager();
			audio.PlayBackgroundSound();
			
			uiScene = new Sce.PlayStation.HighLevel.UI.Scene();
			
			Panel panel  = new Panel();
			panel.Width  = Director.Instance.GL.Context.GetViewport().Width;
			panel.Height = Director.Instance.GL.Context.GetViewport().Height;
			
			rocketCountLabel = new Sce.PlayStation.HighLevel.UI.Label();
			rocketCountLabel.SetPosition(40f, 12f);
			rocketCountLabel.Text = rocketAmount.ToString();
			
			ScoreCountLabel = new Sce.PlayStation.HighLevel.UI.Label();
			ScoreCountLabel.SetPosition(825f, 0f);
			ScoreCountLabel.Text ="Score: "+ Score.ToString();
		
			
			panel.AddChildLast(rocketCountLabel);
			panel.AddChildLast(ScoreCountLabel);
			uiScene.RootWidget.AddChildLast(panel);
			
			UISystem.SetScene(uiScene);
			//Life Bar
			textureInfoBackLife  = new TextureInfo("/Application/textures/HUD/healthbar_background.png");
			spriteLifeBack = new SpriteUV();
			spriteLifeBack = new SpriteUV(textureInfoBackLife);	
			spriteLifeBack.Quad.S = textureInfoBackLife.TextureSizef;
			spriteLifeBack.Position = new Vector2(250f, 520f);
		
			//life bar red
			textureInfoRedLife  = new TextureInfo("/Application/textures/HUD/healthbarRed3.png");
			spriteLifeRed = new SpriteUV();
			spriteLifeRed = new SpriteUV(textureInfoRedLife);	
			spriteLifeRed.Quad.S = textureInfoBackLife.TextureSizef;
			spriteLifeRed.Position = new Vector2(250f, 520f);
		
			
			background = new Background(this);
			bulletList = new List<Bullet>();
			rocketList = new List<Rocket>();
			player = new Bird(this);
			
			_hudSymbolBullets = new Hud(this);
			asteroidManager = new AsteroidManager(this);
			
			this.Camera.SetViewFromViewport();
			Scheduler.Instance.ScheduleUpdateForTarget(this,0,false);
			this.RegisterDisposeOnExitRecursive();			
			this.AddChild(spriteLifeBack);
			this.AddChild(spriteLifeRed);
		}
		public void UpdateLife(int life)
		{	
			switch(life)
				{
					case 90: spriteLifeRed.Scale = new Vector2(0.9f,1.0f); break;
					case 80: spriteLifeRed.Scale = new Vector2(0.8f, 1.0f); break;
					case 70: spriteLifeRed.Scale = new Vector2(0.7f,1.0f); break;
					case 60: spriteLifeRed.Scale = new Vector2(0.6f,1.0f); break;
					case 50: spriteLifeRed.Scale = new Vector2(0.5f,1.0f); break;
					case 40: spriteLifeRed.Scale = new Vector2(0.4f,1.0f); break;
					case 30: spriteLifeRed.Scale = new Vector2(0.3f,1.0f); break;
					case 20: spriteLifeRed.Scale = new Vector2(0.2f,1.0f); break;
					case 10: spriteLifeRed.Scale = new Vector2(0.1f,1.0f); break;
			}
		}
		
		public override void OnEnter()
		{
			base.OnEnter();// add stuff between end of transition and start of level
		}
		
		public override void Update(float dt)
		{
			base.Update(dt);
			if(player.Alive == true)
			{
				UpdateLife(player.getlifeCounter());
				PlayerControls();
				player.Update(0.0f);
				FireBullets(player);
				FireRocket(player);
				player.CheckCollision(AsteroidManager.getAsteroidArray());
				asteroidManager.HandleSpawn();
				asteroidManager.Update();
				UpdateRockets();
				UpdateBullets();
				background.Update(0.0f);
			}
			if(player.Alive == false)
			{
				//Ship explodes
				//audio.StopBackgroundMusic();
				audio.PlayShipDyingSound();
				//play end game if died play something if finished levrel play something else
				SceneManager.Instance.SendSceneToFront(new GameoverScene(), SceneManager.SceneTransitionType.SolidFade, 0.0f);
			    audio.Dispose();
			}
		
		}
				
		public override void Draw()
		{
			base.Draw();
		}
		
		public void UpdateBullets()
		{
			for(int i=0; i<bulletList.Count; i++)
			{
				bulletList[i].Update();
			    bulletList[i].CheckCollision(AsteroidManager.getAsteroidArray(), this);	
				if(bulletList[i].getIncrement())
				{
					UpdateScoreAmount();
				}
				if(bulletList[i].getX() > Director.Instance.GL.Context.GetViewport().Width)
				{
					bulletList.RemoveAt(i);
				}
			}
		}
		public void UpdateRockets()
		{
			for(int i=0; i<rocketList.Count; i++)
			{	
				rocketList[i].Update();
		
				if(rocketList[i].getX() > Director.Instance.GL.Context.GetViewport().Width)
				{
					rocketList.RemoveAt(i);
				}
				
			}
		}
		public void PlayerControls()
		{
			var GamePadData = GamePad.GetData(0);
			if ((GamePadData.Buttons & GamePadButtons.Up ) != 0){player.goUp();}
			if ((GamePadData.Buttons & GamePadButtons.Down ) != 0){player.goDown();}
			if ((GamePadData.Buttons & GamePadButtons.Right ) != 0){player.goRight();}
			if ((GamePadData.Buttons & GamePadButtons.Left) != 0){player.goLeft();} 
		
		}
		public void FireBullets(Bird bird)
		{
			var GamePadData =  GamePad.GetData(0);
			if (TriangleDown == false && ((GamePadData.Buttons & GamePadButtons.Triangle) != 0))
			{	
				audio.PlayLazerSound();
				Bullet bullet = new Bullet(this);
				bulletList.Add(bullet);
				bullet.Fire(new Vector2(bird.getX()+160f, bird.getY()+50f));
				TriangleDown = true;
			}
			if (Input2.GamePad.GetData(0).Triangle.Release)
			{
				audio.StopLazerSound();
				
				TriangleDown = false;
			}
			
		}
		public void FireRocket(Bird bird)
		{
			var GamePadData =  GamePad.GetData(0);
			if (CrossDown == false && ((GamePadData.Buttons & GamePadButtons.Cross) != 0) && rocketAmount > 0)
			{
				Rocket rocket = new Rocket(this);
				rocketList.Add(rocket);
				rocket.Fire(new Vector2(bird.getX(), bird.getY()));
				CrossDown = true;
				UpdateRocketAmount();
			}
			if (Input2.GamePad.GetData(0).Cross.Release)
			{
				CrossDown = false;
			}
			
		}
		public void UpdateRocketAmount()
		{
			rocketAmount = rocketAmount - 1;
			rocketCountLabel.Text = rocketAmount.ToString();
		}
		public void UpdateScoreAmount()
		{
		    Score += 10;
			ScoreCountLabel.Text = "Score: "+ Score.ToString();
		}
			
	}
}
