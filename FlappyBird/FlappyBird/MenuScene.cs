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
		private static float screenWidth;
		private static float screenHeight;
		
		private static TextureInfo _ti;
		private static Texture2D 	_texture;
		
		private static TextureInfo tiBackground;
		private static Texture2D   textureBackground;
		
		private static Rectangle backRect;
		private static TouchStatus touchStatus, lastTouchStatus;

		
		public MenuScene()
		{
			var screenSize = Director.Instance.GL.Context.GetViewport();
			screenWidth = screenSize.Width;
			screenHeight = screenSize.Height;
			
			Sce.PlayStation.HighLevel.UI.Scene scene = new Sce.PlayStation.HighLevel.UI.Scene();
			
			ImageBox back = new ImageBox();
			back.Image = new ImageAsset("/Application/textures/asteroid.png");
			back.ImageScaleType = ImageScaleType.AspectInside;
			back.Width = back.Image.Width;
			back.Height = back.Image.Height;
			back.X = 0;
			back.Y = screenSize.Height - back.Height;
			backRect = new Rectangle(back.X, back.Y, back.Width, back.Height);
			scene.RootWidget.AddChildLast(back);
			
			
			_texture = new Texture2D(screenSize.Width, screenSize.Height, false, PixelFormat.Rgba);
			_ti = new TextureInfo(_texture);

			textureBackground = new Texture2D("/Application/textures/bottompipe.png", false);
			tiBackground = new TextureInfo(textureBackground);
			
			SpriteUV background = new SpriteUV(tiBackground);
			background.Quad.S = _ti.TextureSizef;
			background.Position = new Vector2(0, 0);
			this.AddChild(background);//adds background

			UISystem.SetScene(scene);//, null);
			this.Camera.SetViewFromViewport();
			Scheduler.Instance.ScheduleUpdateForTarget(this,0,false);
			this.RegisterDisposeOnExitRecursive();
		}
		
		~MenuScene()
		{
	
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
					if(ButtonHit(xPos, yPos, backRect))
					{
						Touch.GetData(0).Clear();
						SceneManager.Instance.SendSceneToFront(new LevelSceneNew(), SceneManager.SceneTransitionType.SolidFade, 0.0f);
					}
					
					lastTouchStatus = touchStatus;
					Touch.GetData(0).Clear();
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


