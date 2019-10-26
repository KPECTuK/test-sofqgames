using System;
using App;
using Model;
using Structure;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utility;

namespace Rig
{
	public class ControllerScreen : UIBehaviour
	{
		public Button ButtonBet { get; private set; }
		public Button ButtonSpin { get; private set; }
		//
		public Text TextCoins { get; private set; }
		public Text TextBet { get; private set; }
		public Text TextSpinCount { get; private set; }
		public Text TextSpinRestore { get; private set; }
		//
		public Image ProgressSpin { get; private set; }
		// mutable - todo: protect
		public ControllerBarrel[] ControllerBarrel { get; } = new ControllerBarrel[3];

		// unstable
		private IContext _context;

		protected override void Awake()
		{
			base.Awake();

			// hello Unity
			enabled = false;

			try
			{
				ButtonBet = transform.FindDownwards<Button>(_ => _.name == "button-bet") ?? throw new Exception("not found: button-bet");
				ButtonSpin = transform.FindDownwards<Button>(_ => _.name == "button-spin") ?? throw new Exception("not found: button-spin");
				//
				TextCoins = transform.FindDownwards<Text>(_ => _.name == "text-coins") ?? throw new Exception("not found: text-coins");
				TextBet = transform.FindDownwards<Text>(_ => _.name == "text" && _.transform.parent.name == "button-bet") ?? throw new Exception("not found: text-bet");
				TextSpinCount = transform.FindDownwards<Text>(_ => _.name == "text" && _.transform.parent.name == "group-spins") ?? throw new Exception("not found: text-spins");
				TextSpinRestore = transform.FindDownwards<Text>(_ => _.name == "text-info") ?? throw new Exception("not found: text-info");
				//
				ProgressSpin = transform.FindDownwards<Image>(_ => _.name == "group-spins") ?? throw new Exception("not found: group-spins");
				//
				ControllerBarrel[0] = transform.FindDownwards<Transform>(_ => _.name == "group-barrel-00")?.gameObject.AddComponent<ControllerBarrel>() ?? throw new Exception("not found: group-barrel-00");
				ControllerBarrel[1] = transform.FindDownwards<Transform>(_ => _.name == "group-barrel-01")?.gameObject.AddComponent<ControllerBarrel>() ?? throw new Exception("not found: group-barrel-01");
				ControllerBarrel[2] = transform.FindDownwards<Transform>(_ => _.name == "group-barrel-02")?.gameObject.AddComponent<ControllerBarrel>() ?? throw new Exception("not found: group-barrel-02");
			}
			catch(Exception exception)
			{
				exception.Log();
			}
		}

		protected override void OnEnable()
		{
			//! should resume
			base.OnEnable();

			_context = transform.FindUpwards<ControllerApp>(null)?.Composition;
			if(ReferenceEquals(null, _context))
			{
				"can't find Context in: screen-ui".Log();
				return;
			}

			if(ButtonBet)
			{
				ButtonBet.onClick.AddListener(() => _context.Resolve<IScheduler>().Apply(_context.Resolve<CommandBet>()));
			}

			if(ButtonSpin)
			{
				ButtonSpin.onClick.AddListener(() => _context.Resolve<IScheduler>().Apply(_context.Resolve<CommandSpin>()));
			}

			var container = _context.Resolve<ContainerApp>();
			if(container == null)
			{
				return;
			}

			_context.Resolve<MediatorBarrels>().UpdateForce(container, this);
			// top down
			_context.Resolve<MediatorCoins>().UpdateForce(container, this);
			_context.Resolve<MediatorBet>().UpdateForce(container, this);
			_context.Resolve<MediatorSpinsAvailable>().UpdateForce(container, this);
			_context.Resolve<MediatorSpinsRestore>().UpdateForce(container, this);

			ControllerBarrel[0].enabled = true;
			ControllerBarrel[1].enabled = true;
			ControllerBarrel[2].enabled = true;
		}

		protected override void OnDisable()
		{
			//! should pause
			base.OnDisable();

			if(ButtonBet)
			{
				ButtonBet.onClick.RemoveAllListeners();
			}

			if(ButtonSpin)
			{
				ButtonSpin.onClick.RemoveAllListeners();
			}

			_context = null;
		}

		private void Update()
		{
			var container = _context?.Resolve<ContainerApp>();
			if(container == null)
			{
				return;
			}

			_context.Resolve<MediatorBarrels>()?.Update(container, this);
			// top down
			_context.Resolve<MediatorCoins>()?.Update(container, this);
			_context.Resolve<MediatorBet>()?.Update(container, this);
			_context.Resolve<MediatorSpinsAvailable>()?.Update(container, this);
			_context.Resolve<MediatorSpinsRestore>()?.Update(container, this);
		}
	}
}
