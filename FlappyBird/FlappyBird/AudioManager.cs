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
				winamp.Volume = 10.0f;
				winamp.PlaybackRate = 2.0f;
			}
			winamp.Play();
		
		}
		public void Dispose()
		{
			if (bgmPlayerLvl1 != null)
			{
				bgmPlayerLvl1.Dispose();
			}
			if (winamp != null)
			{	
				winamp.Dispose();
			}
			if (shipHitSound!= null)
			{
				shipHitSound.Dispose();
			}
			if(bgmLvl1 != null)
			{
				bgmLvl1.Dispose();
			}
		}
		public void StopLazerSound()
		{
			winamp.Stop();
			
		}
		public void StopBackgroundMusic()
		{
			if (bgmPlayerLvl1 != null)
			{
				bgmPlayerLvl1.Stop();
			}
		}
		public void PlayBackgroundSound()
		{
			if (bgmLvl1 == null)
			{
				//setup bgm 
				bgmLvl1 = new Bgm("/Application/Audio/BGLoop.mp3"); //mp3 only
		
			}
			if(bgmPlayerLvl1 == null)
			{
				bgmPlayerLvl1 = bgmLvl1.CreatePlayer();
				bgmPlayerLvl1.Volume = 10.0f;
				bgmPlayerLvl1.Loop = true;
			}
			bgmPlayerLvl1.Play();
		}
		public void PlayShipHitSound()
		{
			if( winampHitShip == null)
			{
				//set up fx
				shipHitSound = new Sound("/Application/Audio/ShipHit.wav");//waw only
				winampHitShip = shipHitSound.CreatePlayer();
				winampHitShip.Volume = 10.0f;
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
				winampHitAsteroid.Volume = 10.0f;
				winampHitAsteroid.PlaybackRate = 2.0f;
			}
				winampHitAsteroid.Play();
		}	
		public void PlayShipDyingSound()
		{
			if( winampHitAsteroid == null)
			{
				//set up fx
				asteroidHitSound = new Sound("/Application/Audio/ShipDying.wav");//waw only
				winampHitAsteroid = asteroidHitSound.CreatePlayer();
				winampHitAsteroid.Volume = 10.0f;
				winampHitAsteroid.PlaybackRate = 2.0f;
			}
				winampHitAsteroid.Play();
		}
	}

}

