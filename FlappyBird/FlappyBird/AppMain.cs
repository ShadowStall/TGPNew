using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.UI;

namespace FlappyBird
{
	public class AppMain
	{
		public static void Main(string [] args)
		{
			Initialise();
			while(true)			///Game Loop
			{
				SystemEvents.CheckEvents();
				Director.Instance.Update();
				Director.Instance.Render();
				UISystem.Render();
				Director.Instance.GL.Context.SwapBuffers();
				Director.Instance.PostSwap();
			}
			Director.Terminate();
		}
		public static void Initialise()
		{
			Director.Initialize();
			UISystem.Initialize(Director.Instance.GL.Context);
			
			MenuScene scnMenu = new MenuScene();
			
			//LevelSceneNew scnMenu = new LevelSceneNew();
			
			Director.Instance.RunWithScene(scnMenu, true);
		}
	}
}

