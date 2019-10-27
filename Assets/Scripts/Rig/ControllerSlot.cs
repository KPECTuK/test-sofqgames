using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rig
{
	[RequireComponent(typeof(Image))]
	public class ControllerSlot : UIBehaviour
	{
		public RectTransform Transform { get; private set; }
		public Image Image { get; private set; }

		protected override void Awake()
		{
			base.Awake();

			enabled = false;

			Transform = GetComponent<RectTransform>();
			Image = GetComponent<Image>();
		}
	}
}
