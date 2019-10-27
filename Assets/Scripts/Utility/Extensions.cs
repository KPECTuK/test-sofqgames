using System;
using System.Linq;
using Model;
using Structure;
using UnityEngine;

namespace Utility
{
	public static class Extensions
	{
		// link to all code settlement for barrels number
		public const int BARRELS_I = 3;

		private static readonly System.Random _random = new System.Random();

		public static T FindDownwards<T>(this Component source, Predicate<T> predicate) where T : Component
		{
			predicate = predicate ?? (_ => true);

			var transform = source.transform;
			var component = transform.GetComponent<T>();
			if(component != null && predicate(component))
			{
				return component;
			}

			for(var index = 0; index < transform.childCount; index++)
			{
				component = transform.GetChild(index).FindDownwards(predicate);
				if(component is null)
				{
					continue;
				}
				return component;
			}

			return null;
		}

		public static T FindUpwards<T>(this Component source, Predicate<T> predicate) where T : Component
		{
			while(true)
			{
				predicate = predicate ?? (_ => true);

				var transform = source.transform;
				var component = transform.GetComponent<T>();

				if(component != null && predicate(component))
				{
					return component;
				}

				source = transform.parent;

				if(source is null)
				{
					return null;
				}
			}
		}

		public static bool Apply(this IScheduler source, IContext context, ICommand command)
		{
			command = command.Accept(source.Acceptable) ? command : null;
			command?.Execute(context);
			return context != null;
		}

		public static bool Accept(this ICommand command, Type[] acceptable)
		{
			var result = false;
			var type = command.GetType();
			for(var index = 0; index < acceptable.Length; index++)
			{
				result = result || acceptable[index].IsAssignableFrom(type);
			}
			return result;
		}

		public static bool IsSpinDenied(this ContainerApp source)
		{
			return source.Coins < source.Bet || source.SpinsAvailable <= 0;
		}

		public static bool IsRolling(this ContainerApp source)
		{
			return source.PhysicsState.Sum(_ => _.Speed) > 0f;
		}

		public static TimeSpan Random(this TimeSpan cap)
		{
			return TimeSpan.FromMilliseconds(cap.TotalMilliseconds * _random.NextDouble());
		}

		public static Rect ShiftUp(this Rect source)
		{
			return new Rect(source.position + Vector2.up * source.height, new Vector2(source.height, source.width));
		}

		public static Rect Shift(this Rect source, Vector2 offset)
		{
			return new Rect(source.position + offset, new Vector2(source.height, source.width));
		}

		public static void Log<T>(this T source)
		{
			var result =
				Log(source as Exception) ||
				Log(source as object);
			if(!result)
			{
				Debug.LogWarning($"object log lost: {typeof(T)} value: {source}");
			}
		}

		private static bool Log(Exception exception)
		{
			if(ReferenceEquals(null, exception))
			{
				return false;
			}

			Debug.LogError(exception.Message);
			return true;
		}

		private static bool Log(object @object)
		{
			if(@object is null)
			{
				return false;
			}

			Debug.Log(@object);
			return true;
		}
	}
}
