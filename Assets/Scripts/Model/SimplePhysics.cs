using Structure;
using UnityEngine;

namespace Model
{
	public class SimplePhysics : IPhysics
	{
		public void UpdateState(StatePhysics model)
		{
			model.Speed = Time.deltaTime * (model.Acceleration + model.Friction) + model.Speed;
			model.Acceleration = 0f;
		}
	}
}
