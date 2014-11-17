using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.UI;
namespace FlappyBird
{
	public class TimeManager
	{
		private static Timer time; 
		private double interval;
		public TimeManager (double interval)
		{
			this.interval = interval;
		}
		public void StartTimer()
		{
			time = new Timer();
		}
		public void ResetTimer()
		{
			time.Reset();
		}
		public bool HasIntervalPassed()				//returns true if interval amount of seconds has passed 
		{
			if(time.Seconds() >= interval)
			{
				ResetTimer();
				StartTimer();
				return true;
			}
				return false;
		}
		public void Debugg()
		{
			Console.WriteLine(time.Seconds());
		}
	}
}

