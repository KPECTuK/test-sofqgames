namespace Structure
{
	public interface IFSM
	{
		void RecycleProcess(IContext context);
		void RecycleProcessForce(IContext context);
	}
}
