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
		private Sce.PlayStation.HighLevel.UI.Label optionsLabel, brightnessLabel, musicLabel, soundFXLabel;
		
		private Slider musicVolSlider, soundVolSlider, brightnessSlider;
		
		public OptionScene ()
		{
			var screenSize = Director.Instance.GL.Context.GetViewport();
			screenWidth = screenSize.Width;
			screenHeight = screenSize.Height;
			
			Sce.PlayStation.HighLevel.UI.Scene scene = new Sce.PlayStation.HighLevel.UI.Scene();
			
			optionsLabel = new Sce.PlayStation.HighLevel.UI.Label();
			optionsLabel.SetPosition(screenSize.Width/2 - 50, screenSize.Height/4 - 50);
			optionsLabel.Text = "Options";
			scene.RootWidget.AddChildLast(optionsLabel);
			
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
			musicVolSlider.SetPosition(screenWidth/2 - 175, 220);
            musicVolSlider.SetSize(350, 50);
            musicVolSlider.Anchors = Anchors.Height;
            musicVolSlider.Visible = true;
			musicVolSlider.MinValue = 0;
            musicVolSlider.MaxValue = 100;
            musicVolSlider.Value = 100;
            musicVolSlider.Step = 1;
			scene.RootWidget.AddChildLast(musicVolSlider);
			
			musicLabel = new Sce.PlayStation.HighLevel.UI.Label();
			musicLabel.SetPosition(musicVolSlider.X + musicVolSlider.Width/4 , musicVolSlider.Y - 30);
			musicLabel.Text = "Music Volume";
			scene.RootWidget.AddChildLast(musicLabel);
			
			soundVolSlider = new Slider();
			soundVolSlider.SetPosition(screenWidth/2 - 175, 320);
            soundVolSlider.SetSize(350, 50);
            soundVolSlider.Anchors = Anchors.Height;
            soundVolSlider.Visible = true;
			soundVolSlider.MinValue = 0;
            soundVolSlider.MaxValue = 100;
            soundVolSlider.Value = 100;
            soundVolSlider.Step = 1;
			scene.RootWidget.AddChildLast(soundVolSlider);
			
			soundFXLabel = new Sce.PlayStation.HighLevel.UI.Label();
			soundFXLabel.SetPosition(soundVolSlider.X + soundVolSlider.Width/4 , soundVolSlider.Y - 30);
			soundFXLabel.Text = "SoundFX Volume";
			scene.RootWidget.AddChildLast(soundFXLabel);
		}
	}
}

