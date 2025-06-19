using UnityEngine;

namespace Gamecore
{
    public class AudioManager : GlobalBehaviour
    {
        public AudioSource BGMSource { get; private set; }
        public AudioSource SFXSource { get; private set; }

        public override void Initialize()
        {
            BGMSource = CreateSource("BGMSource");
            BGMSource.loop = true;
            SFXSource = CreateSource("SFXSource");
        }

        private AudioSource CreateSource(string sourceName)
        {
            var _source = new GameObject(sourceName).AddComponent<AudioSource>();
            _source.transform.SetParent(Game.Manager.transform);
            return _source;
        }
    }
}
