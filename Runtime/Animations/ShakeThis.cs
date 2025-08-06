using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gamecore
{
    public class ShakeThis : MonoBehaviour
    {
        [SerializeField] private bool useUnScaledTime;
        
        [SerializeField] private bool autoPlay;
        [SerializeField] private float moveDuration = 0.5f; // Durasi gerakan shake pertama
        [SerializeField] private float shakeOffsetStrength = 0.1f; // Seberapa jauh efek shake dari posisi nol
        [SerializeField] private float returnDuration = 0.2f; // Durasi kembali ke posisi nol
        [SerializeField] private LeanTweenType type = LeanTweenType.easeShake;
        
        [SerializeField] private bool loop;
        [SerializeField, ShowIf(nameof(loop))] private LeanTweenType loopType;

        private int tweenID;
        private void OnEnable()
        {
            if (autoPlay)
            {
                Play();
            }
        }

        private void OnDisable()
        {
            gameObject.LeanCancel();
        }

        public void Play()
        {
            gameObject.LeanCancel(tweenID);
            var _tween = gameObject.LeanMoveLocal(Vector3.zero, moveDuration)
                .setEase(type)
                .setFrom(Vector3.zero + Random.insideUnitSphere * shakeOffsetStrength)
                .setOnComplete(() =>
                {
                    gameObject.LeanMoveLocal(Vector3.zero, returnDuration);
                }).setIgnoreTimeScale(useUnScaledTime);

            tweenID = _tween.id;

            if (loop)
            {
                _tween.setLoopType(loopType);
            }
        }
    }
}
