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
		private Asteroid [] asteroidArray;
		private const int numberOfAsteroids = 5;
		private Random rand;
		private RandomNumberGenerator rand2;
		private int[] asteroidYPos;
		//
	
		public AsteroidManager (Sce.PlayStation.HighLevel.GameEngine2D.Scene scene)
		{
			timeManaged = new TimeManager(2.5);
			
			asteroidArray = new Asteroid[numberOfAsteroids];					//Allocating memory for 50
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

		public void HandleSpawnTest2()
		{
			
				if(timeManaged.HasIntervalPassed())
				{
					for(int i = 0; i< numberOfAsteroids; i++)
					{
						SaveRandomNumbers();
						asteroidArray[i].SpawnAsteroid(asteroidYPos[i]);	
						//asteroidArray[i].Update();
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
		public Asteroid[] getAsteroidArray()
		{
			return asteroidArray;
		}
	}
}

