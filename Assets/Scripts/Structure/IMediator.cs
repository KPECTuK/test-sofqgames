using Model;
using Rig;

namespace Structure
{
	// UI animation
	public interface IMediator
	{
		// better to keep week, but easy to pass - ControllerScreen { .. }

		void Update(ContainerApp container, ControllerScreen controller);

		void UpdateForce(ContainerApp container, ControllerScreen controller);
	}
}
