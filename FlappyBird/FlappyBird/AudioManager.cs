using System;
using Sce.PlayStation.Core.Audio;

namespace FlappyBird
{
	
	public class AudioManager
	{
		private static SoundPlayer winamp;// of soundPlayer or MediaPLayer 
		private static Sound soundMovement;// of the actual sound
	
		private static Bgm bgm;
		private static BgmPlayer bgmPlayer;
		public AudioManager ()
		{
			//set up fx
			soundMovement = new Sound("/Application/Audio/Lazer1.wav");//waw only
			winamp = soundMovement.CreatePlayer();
			//winamp.Loop = true;
			winamp.Volume = 10.0f;
			winamp.PlaybackRate = 2.0f;

			
			//setup bgm 
			bgm = new Bgm("/Application/Audio/BGLoop.mp3"); //mp3 only
			bgmPlayer = bgm.CreatePlayer();
			bgmPlayer.Volume = 10.0f;
			bgmPlayer.Loop = true;
			
		}
		public void PlayLazerSound()
		{
			winamp.Play();
		
		}
		public void StopLazerSound()
		{
			winamp.Stop();
			
		}
		public void PlayBackgroundSound()
		{
			bgmPlayer.Play();
		}
		public void PlayDestructionSound()
		{
			
		}

		
	}

}

