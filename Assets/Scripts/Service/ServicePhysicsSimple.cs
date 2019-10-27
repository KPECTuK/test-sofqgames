using System;
using Model;
using Structure;
using UnityEngine;

namespace Service
{
	public class ServicePhysicsSimple : IServicePhysics
	{
		private readonly int _slots;

		public ServicePhysicsSimple(int slots)
		{
			_slots = slots;
		}

		public void StateUpdate(StatePhysics model)
		{
			model.Speed = Time.deltaTime * (model.Acceleration + model.Friction) + model.Speed;
			model.Speed = model.Speed < 0f ? 0f : model.Speed;

			var isGoal =
				DateTime.UtcNow - model.StartStamp > model.DelayPeriod &&
				model.IndexTarget == model.IndexCurrent;

			if(isGoal)
			{
				model.Speed = 0f;
				model.RenderOffset = 0f;
			}
		}

		public void StateResetIdle(StatePhysics model)
		{
			model.Acceleration = 0f;
			model.Friction = -.02f;
			model.Speed = 0f;
			model.IndexCurrent = UnityEngine.Random.Range(0, _slots);
			model.IndexTarget = model.IndexCurrent;
		}

		public void StateResetRoll(StatePhysics model)
		{
			model.Friction = -.02f;
			model.Speed = Time.deltaTime * (14f + UnityEngine.Random.value * 2f + model.Friction);
		}
	}
}
