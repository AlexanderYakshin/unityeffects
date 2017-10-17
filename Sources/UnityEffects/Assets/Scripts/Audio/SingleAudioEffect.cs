using UnityEngine;

namespace CustomUnityEffects
{
	[CreateAssetMenu(menuName = "CustomUnityEffects/Audio/Simple")]
	public class SingleAudioEffect : AudioEffectBase
	{
		public AudioClip[] Clips;

		public RangedFloat Volume;

		[MinMaxFloatRange(0, 2)]
		public RangedFloat Pitch;

		public override void Play(AudioSource source)
		{
			var clip = GetAudioClip();
			if (clip == null)
				return;

			source.clip = clip;
			source.volume = Random.Range(Volume.MinValue, Volume.MaxValue);
			source.pitch = Random.Range(Pitch.MinValue, Pitch.MaxValue);
			source.Play();
		}

		public override void PlayAtPosition(Vector3 position)
		{
			var clip = GetAudioClip();
			if (clip == null)
				return;

			AudioSource.PlayClipAtPoint(clip, position, Random.Range(Volume.MinValue, Volume.MaxValue));
		}

		public override void FindAudioSourceAndPlay()
		{
			if (string.IsNullOrEmpty(AudioSourceTag))
				return;

			var go = GameObject.FindGameObjectWithTag(AudioSourceTag);
			if (go != null)
			{
				var audioSource = go.GetComponent<AudioSource>();
				if (audioSource != null)
				{
					Play(audioSource);
				}
			}
			else
			{
				Debug.LogError(string.Format("Can't find audio source by tag '{0}'", AudioSourceTag));
			}
		}

		private AudioClip GetAudioClip()
		{
			return Clips.Length == 0
				? null
				: Clips[Random.Range(0, Clips.Length)];
		}
	}
}
