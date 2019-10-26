using Rig;
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
			Composition.Resolve<IScheduler>().Apply(new CommandUpdate());
		}

		private void LateUpdate()
		{
			if(Composition.Resolve<IProcess>().AssertComplete())
			{
				Composition.Resolve<IScheduler>().NotifyProcessComplete();
			}
		}
	}
}
