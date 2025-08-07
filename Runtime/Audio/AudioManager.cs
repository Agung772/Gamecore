using UnityEngine;

namespace Gamecore
{
    public class AudioManager : GlobalBehaviour
    {
        public AudioSource BGMSource { get; private set; }
        public AudioSource SFXSource { get; private set; }

        public override void Initialize()
        {
            BGMSource = CreateSource("BGM");
            BGMSource.loop = true;
            SFXSource = CreateSource("SFX");
        }

        private AudioSource CreateSource(string name)
        {
            var _source = new GameObject($"{name}Source").AddComponent<AudioSource>();
            _source.transform.SetParent(Game.Manager.transform);
            _source.volume = PlayerPrefs.GetFloat($"{name}Volume", 1f);
            return _source;
        }
        
        public void MuteBGM(bool active)
        {
            BGMSource.mute = active;
        }
        
        public void MuteSFX(bool active)
        {
            SFXSource.mute = active;
        }

        public void SetBGMVolume(float volume)
        {
            BGMSource.volume = volume;
            PlayerPrefs.GetFloat("BGMVolume", volume);
        }
        
        public void SetSFXVolume(float volume)
        {
            SFXSource.volume = volume;
            PlayerPrefs.GetFloat("SFXVolume", volume);
        }
    }
}
