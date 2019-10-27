using Model;

namespace Structure
{
	public interface IServiceDistribution
	{
		void Reset(IContext context);

		void Setup(IContext context, StatePhysics state);

		void SetStrategy<T>() where T : IServiceDistribution;
	}
}
