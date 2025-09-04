using System;
using Gamecore.Tool;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gamecore.Animation
{
    public class MoveLocalThis : AnimationBase
    {
        [SerializeField] private bool isFrom;
        [SerializeField, ShowIf(nameof(isFrom)), PickFromScene] private Vector3 from;
        [SerializeField, PickFromScene] private Vector3 to;
        
        [SerializeField] private float time = 1;
        [SerializeField] private LeanTweenType type;

        private void Awake()
        {
            if (isFrom && !base.autoPlay)
            { 
                transform.localPosition = from;
            }
        }

        public override void Play()
        {
            base.Stop();
            if (isFrom && base.autoPlay)
            { 
                transform.localPosition = from;
            }
            base.descr = gameObject.LeanMoveLocal(to, time).setEase(type).setIgnoreTimeScale(base.useUnScaledTime);
            base.Play();
        }

        public override void ToDefault()
        {
            base.ToDefault();
            if (isFrom) transform.localPosition = from;
        }
    }
}
