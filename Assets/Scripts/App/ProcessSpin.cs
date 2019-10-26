using Structure;

namespace App
{
	public class ProcessSpin : IScheduler, IProcess
	{
		private readonly IContext _context;

		public ProcessSpin(IContext context)
		{
			_context = context;
		}

		public bool Apply(ICommand command)
		{
			return false;
		}

		public void NotifyProcessComplete() { }

		public bool AssertComplete()
		{
			return true;
		}
	}
}
