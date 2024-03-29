﻿using Service;
using Structure;

namespace App
{
	public class CommandSwitchStrategy : ICommand
	{
		private int _counter;

		public void Execute(IContext context)
		{
			if(_counter % 2 > 0)
			{
				context.Resolve<IServiceDistribution>().SetStrategy<ServiceDistributionWin>();
			}
			else
			{
				context.Resolve<IServiceDistribution>().SetStrategy<ServiceDistributionSimple>();
			}

			_counter++;
		}
	}
}
