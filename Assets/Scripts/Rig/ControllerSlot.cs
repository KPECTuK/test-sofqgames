using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utility;

namespace Rig
{
	[RequireComponent(typeof(Image))]
	public class ControllerSlot : UIBehaviour
	{
		private ControllerBarrel _parent;

		public RectTransform Transform { get; private set; }
		public Image Image { get; private set; }

		protected override void Awake()
		{
			base.Awake();

			enabled = false;

			Transform = GetComponent<RectTransform>();
			Image = GetComponent<Image>();

			_parent = transform.FindUpwards<ControllerBarrel>(null) ?? throw new Exception("can't find parent in ControllerSlots");
		}

		private void Update()
		{
		}
	}
}
