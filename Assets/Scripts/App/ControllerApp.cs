using Rig;
using Service;
using Structure;
using UnityEngine;

namespace App
{
	public class ControllerApp : MonoBehaviour
	{
		public CompositionApp Composition { get; private set; }

		private void Awake()
		{
			Composition = new CompositionApp(this);
			// goodbye Unity
			Composition.Resolve<ControllerScreen>().enabled = true;
		}

		private void LateUpdate()
		{
			Composition.Resolve<DriverEmulator>().Consume(Composition);
			Composition.Resolve<IFSM>().RecycleProcess(Composition);
		}

		private void OnEnable()
		{
			Composition.Resolve<DriverEmulator>().Start(Composition);
		}

		private void OnDisable()
		{
			Composition.Resolve<DriverEmulator>().Stop(Composition);
		}
	}
}
