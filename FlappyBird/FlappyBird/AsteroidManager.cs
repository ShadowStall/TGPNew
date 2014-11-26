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
		private int[] asteroidYPos;
		public AsteroidManager (Sce.PlayStation.HighLevel.GameEngine2D.Scene scene)
		{
			timeManaged = new TimeManager(3.5);
			asteroidArray = new Asteroid[numberOfAsteroids];					//Allocating memory for numberOfAsteroids
			for(int i = 0; i < numberOfAsteroids; i++)
			{
				asteroidArray[i] = new Asteroid(scene); 		//initialise 50 
			}
			timeManaged.StartTimer();
			timeManaged.Debugg();
			SaveRandomNumbers();
		}
		public void Update()
		{
			for(int i = 0; i< numberOfAsteroids; i++)
			{
				asteroidArray[i].Update();
			}
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
		public void HandleSpawn()
		{
			if(timeManaged.HasIntervalPassed())
			{
				for(int i = 0; i< numberOfAsteroids; i++)
					{
						SaveRandomNumbers();
						asteroidArray[i].SpawnAsteroid(asteroidYPos[i]);	
						asteroidArray[i].setAlive(true);
					}
			}
		}
		private void SaveRandomNumbers()
		{
			asteroidYPos = new int[numberOfAsteroids];
			rand2 = new RandomNumberGenerator(500, numberOfAsteroids);
			ArrayList randomYPos = rand2.GiveMeRandomNumbers();
			for(int i = 0; i < numberOfAsteroids; i++)
			{
				asteroidYPos[i] = (int) randomYPos[i];
			}
		}
		public static Asteroid[] getAsteroidArray()
		{
			return asteroidArray;
		}
	}
}

