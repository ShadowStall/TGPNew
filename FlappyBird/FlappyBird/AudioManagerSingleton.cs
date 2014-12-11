using System;
using Sce.PlayStation.Core.Audio;


namespace FlappyBird
{
	public class AudioManagerSingleton
	{
		private static AudioManagerSingleton instance;
		private AudioManagerSingleton(){}
		
		// Background Music 
		private BgmPlayer bgmPlayMainMenu;
		private Bgm bgmMainMenu;
		
		public static AudioManagerSingleton Instance
		{
		get {
	         	if (instance == null)
	         	{
	            	instance = new AudioManagerSingleton();
	         	}
	         	return instance;
	      	}
		}
		
	}
}

