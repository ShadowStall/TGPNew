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
	public class LevelSceneNew : Sce.PlayStation.HighLevel.GameEngine2D.Scene
	{	
		//private static Sce.PlayStation.HighLevel.GameEngine2D.Scene gameScene;
		private Sce.PlayStation.HighLevel.UI.Scene uiScene;
		private Sce.PlayStation.HighLevel.UI.Label rocketCountLabel;
		private Bird	player;
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
		
		public LevelSceneNew()
		{
			Initialize();
		}
		public void Initialize ()
		{
			
			//gameScene = new Sce.PlayStation.HighLevel.GameEngine2D.Scene();
			var screenSize = Director.Instance.GL.Context.GetViewport();
			
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
			
			panel.AddChildLast(rocketCountLabel);
			
			uiScene.RootWidget.AddChildLast(panel);
			
			UISystem.SetScene(uiScene);
			
			background = new Background(this);
			bulletList = new List<Bullet>();
			rocketList = new List<Rocket>();
			player = new Bird(this);
			
			_hudSymbolBullets = new Hud(this);
			asteroidManager = new AsteroidManager(this);
			
			this.Camera.SetViewFromViewport();
			Scheduler.Instance.ScheduleUpdateForTarget(this,0,false);
			this.RegisterDisposeOnExitRecursive();			
			
		}
		public void addContent()
		{	
		}
		
		public override void OnEnter()
		{
			base.OnEnter();// add stuff between end of transition and start of level
		}
		
		public override void Update(float dt)
		{
			base.Update(dt);
			PlayerControls();
			player.Update(0.0f);
			FireBullets(player);
			FireRocket(player);
			player.CheckCollision(AsteroidManager.getAsteroidArray());
			
			asteroidManager.HandleSpawnTest2();
			asteroidManager.Update();
			UpdateRockets();
			UpdateBullets();
			if(player.Alive)
			{
				background.Update(0.0f);				
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
			
	}
}
