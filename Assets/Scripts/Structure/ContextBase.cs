using System;
using System.Collections.Generic;
using System.Linq;

namespace Structure
{
	public abstract class ContextBase : IContext
	{
		private static readonly Type[] _paramTypes = { typeof(IContext) };

		private readonly object[] _params;
		private readonly IDictionary<Type, object> _singletons = new Dictionary<Type, object>();
		private readonly IDictionary<Type, IContext> _providers = new Dictionary<Type, IContext>();

		protected void AppendSingleton(Type type, object value)
		{
			_singletons.Add(type, value);
		}

		protected void AppendProvider(Type type, IContext value)
		{
			_providers.Add(type, value);
		}

		protected ContextBase()
		{
			_params = new object[] { this };
		}

		public T Resolve<T>() where T : class
		{
			if(_singletons.TryGetValue(typeof(T), out var value))
			{
				return value as T;
			}

			// ReSharper disable once ConvertIfStatementToReturnStatement
			if(_providers.TryGetValue(typeof(T), out var provider))
			{
				return provider.Resolve<T>();
			}

			return BuildType<T>();
		}

		private T BuildType<T>() where T : class
		{
			var ctors = typeof(T).GetConstructors();

			var ctor = ctors.FirstOrDefault(_ => _.GetParameters().Select(_1 => _1.ParameterType).SequenceEqual(_paramTypes));
			if(ctor != null)
			{
				return ctor.Invoke(_params) as T;
			}

			ctor = ctors.FirstOrDefault(_ => _.GetParameters().Length == 0);
			if(ctor != null)
			{
				return ctor.Invoke(null) as T;
			}

			throw new Exception($"can't build type: {typeof(T)}");
		}
	}
}
