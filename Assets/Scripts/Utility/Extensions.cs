using System;
using UnityEngine;

namespace Utility
{
	public static class Extensions
	{
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
