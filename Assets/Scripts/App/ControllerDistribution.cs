using System;
using Model;
using Structure;

namespace Service
{
	public class ControllerDistribution : IServiceDistribution
	{
		private readonly IServiceDistribution[] _strategies;
		private int _indexCurrent;

		public ControllerDistribution(IContext context)
		{
			var slots = context.Resolve<ContainerApp>().SlotsTotal;
			_strategies = new IServiceDistribution[]
			{
				new ServiceDistributionSimple(slots),
				new ServiceDistributionWin(slots),
			};
		}

		public void Reset(IContext context)
		{
			_strategies[_indexCurrent].Reset(context);
		}

		public void Setup(IContext context, StatePhysics state)
		{
			_strategies[_indexCurrent].Setup(context, state);
		}

		public void SetStrategy<T>() where T : IServiceDistribution
		{
			_indexCurrent = Array.FindIndex(_strategies, _ => _ is T);
			_indexCurrent = _indexCurrent < 0 ? 0 : _indexCurrent;
		}
	}
}
