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
            lstNumbers = new ArrayList();
            Random rndNumber = new Random(DateTime.Now.Millisecond);
            int number = rndNumber.Next(1, max + 1);
            lstNumbers.Add(number);
            int count = 0;

            do 
            {
				rndNumber = new Random(DateTime.Now.Millisecond);
                number = rndNumber.Next(1, max + 1) + 20;

                if (!lstNumbers.Contains(number))
                {
                    lstNumbers.Add(number);	
					count++;
                }         
            } while (count < NumberOfAsteroids); 
        }
		public ArrayList GiveMeRandomNumbers()
		{
			return lstNumbers;
		}
    }
	
}

