using Structure;

namespace App
{
	/// <summary>
	/// start stub
	/// </summary>
	public class ProcessStart : IProcess
	{
		public bool TryStart(IContext context)
		{
			return false;
		}

		public bool TryStop(IContext context)
		{
			return true;
		}

		public void StopForce(IContext context) { }
	}
}
