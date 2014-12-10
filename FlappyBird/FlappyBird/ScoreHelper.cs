using System;

namespace FlappyBird
{
	public class ScoreHelper
	{
		private int score = 0;
		public ScoreHelper (){}
		
		public void Modify(int score)
		{
			this.score += score;
		}
		public int getScore()
		{
			return this.score;
		}
	}
}

