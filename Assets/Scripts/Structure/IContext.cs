namespace Structure
{
	public interface IContext
	{
		T Resolve<T>() where T : class;
	}
}
