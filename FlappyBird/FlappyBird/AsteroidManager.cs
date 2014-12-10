using System;
using System.Collections.Generic;
using System.Collections;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.UI;
// Handles the amount of asteroids on screen 
namespace FlappyBird
{
	public class AsteroidManager
	{
		
		private TimeManager timeManaged;
		private static Asteroid [] asteroidArray;
		private const int numberOfAsteroids = 5;
		private Random rand;
		private RandomNumberGenerator rand2;
		private RandomNumberGenerator rand3;
		private int[] asteroidYPos;
		private int[] asteroidXPos;
		public AsteroidManager (Sce.PlayStation.HighLevel.GameEngine2D.Scene scene)
		{
			timeManaged = new TimeManager(2.5);
			asteroidArray = new Asteroid[numberOfAsteroids];					//Allocating memory for numberOfAsteroids
			for(int i = 0; i < numberOfAsteroids; i++)
			{
				asteroidArray[i] = new Asteroid(scene); 		//initialise 50 
			}
			timeManaged.StartTimer();
			timeManaged.Debugg();
			SaveRandomYpos();
		}
		public void DestroyAsteroid()
		{
			// move off screen
			//asteroidArray[i].detonateAsteroid();
			for(int i = 0 ; i< numberOfAsteroids; i++)
			{
				if (asteroidArray[i].getAlive() == false)
				{
					asteroidArray[i].detonateAsteroid();
				}

			}
			
		}
		public void Update()
		{
			for(int i = 0; i< numberOfAsteroids; i++)
			{
				asteroidArray[i].Update();
			}
		}

		public void HandleSpawn()
		{
			if(timeManaged.HasIntervalPassed() )
			{
				for(int i = 0; i< numberOfAsteroids; i++)
					{
						SaveRandomYpos();
						SaveRandomXpos();
						
							asteroidArray[i].SpawnAsteroid(asteroidXPos[i],asteroidYPos[i]);
						
						
					}
				
			}
		}
		private void SaveRandomYpos()
		{		
			int screenHeight = 544;
			int a = screenHeight/numberOfAsteroids;
			asteroidYPos = new int[numberOfAsteroids];
			for (int i =0 ; i< numberOfAsteroids; i++)
			{
				asteroidYPos[i]=(a*(i+1));
			}
		}
		private void SaveRandomXpos()
		{
			asteroidXPos = new int[numberOfAsteroids];
			rand2 = new RandomNumberGenerator(100, numberOfAsteroids);
			ArrayList randomXPos = rand2.GiveMeRandomNumbers();
			for(int i = 0; i < numberOfAsteroids; i++)
			{
				asteroidXPos[i] = (int) randomXPos[i];
			}
			

		}
		public static Asteroid[] getAsteroidArray()
		{
			return asteroidArray;
		}
	}
}

