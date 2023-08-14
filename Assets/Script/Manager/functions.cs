using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace functions 
{ 
	public class FMath
	{
		public static float percentageToNum(float max, float per) => max * (per / 100f);
		public static float numToPercentage(float max, float num)
		{
			if(max == 0) return 0;
			return (num / max) * 100f;
		}
	}	
}
