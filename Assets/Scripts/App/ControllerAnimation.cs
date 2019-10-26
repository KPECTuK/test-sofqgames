using System.Linq;
using Rig;
using Structure;

namespace App
{
	public class ControllerAnimation : IContext
	{
		private readonly IMediatorRig[] _mediators;

		public ControllerAnimation()
		{
			_mediators = new IMediatorRig[]
			{
				new MediatorCoins(),
				new MediatorBet(),
				new MediatorSpinsAvailable(),
				new MediatorSpinsRestore(),
				new MediatorBarrels(),
			};
		}

		public T Resolve<T>() where T : class
		{
			return _mediators.OfType<T>().FirstOrDefault();
		}
	}
}
