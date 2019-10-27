using Model;
using Structure;

namespace Rig
{
	public class MediatorCoins : IMediator
	{
		private int _target;
		private int _current;

		public void Update(ContainerApp container, ControllerScreen controller)
		{
			_target = container.Coins;

			if(_target == _current)
			{
				return;
			}

			StartAnimationProcess(controller);
		}

		public void UpdateForce(ContainerApp container, ControllerScreen controller)
		{
			_target = container.Coins;

			StartAnimationProcess(controller);
		}

		private void StartAnimationProcess(ControllerScreen controller)
		{
			// stub
			_current = _target;

			controller.TextCoins.text = $"{_current}";
		}
	}
}
