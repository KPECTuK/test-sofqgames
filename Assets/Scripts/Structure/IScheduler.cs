namespace Structure
{
	public interface IScheduler
	{
		bool Apply(ICommand command);

		void NotifyProcessComplete();
	}
}
