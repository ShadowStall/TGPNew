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
		
		private SoundPlayer winampFireAsteroid;
		private Sound rocketFiredSound;
		
		private SoundPlayer winampLifePickUP;
		private Sound lifePickupSound;
		
		//rocket Pickup Sound
		private SoundPlayer winampRocketPickUP;
		private Sound rocketPickupSound;
		
		//main menu sound 
		private BgmPlayer bgmPlayMainMenu;
		private Bgm bgmMainMenu;
		
		//Game over Sounds
		private BgmPlayer bgmPlayGameOverMusic;
		private Bgm bgmGameOver;
		
		
		public AudioManager (){}
		public void PlayMainMenuSound()
		{
			if(bgmMainMenu == null)
			{
				bgmMainMenu = new Bgm("/Application/Audio/MenuScreenBackgroundMusic.mp3");
			}
			if(bgmPlayMainMenu == null)
			{
				bgmPlayMainMenu = bgmMainMenu.CreatePlayer();
				bgmPlayMainMenu.Volume = GameManager.Instance.MusicVol;
				bgmPlayMainMenu.Loop = true;
			}
				bgmPlayMainMenu.Play();
		}
		public bool CanPlay()
		{
			if (bgmPlayMainMenu != null)
			{
				if( bgmPlayMainMenu.Status != BgmStatus.Playing)
				{
					return true;
				}
			}
			return false;
		}
		public void PlayRocketPickup()
		{
			if(winampRocketPickUP == null)
			{
				rocketPickupSound = new Sound("/Application/Audio/rocketPickupSound.wav");
				winampRocketPickUP = rocketPickupSound.CreatePlayer();
				winampRocketPickUP.Volume = GameManager.Instance.SoundFXVol;
				winampRocketPickUP.PlaybackRate = 2.0f;
			}
			winampRocketPickUP.Play();
		}
		public void PlayLifePickup()
		{
			if(winampLifePickUP== null)
			{
				lifePickupSound = new Sound ("/Application/Audio/collectLifePowerup.wav");
				winampLifePickUP = lifePickupSound.CreatePlayer();
				winampLifePickUP.Volume = GameManager.Instance.SoundFXVol;
				winampLifePickUP.PlaybackRate = 2.0f;
			}
			winampLifePickUP.Play();
		}
		public void PlayGameOverMusic()
		{
			if (bgmGameOver == null)
			{
				bgmGameOver = new Bgm("/Application/Audio/GameOver.mp3"); //mp3 only
			}
			if(bgmPlayGameOverMusic == null)
			{
				bgmPlayGameOverMusic = bgmGameOver.CreatePlayer();
				bgmPlayGameOverMusic.Volume = GameManager.Instance.MusicVol;
				bgmPlayGameOverMusic.Loop = true;
			}
			bgmPlayGameOverMusic.Play();
		}
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
		public void Dispose()
		{
			if(bgmGameOver != null)
			{
				bgmGameOver.Dispose();
			}
			if(bgmPlayGameOverMusic!= null)
			{
				bgmPlayGameOverMusic.Dispose();
			}
			if(bgmPlayMainMenu != null)
			{
				bgmPlayMainMenu.Dispose();
			}
			if(bgmMainMenu != null)
			{
				bgmMainMenu.Dispose();
			}
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
			if(winampFireAsteroid != null)
			{
				winampFireAsteroid.Dispose();
			}
			if(rocketFiredSound != null)
			{
				rocketFiredSound.Dispose();
			}
			if(rocketPickupSound != null)
			{
				rocketPickupSound.Dispose();
			}
			if(winampRocketPickUP != null)
			{
				winampRocketPickUP.Dispose();
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
				bgmLvl1 = new Bgm("/Application/Audio/LevelOneBackGround.mp3"); //mp3 only
		
			}
			if(bgmPlayerLvl1 == null)
			{
				bgmPlayerLvl1 = bgmLvl1.CreatePlayer();
				bgmPlayerLvl1.Volume = GameManager.Instance.MusicVol;
				bgmPlayerLvl1.Loop = true;
			}
			bgmPlayerLvl1.Play();
		}
		
		public void PlayRocketLaunchSound()
		{
			if( winampFireAsteroid == null)
			{
				//set up fx
				rocketFiredSound = new Sound("/Application/Audio/rocketLaunch.wav");//waw only
				winampFireAsteroid = rocketFiredSound.CreatePlayer();
				winampFireAsteroid.Volume = GameManager.Instance.SoundFXVol;
				winampFireAsteroid.PlaybackRate = 2.0f;
			}
				winampFireAsteroid.Play();
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
		public void PlayShipDyingSound()
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

