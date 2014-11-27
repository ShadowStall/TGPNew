using System;

namespace FlappyBird
{
	public class GameManager
	{
		private static GameManager instance;
		private bool  itemsOn;
		private float brightness;
		private float contrast;
		private float musicVolume;
		private float soundVolume;
		
		public static GameManager Instance
		{
			get
			{
				if(instance == null)
				{
					instance = new GameManager();
				}
				return instance;
			}
		}
		
		private GameManager()
		{
			itemsOn = true;
			brightness = 0.0f;
			contrast = 0.0f;
			musicVolume = 1.0f;
			soundVolume = 1.0f;
		}
		
		public bool ItemsOn
		{
			get{return itemsOn;}
			set{itemsOn=value;}
		}
		
		public float Brightness
		{
			get{return brightness;}
			set{
				if (value < 0)
					brightness = 0;
				else if (value > 1)
					brightness = 1;
				else
					brightness = value;
			}
		}
		
		public float Contrast
		{
			get{return contrast;}
			set{
				if (value < 0)
					contrast = 0;
				else if (value > 1)
					contrast = 1;
				else
					contrast = value;
			}
		}
		
		public float MusicVol
		{
			get{return musicVolume;}
			set{
				if (value < 0)
					musicVolume = 0;
				else if (value > 1)
					musicVolume = 1;
				else
					musicVolume = value;
			}
		}
		
		public float SoundFXVol
		{
			get{return soundVolume;}
			set{
				if (value < 0)
					soundVolume = 0;
				else if (value > 1)
					soundVolume = 1;
				else
					soundVolume = value;
			}
		}
	}
}

