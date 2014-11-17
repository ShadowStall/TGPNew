using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using System.Collections;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using System.Collections.Generic;

namespace FlappyBird
{
	//Test out at home the new random number generator 
	public class RandomNumberGenerator
	{
		ArrayList lstNumbers;
		
		public RandomNumberGenerator(int max, int NumberOfAsteroids)
        {
            // Create an ArrayList object that will hold the numbers
            lstNumbers = new ArrayList();
            // The Random class will be used to generate numbers
			
            Random rndNumber = new Random(DateTime.Now.Millisecond);

            // Generate a random number between 1 and the Max
            int number = rndNumber.Next(1, max + 1);
            // Add this first random number to the list
            lstNumbers.Add(number);
            // Set a count of numbers to 0 to start
            int count = 0;

            do // Repeatedly...
            {
                // ... generate a random number between 1 and the Max
				rndNumber = new Random(DateTime.Now.Millisecond);
                number = rndNumber.Next(1, max + 1) + 20;

                // If the newly generated number in not yet in the list...
                if (!lstNumbers.Contains(number))
                {
                    // ... add it
                    lstNumbers.Add(number);
					
					count++;
                }         
            } while (count < NumberOfAsteroids); // Do that again

            // Once the list is built, return it
        }
		public ArrayList GiveMeRandomNumbers()
		{
			return lstNumbers;
		}
    }
	
}

