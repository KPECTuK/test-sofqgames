using System;
using System.Linq;
using App;
using Model;
using Structure;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utility;

namespace Rig
{
	// not optimal but easy, one may use overall mask on group (depends on design)
	[RequireComponent(typeof(Mask))]
	public class ControllerBarrel : UIBehaviour
	{
		private IContext _context;
		private ControllerSlot[] _slotControllers = new ControllerSlot[5];
		private Sprite[] _slotSprites;
		private int _indexSlotInQueue;

		public readonly StatePhysics PhysicsState = new StatePhysics();

		public RectTransform Transform { get; private set; }

		public Sprite GetSpriteForSlot()
		{
			var result = _slotSprites[_indexSlotInQueue];
			_indexSlotInQueue = ++_indexSlotInQueue % _slotSprites.Length;
			return result;
		}

		protected override void Awake()
		{
			base.Awake();

			enabled = false;

			Transform = GetComponent<RectTransform>();

			_slotSprites = ResourceSlots.Load().ToArray();

			for(var index = 0; index < _slotControllers.Length; index++)
			{
				var @object = new GameObject();
				@object.transform.SetParent(Transform);
				_slotControllers[index] = @object.AddComponent<ControllerSlot>();
				_slotControllers[index].Image.sprite = _slotSprites[index];
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();

			var component = transform.FindUpwards<ControllerApp>(null);
			if(component)
			{
				_context = component.Composition ?? throw  new Exception("can't find context in ControllerBarrel");
			}

			Array.ForEach(_slotControllers, _ =>
			{
				_.enabled = true;
				_.Transform.localScale = Vector3.one;
			});
		}

		protected override void OnDisable()
		{
			base.OnDisable();

			_context = null;
		}

		protected void Update()
		{
			_context?.Resolve<IPhysics>()?.UpdateState(PhysicsState);
			for(var index = 0; index < _slotControllers.Length; index++)
			{
				// ReSharper disable once LocalVariableHidesMember
				var transform = _slotControllers[index].Transform;
				var rect = Transform.rect;
				transform.anchoredPosition = Transform.rect.Shift(Vector2.down * PhysicsState.Speed).position + Vector2.up * rect.width * index;
				transform.sizeDelta = new Vector2(rect.width, rect.width);
			}
		}

		public void LateUpdate()
		{
			var rect = Transform.rect;
			var home = Transform.rect.ShiftUp();
			for(var index = _slotControllers.Length - 1; index > -1; index--)
			{
				if(_slotControllers[index].Transform.rect.Overlaps(rect))
				{
					// may optimization
					continue;
				}

				_slotControllers[index].Transform.anchoredPosition = home.position;
				_slotControllers[index].Image.sprite = GetSpriteForSlot();
				home = home.ShiftUp();
			}
		}

		private GUIStyle _style;

		protected void OnGUI()
		{
			Handles.BeginGUI();
			{
				var rect = Transform.rect;
				_style = _style ?? new GUIStyle { normal = new GUIStyleState { textColor = Color.red } };
				Handles.Label(rect.position, $"speed: {PhysicsState.Speed:####.000}", _style);
			}
			Handles.EndGUI();
		}
	}
}
