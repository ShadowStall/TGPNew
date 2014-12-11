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
	public class GameoverScene : Sce.PlayStation.HighLevel.GameEngine2D.Scene
	{ 
		private float screenWidth;
		private float screenHeight;
		
		private TextureInfo _ti;
		private Texture2D 	_texture;
		
		private TextureInfo tiBackground;
		private Texture2D   textureBackground;
		
		private Rectangle retryRect, menuRect;
		private TouchStatus touchStatus, lastTouchStatus;
		private Sce.PlayStation.HighLevel.UI.Label gameoverLabel;
		private AudioManager audio;
		
		public GameoverScene()
		{
			var screenSize = Director.Instance.GL.Context.GetViewport();
			screenWidth = screenSize.Width;
			screenHeight = screenSize.Height;
			
			Sce.PlayStation.HighLevel.UI.Scene scene = new Sce.PlayStation.HighLevel.UI.Scene();
			
			if (audio == null)
			{
				audio = new AudioManager();
			}
			audio.PlayGameOverMusic();
			
			gameoverLabel = new Sce.PlayStation.HighLevel.UI.Label();
			gameoverLabel.SetPosition(screenSize.Width/2 - 50, screenSize.Height/4);
			gameoverLabel.Text = "";
			scene.RootWidget.AddChildLast(gameoverLabel);
			
			ImageBox retry = new ImageBox();
			retry.Image = new ImageAsset("/Application/textures/Retry.png");
			retry.ImageScaleType = ImageScaleType.AspectInside;
			retry.Width = retry.Image.Width;
			retry.Height = retry.Image.Height;
			retry.X = screenSize.Width/2 - retry.Width/2;
			retry.Y = screenSize.Height/2;
			retryRect = new Rectangle(retry.X, retry.Y, retry.Width, retry.Height);
			scene.RootWidget.AddChildLast(retry);
			
			ImageBox menu = new ImageBox();
			menu.Image = new ImageAsset("/Application/textures/Menu.png");
			menu.ImageScaleType = ImageScaleType.AspectInside;
			menu.Width = menu.Image.Width;
			menu.Height = menu.Image.Height;
			menu.X = screenSize.Width/2 - menu.Width/2;
			menu.Y = screenSize.Height/2 + 50;
			menuRect = new Rectangle(menu.X, menu.Y, menu.Width, menu.Height);
			scene.RootWidget.AddChildLast(menu);
			
			_texture = new Texture2D(screenSize.Width, screenSize.Height, false, PixelFormat.Rgba);
			_ti = new TextureInfo(_texture);

			textureBackground = new Texture2D("/Application/textures/BackgroundGameOver.png", false);
			tiBackground = new TextureInfo(textureBackground);
			
			SpriteUV background = new SpriteUV(tiBackground);
			background.Quad.S = _ti.TextureSizef;
			background.Position = new Vector2(0, 0);
			this.AddChild(background);//adds background

			UISystem.SetScene(scene);
			this.Camera.SetViewFromViewport();
			Scheduler.Instance.ScheduleUpdateForTarget(this,0,false);
			this.RegisterDisposeOnExitRecursive();
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
					if(ButtonHit(xPos, yPos, retryRect))
					{
						audio.Dispose();
						Touch.GetData(0).Clear();
						SceneManager.Instance.SendSceneToFront(new LevelOne(), SceneManager.SceneTransitionType.SolidFade, 0.0f);
						
					}
					
					if(ButtonHit(xPos, yPos, menuRect))
					{
						audio.Dispose();
						Touch.GetData(0).Clear();
						SceneManager.Instance.SendSceneToFront(new MenuScene(), SceneManager.SceneTransitionType.SolidFade, 0.0f);
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


