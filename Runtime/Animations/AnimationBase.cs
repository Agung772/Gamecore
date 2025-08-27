using Sirenix.OdinInspector;
using UnityEngine;

namespace Gamecore.Animation
{
    public class AnimationBase : MonoBehaviour
    {
        [SerializeField] protected bool useUnScaledTime;
        
        [SerializeField] protected bool loop;
        [SerializeField, ShowIf(nameof(loop))] protected LeanTweenType loopType;
        
        protected LTDescr descr;
        
        protected virtual void OnEnable()
        {
            if (loop)
            {
                descr.setLoopType(loopType);
            }
        }

        protected virtual void OnDisable()
        {
            if (descr != null)
            {
                gameObject.LeanCancel(descr.id);
            }
        }

        public void Play()
        {
            enabled = true;
        }
        
        public void Stop()
        {
            enabled = false;
        }
    }
}
