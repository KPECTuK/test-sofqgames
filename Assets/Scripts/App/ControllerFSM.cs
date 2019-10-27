using System;
using Structure;

namespace App
{
	public class ControllerFSM : IContext, IFSM
	{
		private readonly IProcess[] _process;
		private int _currentIndex;

		public ControllerFSM()
		{
			_process = new IProcess[]
			{
				new ProcessStart(),
				new ProcessBlock(),
				new ProcessAwait(),
				new ProcessSpin(),
			};
		}

		public T Resolve<T>() where T : class
		{
			return _process[_currentIndex] as T;
		}

		public void RecycleProcess(IContext context)
		{
			if(!_process[_currentIndex].TryStop(context))
			{
				return;
			}

			if(SwitchNextAvailable(context))
			{
				return;
			}

			throw new Exception("no process available [regular]");
		}

		public void RecycleProcessForce(IContext context)
		{
			_process[_currentIndex].StopForce(context);

			if(SwitchNextAvailable(context))
			{
				return;
			}

			throw new Exception("no process available [force]");
		}

		private bool SwitchNextAvailable(IContext context)
		{
			for(var index = 0; index < _process.Length; index++)
			{
				var indexRequest = (_currentIndex + index + 1) % _process.Length;
				if(_process[indexRequest].TryStart(context))
				{
					_currentIndex = indexRequest;
					return true;
				}
			}

			return false;
		}
	}
}
