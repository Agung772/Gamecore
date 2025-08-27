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

        public override void Play()
        {
            if (isFrom)
            {
                transform.eulerAngles = from;
            }
            
            base.descr = gameObject.LeanRotateAroundLocal(to, add, time).setEase(type).setIgnoreTimeScale(useUnScaledTime);
            base.Play();
        }
    }
}

