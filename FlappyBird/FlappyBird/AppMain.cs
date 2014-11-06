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
		private static Sce.PlayStation.HighLevel.GameEngine2D.Scene 	gameScene;
		private static Sce.PlayStation.HighLevel.UI.Scene 				uiScene;
		private static Sce.PlayStation.HighLevel.UI.Label				scoreLabel;
		
		private static Bird			bird;
		private static Background	background;
				
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
			bird.Dispose();
		
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
			scoreLabel = new Sce.PlayStation.HighLevel.UI.Label();
			scoreLabel.HorizontalAlignment = HorizontalAlignment.Center;
			scoreLabel.VerticalAlignment = VerticalAlignment.Top;
			scoreLabel.SetPosition(
				Director.Instance.GL.Context.GetViewport().Width/2 - scoreLabel.Width/2,
				Director.Instance.GL.Context.GetViewport().Height*0.1f - scoreLabel.Height/2);
			scoreLabel.Text = "0";
			panel.AddChildLast(scoreLabel);
			uiScene.RootWidget.AddChildLast(panel);
			UISystem.SetScene(uiScene);
			
			//Create the background.
			background = new Background(gameScene);
			
			//Create the flappy douche
			bird = new Bird(gameScene);
			

			Director.Instance.RunWithScene(gameScene, true);
		}
		
		public static void Update()
		{
			ControlCharacter();

			bird.Update(0.0f);
			
			if(bird.Alive)
			{
				//Move the background.
				background.Update(0.0f);				
			}
		}
		public static void ControlCharacter()
		{
			var GamePadData=  GamePad.GetData(0);
			if ((GamePadData.Buttons & GamePadButtons.Up ) != 0)
			{
				//check if button is pressed up 
				bird.goUp();
			}
			if ((GamePadData.Buttons & GamePadButtons.Down ) != 0)
			{
				//check if button is pressed up 
				bird.goDown();
			}
			if ((GamePadData.Buttons & GamePadButtons.Right ) != 0)
			{
				//check if button is pressed up 
				bird.goRight();
			}
			
			if ((GamePadData.Buttons & GamePadButtons.Left) != 0)
			{
				//check if button is pressed up 
				bird.goLeft();
			}
		}
		
	}
}
