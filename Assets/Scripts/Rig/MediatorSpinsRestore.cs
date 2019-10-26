using System;
using Model;
using Structure;

namespace Rig
{
	public class MediatorSpinsRestore : IMediatorRig
	{
		private TimeSpan _targetPeriod;
		private int _targetSpins;
		private TimeSpan _currentPeriod;
		private int _currentSpins;

		public void Update(ContainerApp container, ControllerScreen controller)
		{
			_targetPeriod = container.RestorePeriod;
			_targetSpins = container.SpinsToRestore;

			if(_currentSpins == _targetSpins && _currentPeriod == _targetPeriod)
			{
				return;
			}

			StartAnimationProcess(controller);
		}

		public void UpdateForce(ContainerApp container, ControllerScreen controller)
		{
			_targetPeriod = container.RestorePeriod;
			_targetSpins = container.SpinsToRestore;

			StartAnimationProcess(controller);
		}

		private void StartAnimationProcess(ControllerScreen controller)
		{
			// stub
			_currentPeriod = _targetPeriod;
			_currentSpins = _targetSpins;

			controller.TextSpinRestore.text = $"{_currentSpins} spins in: {_currentPeriod:hh\\:mm\\:ss}";
		}
	}
}
