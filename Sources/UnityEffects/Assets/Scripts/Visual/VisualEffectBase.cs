using UnityEngine;

namespace CustomUnityEffects
{
	public abstract class VisualEffectBase : ScriptableObject
	{
		public abstract GameObject Play(Vector3 position, Transform owner, Transform parent = null);
		public abstract GameObject Play(Vector3 position, Vector3 direction, Transform owner, Transform parent = null);
	}
}
