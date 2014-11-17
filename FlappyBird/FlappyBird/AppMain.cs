using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.UI;
	
namespace FlappyBird
{
	public class AppMain
	{	
		private static Sce.PlayStation.HighLevel.GameEngine2D.Scene gameScene;
		private static Sce.PlayStation.HighLevel.UI.Scene uiScene;
		private static Sce.PlayStation.HighLevel.UI.Label rocketCountLabel;
		private static Bird	player;
		private static Background background;
		private static int rocketAmount = 10;
		private static bool TriangleDown = false;
		private static bool CrossDown = false;
		//Handles projectiles
		private static List <Bullet> bulletList;
		
		private static List <Rocket> rocketList;
		//HUD
		private static Hud _hudSymbolBullets;
		//Testing asteroid manager
		private static AsteroidManager asteroidManager;
		//Testing randomnumber generator
		
		
		public static void Main (string[] args)
		{
			Initialize();
			
			//Game loop
			bool quitGame = false;
			while (!quitGame) 
			{
				Update ();
				Director.Instance.Update();
				Director.Instance.Render();
				UISystem.Render();
				Director.Instance.GL.Context.SwapBuffers();
				Director.Instance.PostSwap();
			}
			
			//Clean up after ourselves.
			player.Dispose();
		
			background.Dispose();
			
			Director.Terminate ();
		}

		public static void Initialize ()
		{
			//Set up director and UISystem.
			Director.Initialize ();
			UISystem.Initialize(Director.Instance.GL.Context);
			
			//Set game scene
			gameScene = new Sce.PlayStation.HighLevel.GameEngine2D.Scene();
			gameScene.Camera.SetViewFromViewport();
			
			//Set the ui scene.
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
			
			//Create the background.
			background = new Background(gameScene);
			//Projectiles
			bulletList = new List<Bullet>();
			rocketList = new List<Rocket>();
			//Create the flappy douche
			player = new Bird(gameScene);
			_hudSymbolBullets = new Hud(gameScene);
		
			//asteroid manager
			asteroidManager = new AsteroidManager(gameScene);
			
			Director.Instance.RunWithScene(gameScene, true);
		}
		
		public static void Update()
		{
			
			PlayerControls();
			player.Update(0.0f);
			FireBullets(player);
			FireRocket(player);
			player.CheckCollision();
			//asteroidManager.HandleSpawnAsteroid();
			//SpawnAsteroids();
			//UpdateAsteroid();
			asteroidManager.HandleSpawnTest2();
			asteroidManager.Update();
			UpdateRockets();
			UpdateBullets();
			//Console.WriteLine(player.getX() + " " + player.getY());
			if(player.Alive)
			{
				background.Update(0.0f);				
			}
		}
		public static void UpdateBullets()
		{
			for(int i=0; i<bulletList.Count; i++)
			{
				bulletList[i].Update();
		
				if(bulletList[i].getX() > Director.Instance.GL.Context.GetViewport().Width)
				{
					bulletList.RemoveAt(i);
				}
			}
		}
		
		public static void UpdateRockets()
		{
			for(int i=0; i<rocketList.Count; i++)
			{	
				rocketList[i].Update();
		
				if(rocketList[i].getX() > Director.Instance.GL.Context.GetViewport().Width)
				{
					rocketList.RemoveAt(i);
					//rocketList[i].Dispose();
				}
				
			}
		}

		public static void CheckCollision()
		{
			
		}
		public static void PlayerControls()
		{
			var GamePadData = GamePad.GetData(0);
			if ((GamePadData.Buttons & GamePadButtons.Up ) != 0)
			{
				//check if button is pressed up 
				player.goUp();
			}
			if ((GamePadData.Buttons & GamePadButtons.Down ) != 0)
			{
				//check if button is pressed up 
				player.goDown();
			}
			if ((GamePadData.Buttons & GamePadButtons.Right ) != 0)
			{
				//check if button is pressed up 
				player.goRight();
			}
			if ((GamePadData.Buttons & GamePadButtons.Left) != 0)
			{
				//check if button is pressed up 
				player.goLeft();
			}
		
		}
		public static void FireBullets(Bird bird)
		{
			var GamePadData =  GamePad.GetData(0);
			if (TriangleDown == false && ((GamePadData.Buttons & GamePadButtons.Triangle) != 0))
			{	
				Bullet bullet = new Bullet(gameScene);
				bulletList.Add(bullet);
				bullet.Fire(new Vector2(bird.getX()+160f, bird.getY()+50f));
				TriangleDown = true;
			}
			if ( Input2.GamePad.GetData(0).Triangle.Release)
			{
				TriangleDown = false;
			}
		}


		public static void FireRocket(Bird bird)
		{
			var GamePadData =  GamePad.GetData(0);
			//sample code to use for when firing rockets
			if (CrossDown == false && ((GamePadData.Buttons & GamePadButtons.Cross) != 0) && rocketAmount > 0)
			{	
				Rocket rocket = new Rocket(gameScene);
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
		public static void UpdateRocketAmount()
		{
			rocketAmount = rocketAmount - 1;
			rocketCountLabel.Text = rocketAmount.ToString();
		}
			
	}
}
