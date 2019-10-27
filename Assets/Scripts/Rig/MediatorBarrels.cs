using System.Linq;
using Model;
using Structure;
using UnityEngine;
using Utility;

namespace Rig
{
	public class MediatorBarrels : IMediator
	{
		private readonly IContext _context;
		private readonly Sprite[] _slotSprites;

		public MediatorBarrels(IContext context)
		{
			_context = context;
			_slotSprites = ResourceSlots.Load().ToArray();
		}

		public void Update(ContainerApp container, ControllerScreen controller)
		{
			UpdateInternal();
		}

		public void UpdateForce(ContainerApp container, ControllerScreen controller)
		{
			var screen = _context.Resolve<ControllerScreen>();
			var data = _context.Resolve<ContainerApp>();
			for(var index = 0; index < Extensions.BARRELS_I; index++)
			{
				_context.Resolve<IServicePhysics>().StateResetIdle(data.PhysicsState[index]);
				ResetSprites(screen.ControllerBarrel[index].SlotControllers, data.PhysicsState[index]);
			}
		}

		private void UpdateInternal()
		{
			var screen = _context.Resolve<ControllerScreen>();
			var data = _context.Resolve<ContainerApp>();
			for(var index = 0; index < Extensions.BARRELS_I; index++)
			{
				var controller = screen.ControllerBarrel[index];
				var state = data.PhysicsState[index];

				_context.Resolve<IServicePhysics>()?.StateUpdate(state);
				state.RenderOffset += state.Speed;
				if(state.RenderOffset > 1f)
				{
					var increment = Mathf.FloorToInt(state.RenderOffset);
					state.IndexCurrent = (state.IndexCurrent + increment) % _slotSprites.Length;
					state.RenderOffset -= increment;

					// lower row wins, but who cares - this is the test, not a production

					// HACK: inject to update sprites from
					controller.Context = _context;
					controller.ToSpriteUpdate = state;
				}

				// kind of a hack also
				controller.RenderOffsetDriven = state.RenderOffset;
			}
		}

		public void ResetSprites(ControllerSlot[] slots, StatePhysics state)
		{
			for(var index = 0; index < slots.Length; index++)
			{
				slots[slots.Length - index - 1].Image.sprite = _slotSprites[(state.IndexCurrent + index) % _slotSprites.Length];
			}
		}
	}
}
