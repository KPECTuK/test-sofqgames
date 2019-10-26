using Model;
using Structure;

namespace Rig
{
	public class MediatorSpinsAvailable : IMediatorRig
	{
		private int _targetAvailable;
		private int _currentAvailable;
		private int _targetTotal;
		private int _currentTotal;

		public void Update(ContainerApp container, ControllerScreen controller)
		{
			_targetAvailable = container.SpinsAvailable;
			_targetTotal = container.SpinsTotal;

			if(_currentAvailable == _targetAvailable && _targetTotal == _currentTotal)
			{
				return;
			}

			StartAnimationProcess(controller);
		}

		public void UpdateForce(ContainerApp container, ControllerScreen controller)
		{
			_targetAvailable = container.SpinsAvailable;
			_targetTotal = container.SpinsTotal;

			StartAnimationProcess(controller);
		}

		private void StartAnimationProcess(ControllerScreen controller)
		{
			// stub
			_currentAvailable = _targetAvailable;
			_currentTotal = _targetTotal;

			controller.TextSpinCount.text = $"{_currentAvailable} / {_currentTotal}";
			unchecked
			{
				controller.ProgressSpin.fillAmount = (float)_currentAvailable / _currentTotal;
			}
		}
	}
}
