using Model;
using Structure;

namespace Rig
{
	public class MediatorBet : IMediatorRig
	{
		private int _target;
		private int _current;

		public void Update(ContainerApp container, ControllerScreen controller)
		{
			_target = container.Bet;

			if(_target == _current)
			{
				return;
			}

			StartAnimationProcess(controller);
		}

		public void UpdateForce(ContainerApp container, ControllerScreen controller)
		{
			_target = container.Bet;

			StartAnimationProcess(controller);
		}

		private void StartAnimationProcess(ControllerScreen controller)
		{
			// stub
			_current = _target;

			controller.TextBet.text = $"Bet x{_current}";
		}
	}
}
