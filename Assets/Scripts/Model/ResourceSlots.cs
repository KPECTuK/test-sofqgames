using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Model
{
	public class ResourceSlots : ScriptableObject, IEnumerable<Sprite>
	{
		private const string FILENAME_S = "resource-slots";

		public Sprite Slot_0;
		public Sprite Slot_1;
		public Sprite Slot_2;
		public Sprite Slot_3;
		public Sprite Slot_4;
		public Sprite Slot_5;

		[MenuItem("Assets/Create/test-sofqgames/Resources")]
		public static void Create()
		{
			var asset = CreateInstance<ResourceSlots>();
			AssetDatabase.CreateAsset(asset, $"Assets/Resources/{FILENAME_S}.asset");
			AssetDatabase.SaveAssets();
		}

		public static ResourceSlots Load()
		{
			return Resources.Load<ResourceSlots>(FILENAME_S);
		}

		public IEnumerator<Sprite> GetEnumerator()
		{
			yield return Slot_0;
			yield return Slot_1;
			yield return Slot_2;
			yield return Slot_3;
			yield return Slot_4;
			yield return Slot_5;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
