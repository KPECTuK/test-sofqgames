using Model;

namespace Structure
{
	public interface IServicePhysics
	{
		void StateUpdate(StatePhysics model);

		void StateResetIdle(StatePhysics model);

		void StateResetRoll(StatePhysics model);
	}
}
