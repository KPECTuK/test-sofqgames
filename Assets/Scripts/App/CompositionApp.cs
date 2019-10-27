using System;
using Model;
using Rig;
using Service;
using Structure;
using UnityEngine;
using Utility;

namespace App
{
	public class CompositionApp : ContextBase
	{
		public CompositionApp(ControllerApp controller)
		{
			// uses this ref in .OnEnable()
			var screenController = controller
				.FindDownwards<Transform>(_ => _.name == "screen-ui")?
				.gameObject
				.AddComponent<ControllerScreen>() ?? throw new Exception("cant find: screen-ui");
			// ScreenController is a persistent singleton

			AppendSingleton(typeof(ControllerApp), controller);
			AppendSingleton(typeof(ControllerScreen), screenController);
			AppendSingleton(typeof(ContainerApp), new ContainerApp());
			AppendSingleton(typeof(IServicePhysics), new ServicePhysicsSimple(Extensions.BARRELS_I));
			AppendSingleton(typeof(IServiceDistribution), new ControllerDistribution(this));
			//
			AppendSingleton(typeof(CommandSwitchStrategy), new CommandSwitchStrategy());
			AppendSingleton(typeof(CommandBet), new CommandBet());
			AppendSingleton(typeof(CommandSpin), new CommandSpin());
			//
			var fsm = new ControllerFSM();
			AppendProvider(typeof(IScheduler), fsm);
			AppendProvider(typeof(IProcess), fsm);
			AppendSingleton(typeof(IFSM), fsm);
			//
			var animator = new ControllerAnimation(this);
			AppendProvider(typeof(MediatorCoins), animator);
			AppendProvider(typeof(MediatorBet), animator);
			AppendProvider(typeof(MediatorSpinsAvailable), animator);
			AppendProvider(typeof(MediatorSpinsRestore), animator);
			AppendProvider(typeof(MediatorBarrels), animator);
			//
			AppendSingleton(typeof(DriverEmulator), new DriverEmulator());
		}
	}
}
