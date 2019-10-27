using System;
using Model;
using Structure;
using Utility;

namespace App
{
	public class ProcessBlock : IScheduler, IProcess
	{
		public Type[] Acceptable { get; } =
		{
			typeof(CommandSwitchStrategy)
		};

		public bool TryStart(IContext context)
		{
			var result = context.Resolve<ContainerApp>().IsSpinDenied();
			if(result)
			{
				$"enter {GetType().Name}".Log();
			}
			return result;
		}

		public bool TryStop(IContext context)
		{
			return !context.Resolve<ContainerApp>().IsSpinDenied();
		}

		public void StopForce(IContext context) { }
	}
}
