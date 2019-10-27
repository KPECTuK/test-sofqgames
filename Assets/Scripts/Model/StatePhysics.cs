using System;

namespace Model
{
	public class StatePhysics
	{
		public float Acceleration;
		public float Friction;
		public float Speed;
		//
		public float RenderOffset;
		public int IndexCurrent;
		public int IndexTarget;
		public TimeSpan DelayPeriod;
		public DateTime StartStamp;
	}
}
