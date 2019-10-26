using Structure;

namespace App
{
	public class ControllerFSM : IContext
	{
		private readonly IScheduler[] _process;
		private int _current;

		public ControllerFSM(IContext context)
		{
			_process = new IScheduler[]
			{
				new ProcessIdle(context),
				new ProcessSpin(context),
			};
		}

		public T Resolve<T>() where T : class
		{
			return _process[_current] as T;
		}
	}
}
