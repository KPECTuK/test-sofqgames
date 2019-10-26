using Structure;

namespace App
{
	public class ProcessIdle : IScheduler, IProcess
	{
		private readonly IContext _context;

		public ProcessIdle(IContext context)
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
