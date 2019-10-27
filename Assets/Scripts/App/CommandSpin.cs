using Structure;

namespace App
{
	public class CommandSpin : ICommand
	{
		public void Execute(IContext context)
		{
			context.Resolve<IFSM>().RecycleProcessForce(context);
		}
	}
}
