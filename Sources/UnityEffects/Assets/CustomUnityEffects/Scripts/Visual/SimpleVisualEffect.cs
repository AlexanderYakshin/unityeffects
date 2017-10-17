using UnityEngine;

namespace CustomUnityEffects
{
	[CreateAssetMenu(menuName = "CustomUnityEffects/Video/Simple")]
	public class SimpleVisualEffect : VisualEffectBase
	{
		public GameObject Effect;
		[Range(0f, 360f)] public float RotationRandomness;

		public bool TryToFindInstantiatePoint;
		public string InstantiatePointObjectName;

		public override GameObject Play(Vector3 position, Transform owner, Transform parent = null)
		{
			return Play(position, Vector3.right, owner, parent);
		}

		public override GameObject Play(Vector3 position, Vector3 direction, Transform owner, Transform parent = null)
		{
			if (Effect == null)
			{
				Debug.LogError("Effect is null. Set Effect first and then play it.");
				return null;
			}

			var instantiatePointPosition = position;

			if (TryToFindInstantiatePoint && owner != null)
			{
				var insPoint = owner.Find(InstantiatePointObjectName);
				if (insPoint != null)
					instantiatePointPosition = insPoint.position;
			}

			var effectGameObject = GameObject.Instantiate(Effect, instantiatePointPosition, Quaternion.identity);
			effectGameObject.transform.SetParent(parent);

			var initialAngle = Vector2.SignedAngle(Vector2.right, direction);

			if (RotationRandomness > 0.001f)
			{
				var halfAngle = RotationRandomness/2f;
				var angle = Random.Range(-halfAngle, halfAngle);

				initialAngle += angle;
			}

			effectGameObject.transform.rotation = Quaternion.Euler(0f, 0f, initialAngle);

			return effectGameObject;
		}
	}
}