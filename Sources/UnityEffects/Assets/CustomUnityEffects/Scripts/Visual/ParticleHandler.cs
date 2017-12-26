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
		public float SpeedTime;
		public bool RotateWhileFly;

		private float _timer = 0f;

		private bool _started;
		private int _rotationSign;
		private float _rotationSpeed;
		private float _currentAngle;

		public void SetParametersAndStart(Vector2 direction, float maxSpeed, AnimationCurve speedCurve, float speedTime, float lifeTime, float delay, bool rotateWhileFly)
		{
			Direction = direction.normalized;
			MaxSpeed = maxSpeed;
			SpeedCurve = speedCurve;
			SpeedTime = speedTime;
			LifeTime = lifeTime;
			RotateWhileFly = rotateWhileFly;
			if (RotateWhileFly)
				_rotationSign = Random.Range(0, 2) == 0 ? -1 : 1;

			_rotationSpeed = Random.Range(360f, 720f);
			_currentAngle = transform.rotation.eulerAngles.z;
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
				if (LifeTime > 0f && _timer < SpeedTime)
				{
					var currentSpeed = MaxSpeed;
					currentSpeed = MaxSpeed * SpeedCurve.Evaluate(Mathf.Clamp01(_timer / SpeedTime));
					transform.position = transform.position + Direction * currentSpeed * Time.deltaTime;
					_currentAngle = _currentAngle + _rotationSpeed * _rotationSign * Time.deltaTime;
					transform.rotation = Quaternion.Euler(0f, 0f, _currentAngle);
				}

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
