using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CustomUnityEffects
{
	[CreateAssetMenu(menuName = "CustomUnityEffects/Video/Composite")]
	public class CompositeVisualEffect : VisualEffectBase
	{
		public bool ComposeInOneGameObject;
		public List<VisualEffectBase> Effects;
		public override GameObject Play(Vector3 position, Transform owner, Transform parent = null)
		{
			var vfxGameObjects = Effects.Select(vfx => vfx.Play(position, owner, parent)).ToList();

			if (ComposeInOneGameObject)
			{
				var vfxCompositeGo = new GameObject(this.name);
				vfxGameObjects.ForEach(go => go.transform.SetParent(vfxCompositeGo.transform));
				return vfxCompositeGo;
			}
				
			return null;
		}

		public override GameObject Play(Vector3 position, Vector3 direction, Transform owner, Transform parent = null)
		{
			var vfxGameObjects = Effects.Select(vfx => vfx.Play(position, direction, owner, parent)).ToList();

			if (ComposeInOneGameObject)
			{
				var vfxCompositeGo = new GameObject(this.name);
				vfxGameObjects.ForEach(go => go.transform.SetParent(vfxCompositeGo.transform));
				return vfxCompositeGo;
			}

			return null;
		}
	}
}
