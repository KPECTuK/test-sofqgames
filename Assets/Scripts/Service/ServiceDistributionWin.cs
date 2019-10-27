using System;
using Model;
using Structure;
using Utility;

namespace Service
{
	public class ServiceDistributionWin : IServiceDistribution
	{
		private readonly int _slots;
		private TimeSpan _lastPeriod;
		private int _indexTarget;

		public ServiceDistributionWin(int slots)
		{
			_slots = slots;
		}

		public void Reset(IContext context)
		{
			var data = context.Resolve<ContainerApp>();
			_lastPeriod = data.SpinTimeMin + (data.SpinTimeMax - data.SpinTimeMin).Random();
			_indexTarget = UnityEngine.Random.Range(0, _slots);
		}

		public void Setup(IContext context, StatePhysics state)
		{
			var data = context.Resolve<ContainerApp>();
			state.IndexTarget = _indexTarget;
			state.DelayPeriod = _lastPeriod + (data.SpinTimeStepMax - data.SpinTimeStepMin).Random();
			state.StartStamp = DateTime.UtcNow;
			_lastPeriod = state.DelayPeriod;
		}

		public void SetStrategy<T>() where T : IServiceDistribution
		{
			throw new NotSupportedException("use controllerS instead");
		}
	}
}
