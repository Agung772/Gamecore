using Sirenix.OdinInspector;
using UnityEngine;

namespace Gamecore.Animation
{
    public class RotateThis : AnimationBase
    {
        [SerializeField] private bool isFrom;
        [SerializeField, ShowIf(nameof(isFrom))] private Vector3 from;
        [SerializeField] private Vector3 to;
        
        [SerializeField] private float add = 360;
        [SerializeField] private float time = 1;
        [SerializeField] private LeanTweenType type;

        private void Awake()
        {
            if (isFrom && !base.autoPlay)
            {
                transform.eulerAngles = from;
            }
        }
        public override void Play()
        {
            base.Stop();
            if (isFrom && base.autoPlay)
            {
                transform.eulerAngles = from;
            }
            base.descr = gameObject.LeanRotateAroundLocal(to, add, time).setEase(type).setIgnoreTimeScale(useUnScaledTime);
            base.Play();
        }
        
        public override void ToDefault(bool fasted = false)
        {
            base.Stop();
            base.ToDefault(fasted);
            if (isFrom)
            {
                if (fasted) transform.eulerAngles = from;
                else gameObject.LeanRotateAroundLocal(from, add, time).setEase(type).setIgnoreTimeScale(useUnScaledTime);
            }
        }
    }
}

