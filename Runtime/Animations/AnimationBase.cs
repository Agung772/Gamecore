using Sirenix.OdinInspector;
using UnityEngine;

namespace Gamecore
{
    public class AnimationBase : MonoBehaviour
    {
        [SerializeField] protected bool autoPlay = true;
        [SerializeField] protected bool useUnScaledTime;
        
        [SerializeField] protected bool loop;
        [SerializeField, ShowIf(nameof(loop))] protected LeanTweenType loopType;
        
        protected LTDescr descr;
        
        protected virtual void OnEnable()
        {
            if (autoPlay)
            {
                Play();
            }
        }

        protected virtual void OnDisable()
        {
            Stop();
        }
        
        public virtual void Play()
        {
            if (loop)
            {
                descr.setLoopType(loopType);
            }
        }
        
        public virtual void Stop()
        {
            if (descr != null)
            {
                gameObject.LeanCancel(descr.id);
            }
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}
