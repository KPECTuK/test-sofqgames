using System;
using Utility;

namespace Model
{
	public class ContainerApp
	{
		public int SpinsTotal = 20;
		public int BetMin = 1;
		public int BetMax = 5;
		public TimeSpan SpinTimeMin = TimeSpan.FromSeconds(1d);
		public TimeSpan SpinTimeMax = TimeSpan.FromSeconds(2d);
		public TimeSpan SpinTimeStepMin = TimeSpan.FromSeconds(1d);
		public TimeSpan SpinTimeStepMax = TimeSpan.FromSeconds(3d);
		public int SlotsTotal = 6;

		public int Coins = 50;
		public int Bet = 1;
		public int SpinsAvailable = 10;
		public int SpinsToRestore = 5;
		public TimeSpan RestorePeriod = TimeSpan.FromMinutes(1d);

		public readonly Match[] Matches =
		{
			new Match { Sequence = new[] { 0, 0, 0 }, Factor = 5 }, //! Extensions.BARRELS_I
			new Match { Sequence = new[] { 1, 1, 1 }, Factor = 5 },
			new Match { Sequence = new[] { 2, 2, 2 }, Factor = 5 },
			new Match { Sequence = new[] { 3, 3, 3 }, Factor = 5 },
			new Match { Sequence = new[] { 4, 4, 4 }, Factor = 5 },
			new Match { Sequence = new[] { 5, 5, 5 }, Factor = 5 },
		};

		public readonly StatePhysics[] PhysicsState;

		public ContainerApp()
		{
			PhysicsState = new StatePhysics[Extensions.BARRELS_I];
			for(var index = 0; index < PhysicsState.Length; index++)
			{
				PhysicsState[index] = new StatePhysics();
			}
		}
	}
}
