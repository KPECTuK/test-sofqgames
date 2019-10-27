using System.Linq;

namespace Model
{
	public struct Match
	{
		public int Factor;
		//public fixed int Sequence[3];
		public int[] Sequence;

		public bool Is(StatePhysics[] states)
		{
			// should optimization
			return states.Select(_ => _.IndexTarget).SequenceEqual(Sequence);
		}
	}
}
