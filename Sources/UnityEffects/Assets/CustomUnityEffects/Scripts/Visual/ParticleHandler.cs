using System.Collections;
using UnityEngine;

namespace CustomUnityEffects
{
	public class ParticleHandler : MonoBehaviour
	{
		public Vector3 Direction;
		public float MaxSpeed;
		public AnimationCurve SpeedCurve;
		public float LifeTime;

		private float _timer = 0f;
		private bool _started;

		public void SetParametersAndStart(Vector2 direction, float maxSpeed, AnimationCurve speedCurve, float lifeTime, float delay)
		{
			Direction = direction.normalized;
			MaxSpeed = maxSpeed;
			SpeedCurve = speedCurve;
			LifeTime = lifeTime;
			StartCoroutine(StartParticle(delay));
		}

		private IEnumerator StartParticle(float delay)
		{
			yield return new WaitForSeconds(delay);
			_started = true;
		}

		private void Update()
		{
			if (_started)
			{
				_timer += Time.deltaTime;
				var currentSpeed = MaxSpeed;
				if (LifeTime > 0f)
				{
					currentSpeed = MaxSpeed * SpeedCurve.Evaluate(Mathf.Clamp01(_timer / LifeTime));
				}

				transform.position = transform.position + Direction * currentSpeed * Time.deltaTime;

				if (_timer >= LifeTime)
				{
					DestroyParticle();
				}
			}
		}

		private void DestroyParticle()
		{
			Destroy(gameObject);
		}
	}
}
