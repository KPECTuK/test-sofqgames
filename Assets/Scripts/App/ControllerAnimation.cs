using System.Linq;
using Rig;
using Structure;

namespace App
{
	public class ControllerAnimation : IContext
	{
		private readonly IMediator[] _mediators;

		public ControllerAnimation(IContext context)
		{
			_mediators = new IMediator[]
			{
				new MediatorBarrels(context),
				new MediatorCoins(),
				new MediatorBet(),
				new MediatorSpinsAvailable(),
				new MediatorSpinsRestore(),
			};
		}

		public T Resolve<T>() where T : class
		{
			return _mediators.OfType<T>().FirstOrDefault();
		}
	}
}
