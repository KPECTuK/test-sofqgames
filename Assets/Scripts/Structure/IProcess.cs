namespace Structure
{
	public interface IProcess
	{
		bool TryStart(IContext context);

		bool TryStop(IContext context);

		void StopForce(IContext context);
	}
}
