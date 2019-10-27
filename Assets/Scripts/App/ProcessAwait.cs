using System;
using Model;
using Rig;
using Structure;
using Utility;

namespace App
{
	public class ProcessAwait : IScheduler, IProcess
	{
		public Type[] Acceptable { get; } =
		{
			typeof(CommandBet),
			typeof(CommandSpin),
			typeof(CommandSwitchStrategy),
		};

		public bool TryStart(IContext context)
		{
			$"enter {GetType().Name}".Log();

			var controller = context.Resolve<ControllerScreen>();

			if(controller.ButtonBet)
			{
				controller.ButtonBet.onClick.AddListener(() => context.Resolve<IScheduler>().Apply(context, context.Resolve<CommandBet>()));
			}

			if(controller.ButtonSpin)
			{
				controller.ButtonSpin.onClick.AddListener(() => context.Resolve<IScheduler>().Apply(context, context.Resolve<CommandSpin>()));
			}

			return true;
		}

		public bool TryStop(IContext context)
		{
			return false;
		}

		public void StopForce(IContext context)
		{
			var screen = context.Resolve<ControllerScreen>();

			if(screen.ButtonBet)
			{
				screen.ButtonBet.onClick.RemoveAllListeners();
			}

			if(screen.ButtonSpin)
			{
				screen.ButtonSpin.onClick.RemoveAllListeners();
			}
		}
	}
}
