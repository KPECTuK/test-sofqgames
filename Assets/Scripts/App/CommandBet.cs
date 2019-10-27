using Model;
using Structure;

namespace App
{
	public class CommandBet : ICommand
	{
		public void Execute(IContext context)
		{
			var data = context.Resolve<ContainerApp>();
			if(data.Bet < data.Coins && data.Bet < data.BetMax)
			{
				data.Bet += 1;
			}
		}
	}
}
