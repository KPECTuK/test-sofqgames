using System;
using Model;
using Structure;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Rig
{
	public class ControllerBarrel : UIBehaviour
	{
		[NonSerialized]
		public readonly ControllerSlot[] SlotControllers = new ControllerSlot[4];
		[NonSerialized]
		public float RenderOffsetDriven;
		[NonSerialized]
		public StatePhysics ToSpriteUpdate;
		[NonSerialized]
		public IContext Context;

		private RectTransform _rectTransform;

		protected override void Awake()
		{
			base.Awake();

			_rectTransform = GetComponent<RectTransform>();
			for(var index = 0; index < SlotControllers.Length; index++)
			{
				var @object = new GameObject($"slot-{index:0}");
				@object.transform.SetParent(_rectTransform);
				SlotControllers[index] = @object.AddComponent<ControllerSlot>();
				SlotControllers[index].Transform.pivot = new Vector2(0f, 1f);
				SlotControllers[index].Transform.anchorMin = new Vector2(0f, 1f);
				SlotControllers[index].Transform.anchorMax = new Vector2(0f, 1f);
				SlotControllers[index].Transform.localScale = Vector3.one;
			}
		}

		protected void Update()
		{
			var thisRect = _rectTransform.rect;

			//HACK: it doesn't link Update and LateUpdate - the test only
			Context?.Resolve<MediatorBarrels>().ResetSprites(SlotControllers, ToSpriteUpdate);

			for(var index = 0; index < SlotControllers.Length; index++)
			{
				var transformChild = SlotControllers[index].Transform;
				transformChild.sizeDelta = new Vector2(thisRect.width, thisRect.width);
				transformChild.anchoredPosition3D = (index + RenderOffsetDriven - 1) * thisRect.width * Vector3.down;
			}
		}
	}
}
