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
	public class OptionScene : Sce.PlayStation.HighLevel.GameEngine2D.Scene
	{
		private float screenWidth;
		private float screenHeight;
		
		private TextureInfo _ti;
		private Texture2D 	_texture;
		
		private TextureInfo tiBackground;
		private Texture2D   textureBackground;
		
		private Rectangle backRect;
		private TouchStatus touchStatus, lastTouchStatus;
		
		private Slider musicVolSlider, soundVolSlider, brightnessSlider, contrastSlider;
		
		public OptionScene ()
		{
			var screenSize = Director.Instance.GL.Context.GetViewport();
			screenWidth = screenSize.Width;
			screenHeight = screenSize.Height;
			
			Sce.PlayStation.HighLevel.UI.Scene scene = new Sce.PlayStation.HighLevel.UI.Scene();
			
			ImageBox back = new ImageBox();
			back.Image = new ImageAsset("/Application/textures/Back.jpg");
			back.ImageScaleType = ImageScaleType.AspectInside;
			back.Width = back.Image.Width;
			back.Height = back.Image.Height;
			back.X = 0;
			back.Y = screenSize.Height - back.Height;
			backRect = new Rectangle(back.X, back.Y, back.Width, back.Height);
			scene.RootWidget.AddChildLast(back);
			
			InitaliseSliders(scene);
			musicVolSlider.Value = GameManager.Instance.MusicVol * 100;
			soundVolSlider.Value = GameManager.Instance.SoundFXVol * 100;
			brightnessSlider.Value = GameManager.Instance.Brightness * 100;
			contrastSlider.Value = GameManager.Instance.Contrast * 100;
			
			_texture = new Texture2D(screenSize.Width, screenSize.Height, false, PixelFormat.Rgba);
			_ti = new TextureInfo(_texture);

			textureBackground = new Texture2D("/Application/textures/backgroundV2.png", false);
			tiBackground = new TextureInfo(textureBackground);
			
			SpriteUV background = new SpriteUV(tiBackground);
			background.Quad.S = _ti.TextureSizef;
			background.Position = new Vector2(0, 0);
			this.AddChild(background);
			
			UISystem.SetScene(scene);
			
			this.Camera.SetViewFromViewport();
			Scheduler.Instance.ScheduleUpdateForTarget(this,0,false);
			this.RegisterDisposeOnExitRecursive();
		}
		
		~OptionScene()
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
			
			UISystem.Update(touches);
			GameManager.Instance.MusicVol = musicVolSlider.Value/100;
			GameManager.Instance.SoundFXVol = soundVolSlider.Value/100;
			GameManager.Instance.Brightness = brightnessSlider.Value/100;
			GameManager.Instance.Contrast = contrastSlider.Value/100;
			
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
		
		private void InitaliseSliders(Sce.PlayStation.HighLevel.UI.Scene scene)
		{
			musicVolSlider = new Slider();
			musicVolSlider.SetPosition(screenWidth/10, 220);
            musicVolSlider.SetSize(350, 50);
            musicVolSlider.Anchors = Anchors.Height;
            musicVolSlider.Visible = true;
			musicVolSlider.MinValue = 0;
            musicVolSlider.MaxValue = 100;
            musicVolSlider.Value = 100;
            musicVolSlider.Step = 1;
			scene.RootWidget.AddChildLast(musicVolSlider);
			
			soundVolSlider = new Slider();
			soundVolSlider.SetPosition(screenWidth/10, 320);
            soundVolSlider.SetSize(350, 50);
            soundVolSlider.Anchors = Anchors.Height;
            soundVolSlider.Visible = true;
			soundVolSlider.MinValue = 0;
            soundVolSlider.MaxValue = 100;
            soundVolSlider.Value = 100;
            soundVolSlider.Step = 1;
			scene.RootWidget.AddChildLast(soundVolSlider);
		
			brightnessSlider = new Slider();
			brightnessSlider.SetPosition(screenWidth/10 * 5 + 20, 220);
            brightnessSlider.SetSize(362, 58);
            brightnessSlider.Anchors = Anchors.Height;
            brightnessSlider.Visible = true;
			brightnessSlider.MinValue = 0;
            brightnessSlider.MaxValue = 100;
            brightnessSlider.Value = 100;
            brightnessSlider.Step = 1;
			scene.RootWidget.AddChildLast(brightnessSlider);
			
			contrastSlider = new Slider();
			contrastSlider.SetPosition(screenWidth/10 * 5 + 20, 320);
            contrastSlider.SetSize(362, 58);
            contrastSlider.Anchors = Anchors.Height;
            contrastSlider.Visible = true;
			contrastSlider.MinValue = 0;
            contrastSlider.MaxValue = 100;
            contrastSlider.Value = 100;
            contrastSlider.Step = 1;
			scene.RootWidget.AddChildLast(contrastSlider);
		}
		
	}
}

