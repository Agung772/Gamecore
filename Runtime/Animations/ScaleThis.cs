using Sirenix.OdinInspector;
using UnityEngine;

namespace Gamecore.Animation
{
    public class ScaleThis : AnimationBase
    {
        [SerializeField] private bool isFrom;
        [SerializeField, ShowIf(nameof(isFrom))] private Vector3 from;
        [SerializeField] private Vector3 to;
        
        [SerializeField] private float time = 1;
        [SerializeField] private LeanTweenType type;
        
        private int tweenID;

        private void Awake()
        {
            if (isFrom && !base.autoPlay)
            {
                transform.localScale = from;
            }
        }
        
        public override void Play()
        {
            base.Stop();
            if (isFrom && base.autoPlay)
            {
                transform.localScale = from;
            }
            base.descr = gameObject.LeanScale(to, time).setEase(type).setIgnoreTimeScale(useUnScaledTime);
            base.Play();
        }
        
        public override void ToDefault(bool fasted = false)
        {
            base.Stop();
            base.ToDefault(fasted);
            if (isFrom)
            {
                if (fasted) transform.localScale = from;
                else gameObject.LeanScale(from, time).setEase(type).setIgnoreTimeScale(useUnScaledTime);
            }
        }
    }
}
