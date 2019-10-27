using System;
using System.Collections.Concurrent;
using System.Threading;
using App;
using Structure;
using Utility;

namespace Service
{
	public class DriverEmulator
	{
		private static readonly Thread _tread = new Thread(Run);
		private static readonly ManualResetEventSlim _timer = new ManualResetEventSlim();
		private static readonly TimeSpan _interval = TimeSpan.FromSeconds(10d);
		private static readonly CancellationTokenSource _source = new CancellationTokenSource();

		private static readonly ConcurrentQueue<ICommand> _commands = new ConcurrentQueue<ICommand>();
		private static IContext _mainContext;

		public void Start(IContext context)
		{
			_mainContext = context;
			_tread.Start(_source.Token);
		}

		public void Stop(IContext context)
		{
			_source.Cancel();
			_source.Dispose();
			_mainContext = null;
		}

		public void Consume(IContext context)
		{
			while(_commands.TryDequeue(out var command))
			{
				if(!context.Resolve<IScheduler>().Apply(context, command))
				{
					$"command reject, dropping: {command.GetType().Name}".Log();
				}
			}
		}

		private static void Run(object param)
		{
			var token = (CancellationToken)param;

			while(!token.IsCancellationRequested)
			{
				_timer.Wait(_interval, token);
				// no need to sync
				_commands.Enqueue(_mainContext.Resolve<CommandSwitchStrategy>());
			}
		}
	}
}
