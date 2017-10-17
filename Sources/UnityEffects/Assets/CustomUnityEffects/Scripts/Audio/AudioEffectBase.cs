using UnityEngine;

namespace CustomUnityEffects
{
	public abstract class AudioEffectBase : ScriptableObject
	{
		public string AudioSourceTag;

		public abstract void Play(AudioSource source);
		public abstract void PlayAtPosition(Vector3 position);
		public abstract void FindAudioSourceAndPlay();
	}
}
