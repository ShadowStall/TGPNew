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
	public class MenuScene : Sce.PlayStation.HighLevel.GameEngine2D.Scene
	{ 
		private float screenWidth;
		private float screenHeight;
		
		private TextureInfo _ti;
		private Texture2D 	_texture;
		
		private TextureInfo tiBackground;
		private Texture2D   textureBackground;
		
		private Rectangle startRect, optionRect;
		private TouchStatus touchStatus, lastTouchStatus;
		
		private AudioManager audio;
		
		public MenuScene()
		{
			if (audio == null)
			{
				audio = new AudioManager();
			}
			var screenSize = Director.Instance.GL.Context.GetViewport();
			screenWidth = screenSize.Width;
			screenHeight = screenSize.Height;
			
			Sce.PlayStation.HighLevel.UI.Scene scene = new Sce.PlayStation.HighLevel.UI.Scene();
			
			ImageBox start = new ImageBox();
			start.Image = new ImageAsset("/Application/textures/Start.png");
			start.ImageScaleType = ImageScaleType.AspectInside;
			start.Width = start.Image.Width;
			start.Height = start.Image.Height;
			start.X = screenSize.Width/2 - start.Width/2;
			start.Y = screenSize.Height/2;
			startRect = new Rectangle(start.X, start.Y, start.Width, start.Height);
			scene.RootWidget.AddChildLast(start);
			
			ImageBox option = new ImageBox();
			option.Image = new ImageAsset("/Application/textures/Option.png");
			option.ImageScaleType = ImageScaleType.AspectInside;
			option.Width = option.Image.Width;
			option.Height = option.Image.Height;
			option.X = screenSize.Width/2 - option.Width/2;
			option.Y = screenSize.Height/2 + 50;
			optionRect = new Rectangle(option.X, option.Y, option.Width, option.Height);
			scene.RootWidget.AddChildLast(option);
			
			_texture = new Texture2D(screenSize.Width, screenSize.Height, false, PixelFormat.Rgba);
			_ti = new TextureInfo(_texture);

			textureBackground = new Texture2D("/Application/textures/BackgroundGameMenu.png", false);
			tiBackground = new TextureInfo(textureBackground);
			
			SpriteUV background = new SpriteUV(tiBackground);
			background.Quad.S = _ti.TextureSizef;
			background.Position = new Vector2(0, 0);
			this.AddChild(background);//adds background

			UISystem.SetScene(scene);
			this.Camera.SetViewFromViewport();
			Scheduler.Instance.ScheduleUpdateForTarget(this,0,false);
			this.RegisterDisposeOnExitRecursive();
						// the idea is that if it's already playing do not try to play it again 
			
				audio.PlayMainMenuSound();
			
			
		}
		public override void OnEnter()
		{
			base.OnEnter();
		}
		
		public override void Update(float dt)
		{
			
			base.Update(dt);
			var gamePadData = GamePad.GetData(0);
			List<TouchData> touches = Touch.GetData(0);
			foreach(TouchData data in touches)
			{
				touchStatus = data.Status;
				float xPos = (data.X + 0.5f) * screenWidth;
				float yPos = (data.Y + 0.5f) * screenHeight;
				
				if(data.Status  == TouchStatus.Down)
				{
					if(ButtonHit(xPos, yPos, startRect))		//Level One
					{
						audio.Dispose();
						Touch.GetData(0).Clear();
						SceneManager.Instance.SendSceneToFront(new LevelOne(), SceneManager.SceneTransitionType.SolidFade, 0.0f);
					}
					
					if(ButtonHit(xPos, yPos, optionRect)) 		//Options Menu
					{
						Touch.GetData(0).Clear();
						SceneManager.Instance.SendSceneToFront(new OptionScene(), SceneManager.SceneTransitionType.SolidFade, 0.0f);
						audio.Dispose();
					}
					
					lastTouchStatus = touchStatus;
				}
			}
		}
		
		public override void Draw()
		{
			base.Draw();
		}
		
		private static bool ButtonHit(float pixelX, float pixelY, Rectangle button)
		{
			if(button.X <= pixelX && button.X + button.Width >= pixelX &&
			   button.Y <= pixelY && button.Y + button.Height >= pixelY)
			{
				return true;
			}
			return false;
				
		}
	}
}


