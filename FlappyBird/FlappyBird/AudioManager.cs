using System;
using Sce.PlayStation.Core.Audio;

namespace FlappyBird
{
	
	public class AudioManager
	{
		private SoundPlayer winamp;// of soundPlayer or MediaPLayer 
		private Sound LazerSound;// of the actual sound

		private Bgm bgmLvl1;
		private BgmPlayer bgmPlayerLvl1;
		
	
		private SoundPlayer winampHitShip;// of soundPlayer or MediaPLayer 
		private Sound shipHitSound;// of the actual sound
		
		
		private SoundPlayer winampHitAsteroid;// of soundPlayer or MediaPLayer 
		private Sound asteroidHitSound;// of the actual sound
		
		public AudioManager (){}
		public void PlayLazerSound()
		{
			if(winamp == null)
			{
				//set up fx
				LazerSound = new Sound("/Application/Audio/Lazer1.wav");//waw only
				winamp = LazerSound.CreatePlayer();
				winamp.Volume = GameManager.Instance.SoundFXVol;
				winamp.PlaybackRate = 2.0f;
			}
			winamp.Play();
		
		}
		public void StopLazerSound()
		{
			winamp.Stop();
			
		}
		
		public void PlayBackgroundSound()
		{
			if (bgmPlayerLvl1 == null)
			{
				//setup bgm 
				bgmLvl1 = new Bgm("/Application/Audio/BGLoop.mp3"); //mp3 only
				bgmPlayerLvl1 = bgmLvl1.CreatePlayer();
				bgmPlayerLvl1.Volume = GameManager.Instance.MusicVol;
				bgmPlayerLvl1.Loop = true;
			}
			bgmPlayerLvl1.Play();
		}
		
		public void StopBackgroundMusic()
		{
			bgmPlayerLvl1.Dispose();
		}
		
		public void PlayShipHitSound()
		{
			if( winampHitShip == null)
			{
				//set up fx
				shipHitSound = new Sound("/Application/Audio/ShipHit.wav");//waw only
				winampHitShip = shipHitSound.CreatePlayer();
				winampHitShip.Volume = GameManager.Instance.SoundFXVol;
				winampHitShip.PlaybackRate = 2.0f;
			}
				winampHitShip.Play();
		}
		public void PlayAsteroidHitSound()
		{
			if( winampHitAsteroid == null)
			{
				//set up fx
				asteroidHitSound = new Sound("/Application/Audio/AsteroidHit.wav");//waw only
				winampHitAsteroid = asteroidHitSound.CreatePlayer();
				winampHitAsteroid.Volume = GameManager.Instance.SoundFXVol;
				winampHitAsteroid.PlaybackRate = 2.0f;
			}
				winampHitAsteroid.Play();
		}	
		public void PlayShitDyingSound()
		{
			if( winampHitAsteroid == null)
			{
				//set up fx
				asteroidHitSound = new Sound("/Application/Audio/ShipDying.wav");//waw only
				winampHitAsteroid = asteroidHitSound.CreatePlayer();
				winampHitAsteroid.Volume = GameManager.Instance.SoundFXVol;
				winampHitAsteroid.PlaybackRate = 2.0f;
			}
				winampHitAsteroid.Play();
		}
	}

}

