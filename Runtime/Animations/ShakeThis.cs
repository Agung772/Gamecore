using UnityEngine;
using Random = UnityEngine.Random;

namespace Gamecore
{
    public class ShakeThis : AnimationBase
    {
        private Vector3 from;

        [SerializeField] private float moveDuration = 0.5f; // Durasi gerakan shake pertama
        [SerializeField] private float shakeOffsetStrength = 0.1f; // Seberapa jauh efek shake dari posisi nol
        [SerializeField] private float returnDuration = 0.2f; // Durasi kembali ke posisi nol
        
        public override void Play()
        {
            base.Stop();
            from = transform.localPosition;
            base.descr = gameObject.LeanMoveLocal(from, moveDuration)
                .setEase(LeanTweenType.easeShake)
                .setFrom(from + Random.insideUnitSphere * shakeOffsetStrength)
                .setOnComplete(() =>
                {
                    gameObject.LeanMoveLocal(from, returnDuration);
                }).setIgnoreTimeScale(base.useUnScaledTime);
            
            base.Play();
        }
    }
}
