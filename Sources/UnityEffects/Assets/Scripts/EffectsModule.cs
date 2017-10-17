using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CustomUnityEffects
{
	public class EffectsModule :  MonoBehaviour
	{
		[SerializeField]
		private AudioSource _audioSource;

		[SerializeField]
		private List<AudioEffectBase> _audioEffects;
		[SerializeField]
		private List<VisualEffectBase> _visualEffects;

		public IEnumerable<AudioEffectBase> AudioEffects { get { return _audioEffects.ToArray(); } }
		public IEnumerable<VisualEffectBase> VisualEffects { get { return _visualEffects.ToArray(); } }

		public void PlayAudioEffect(int index)
		{
			if (index >= _audioEffects.Count)
			{
				Debug.LogError("The index of audio effect is out of range");
				return;
			}

			var audioEvent = _audioEffects[index];

			if (_audioSource != null)
				audioEvent.Play(_audioSource);
			else
				audioEvent.PlayAtPosition(transform.position);
		}

		public void PlayVisualFx(int index)
		{
			if (index >= _visualEffects.Count)
			{
				Debug.LogError("The index of visual effect is out of range");
				return;
			}

			var visualEffect = _visualEffects[index];
			visualEffect.Play(transform.position, transform);
		}

		public void PlaySoundFxByName(string effectName)
		{
			if (_audioEffects.Count == 0)
			{
				Debug.LogError("There is no any audio effect in effector");
				return;
			}

			var audioEffect = _audioEffects.FirstOrDefault(audioEff => audioEff.name.Equals(effectName, StringComparison.InvariantCultureIgnoreCase));
			if (audioEffect == null)
			{
				Debug.LogError(string.Format("Can't find Audio Effect with name {0}.", effectName));
				return;
			}

			if (_audioSource != null)
				audioEffect.Play(_audioSource);
			else
				audioEffect.PlayAtPosition(transform.position);
		}

		public void PlayVisualFxByName(string effectName)
		{
			if (_visualEffects.Count == 0)
			{
				Debug.LogError("There is no any visual effect in effector");
				return;
			}

			var visualEffect = _visualEffects.FirstOrDefault(visualEff => visualEff.name.Equals(effectName, StringComparison.InvariantCultureIgnoreCase));
			if (visualEffect == null)
			{
				Debug.LogError(string.Format("Can't find visual effect with name {0}.", effectName));
				return;
			}

			visualEffect.Play(transform.position, transform);
		}
	}
}
