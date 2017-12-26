using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CustomUnityEffects
{
	[CreateAssetMenu(menuName = "CustomUnityEffects/Video/Particles")]
	public class ParticleVisualEffect : VisualEffectBase
	{
		[Serializable]
		private class Particle
		{
			public ParticleHandler Prefab;
			public Vector2 Direction;
			public float RandomAngleWithinDirection;
			public bool _randomDirection;
			public bool DependOnOwnersDirection;
			public float MaxSpeed;
			public AnimationCurve SpeedCurve;
			[MinMaxFloatRange(0.02f, 10f)]
			public RangedFloat SpeedTime;
			public float LifeTime;
			public bool RandomDelay;
			[MinMaxFloatRange(0f, 2f)]
			public RangedFloat DelayValue;
			public bool RotateWhileFly;
			public bool InstantiateWithoutParent;
		}

		[SerializeField]
		private List<Particle> _particles;
		[SerializeField]
		private bool _randomParticle;
		[SerializeField]
		private int _particlesCount;

		public override GameObject Play(Vector3 position, Transform owner, Transform parent = null)
		{
			if (_particles.Count == 0)
			{
				Debug.LogError("There are no any particle in effect.");
				return null;
			}

			if (_particlesCount <= 0)
			{
				Debug.LogError("Particles Count less or equals zero.");
				return null;
			}

			var instantiatePointPosition = position;

			for (int i = 0; i < _particlesCount; i++)
			{
				var particlesCount = _particles.Count;
				int k = i / particlesCount;
				var index = i - k * particlesCount;

				if (_randomParticle)
				{
					index = UnityEngine.Random.Range(0, particlesCount);
				}
				var particle = _particles[index];
				var effectGameObject = GameObject.Instantiate(particle.Prefab, instantiatePointPosition, Quaternion.identity);


				if (!particle.InstantiateWithoutParent)
					effectGameObject.transform.SetParent(parent);
				var particleDirection = particle.Direction;
				if (particle.DependOnOwnersDirection && owner != null)
				{
					particleDirection = owner.rotation * particleDirection;
				}
				if (particle.RandomAngleWithinDirection > 0.01f)
				{
					var halfAngle = particle.RandomAngleWithinDirection / 2f;
					var randomAngle = Random.Range(-halfAngle, halfAngle);
					particleDirection = Quaternion.Euler(0f, 0f, randomAngle) * particleDirection;
				}

				if (particle._randomDirection)
				{
					particleDirection = UnityEngine.Random.insideUnitCircle;
				}

				var delay = particle.DelayValue.MinValue;
				if (particle.RandomDelay)
				{
					delay = UnityEngine.Random.Range(particle.DelayValue.MinValue, particle.DelayValue.MaxValue);
				}
				var speedTime = Random.Range(particle.SpeedTime.MinValue, particle.SpeedTime.MaxValue);
				effectGameObject.SetParametersAndStart(particleDirection, particle.MaxSpeed, particle.SpeedCurve, speedTime, particle.LifeTime, delay, particle.RotateWhileFly);
			}

			return null;
		}

		public override GameObject Play(Vector3 position, Vector3 direction, Transform owner, Transform parent = null)
		{
			if (_particles.Count == 0)
			{
				Debug.LogError("There are no any particle in effect.");
				return null;
			}

			if (_particlesCount <= 0)
			{
				Debug.LogError("Particles Count less or equals zero.");
				return null;
			}

			var instantiatePointPosition = position;
			for (int i = 0; i < _particlesCount; i++)
			{
				var particlesCount = _particles.Count;
				int k = i / particlesCount;
				var index = i - k * particlesCount;

				if (_randomParticle)
				{
					index = UnityEngine.Random.Range(0, particlesCount);
				}
				var particle = _particles[index];
				var effectGameObject = GameObject.Instantiate(particle.Prefab, instantiatePointPosition, Quaternion.identity);
				if (!particle.InstantiateWithoutParent)
					effectGameObject.transform.SetParent(parent);

				var particleDirection = particle.Direction;
				if (particle._randomDirection)
				{
					particleDirection = UnityEngine.Random.insideUnitCircle;
				}
				var resultDirection = particleDirection;
				if (direction != Vector3.zero)
				{
					var directionAngle = Vector2.SignedAngle(Vector2.up, direction);
					var directionRotation = Quaternion.Euler(0f, 0f, directionAngle);
					resultDirection = directionRotation * resultDirection;
				}

				var delay = particle.DelayValue.MinValue;
				if (particle.RandomDelay)
				{
					delay = UnityEngine.Random.Range(particle.DelayValue.MinValue, particle.DelayValue.MaxValue);
				}
				var speedTime = Random.Range(particle.SpeedTime.MinValue, particle.SpeedTime.MaxValue);
				effectGameObject.SetParametersAndStart(resultDirection, particle.MaxSpeed, particle.SpeedCurve, speedTime, particle.LifeTime, delay, particle.RotateWhileFly);
			}

			return null;
		}
	}
}
