using System;
using Model;
using Rig;
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
			AppendSingleton(typeof(IPhysics), new SimplePhysics());
			//
			AppendSingleton(typeof(CommandUpdate), new CommandUpdate());
			AppendSingleton(typeof(CommandBet), new CommandBet());
			AppendSingleton(typeof(CommandSpin), new CommandSpin());
			//
			var fsm = new ControllerFSM(this);
			AppendProvider(typeof(IScheduler), fsm);
			AppendProvider(typeof(IProcess), fsm);
			var animator = new ControllerAnimation();
			AppendProvider(typeof(MediatorCoins), animator);
			AppendProvider(typeof(MediatorBet), animator);
			AppendProvider(typeof(MediatorSpinsAvailable), animator);
			AppendProvider(typeof(MediatorSpinsRestore), animator);
			AppendProvider(typeof(MediatorBarrels), animator);
		}
	}
}
