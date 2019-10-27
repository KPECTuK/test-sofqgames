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
		public ControllerBarrel[] ControllerBarrel { get; } = new ControllerBarrel[Extensions.BARRELS_I];

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
				for(var index = 0; index < ControllerBarrel.Length; index++)
				{
					var source = $"group-barrel-{index:00}";
					ControllerBarrel[index] = transform.FindDownwards<Transform>(_ => _.name == source)?.gameObject.AddComponent<ControllerBarrel>() ?? throw new Exception($"not found: {source}");
				}
			}
			catch(Exception exception)
			{
				exception.Log();
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();

			_context = transform.FindUpwards<ControllerApp>(null)?.Composition;
			if(ReferenceEquals(null, _context))
			{
				"can't find Context in: screen-ui".Log();
				return;
			}

			var container = _context.Resolve<ContainerApp>();

			_context.Resolve<MediatorBarrels>().UpdateForce(container, this);
			_context.Resolve<MediatorCoins>().UpdateForce(container, this);
			_context.Resolve<MediatorBet>().UpdateForce(container, this);
			_context.Resolve<MediatorSpinsAvailable>().UpdateForce(container, this);
			_context.Resolve<MediatorSpinsRestore>().UpdateForce(container, this);

			Array.ForEach(ControllerBarrel, _ => _.enabled = true);
		}

		protected override void OnDisable()
		{
			base.OnDisable();

			_context = null;
		}

		private void Update()
		{
			var container = _context?.Resolve<ContainerApp>();
			if(container == null)
			{
				return;
			}

			// same screen
			_context.Resolve<MediatorBarrels>().Update(container, this);
			_context.Resolve<MediatorCoins>().Update(container, this);
			_context.Resolve<MediatorBet>().Update(container, this);
			_context.Resolve<MediatorSpinsAvailable>().Update(container, this);
			_context.Resolve<MediatorSpinsRestore>().Update(container, this);
		}
	}
}
