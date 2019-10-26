using Structure;
using Utility;

namespace App
{
	public class CommandUpdate : ICommand
	{
		public void Execute(IContext context)
		{
			"updating".Log();
		}
	}
}
