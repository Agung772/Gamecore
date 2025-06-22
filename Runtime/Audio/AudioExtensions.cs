using System;
using UnityEngine;

namespace Gamecore
{
    public static class AudioExtensions
    {
        public static void PlayLoop(this AudioClip clip)
        {
            if (clip == null) return;
            var _source = Game.Get<AudioManager>().BGMSource;
            if (_source.clip == clip) return;
            
            StopLoop(onComplete: () =>
            {
                _source.clip = clip;
                _source.Play();
            
                _source.volume = 0;
                _source.gameObject.LeanValue(value => _source.volume = value, 0, 1, 0.5f);
            });
        }

        public static void StopLoop(Action onComplete = null)
        {
            var _source = Game.Get<AudioManager>().BGMSource;
            if (_source.clip == null)
            {
                onComplete?.Invoke();
                return;
            }
            
            _source.volume = 1;
            _source.gameObject.LeanValue(value => _source.volume = value, 1, 0, 0.5f).setOnComplete(() => onComplete?.Invoke());
        }
        
        public static void PlayOneShot(this AudioClip clip)
        {
            if (clip == null) return;
            var _source = Game.Get<AudioManager>().SFXSource;
            _source.PlayOneShot(clip);
        }
    }
}
