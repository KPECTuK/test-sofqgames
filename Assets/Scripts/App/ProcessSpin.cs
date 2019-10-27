using System;
using System.Security.Cryptography;
using Model;
using Service;
using Structure;
using Utility;

namespace App
{
	public class ProcessSpin : IScheduler, IProcess
	{
		public Type[] Acceptable { get; } = { };

		public bool TryStart(IContext context)
		{
			var data = context.Resolve<ContainerApp>();
			if(data.IsSpinDenied())
			{
				return false;
			}

			$"enter {GetType().Name}".Log();

			data.Coins -= data.Bet;
			data.Bet = data.BetMin;
			data.SpinsAvailable -= 1;

			context.Resolve<IServiceDistribution>().Reset(context);
			for(var index = 0; index < data.PhysicsState.Length; index++)
			{
				context.Resolve<IServiceDistribution>().Setup(context, data.PhysicsState[index]);
				context.Resolve<IServicePhysics>().StateResetRoll(data.PhysicsState[index]);
			}

			return true;
		}

		public bool TryStop(IContext context)
		{
			if(context.Resolve<ContainerApp>().IsRolling())
			{
				return false;
			}

			Score(context);

			return true;
		}

		public void StopForce(IContext context)
		{
			throw new NotImplementedException("align barrels to targets");

			// stop roll
			// score
		}

		private void Score(IContext context)
		{
			var data = context.Resolve<ContainerApp>();
			var index = Array.FindIndex(data.Matches, _ => _.Is(data.PhysicsState));
			if(index != -1)
			{
				data.Coins += data.Bet * data.Matches[index].Factor;
			}
		}
	}
}
