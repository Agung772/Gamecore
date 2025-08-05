using Sirenix.OdinInspector;
using UnityEngine;

namespace Gamecore.Animation
{
    public class RotateThis : MonoBehaviour
    {
        [SerializeField] private bool useUnScaledTime;
        
        [SerializeField] private bool isFrom;
        [SerializeField, ShowIf(nameof(isFrom))] private Vector3 from;
        [SerializeField] private Vector3 to;
        
        [SerializeField] private float add = 360;
        [SerializeField] private float time = 1;
        [SerializeField] private LeanTweenType type;
        
        [SerializeField] private bool loop;
        [SerializeField, ShowIf(nameof(loop))] private LeanTweenType loopType;
        private void OnEnable()
        {
            if (isFrom)
            {
                transform.eulerAngles = from;
            }
            
            if (loop)
            {
                gameObject.LeanRotateAroundLocal(to, add, time).setEase(type).setLoopType(loopType).setIgnoreTimeScale(useUnScaledTime);
            }
            else
            {
                gameObject.LeanRotateAroundLocal(to, add, time).setEase(type).setIgnoreTimeScale(useUnScaledTime);
            }
        }

        private void OnDisable()
        {
            gameObject.LeanCancel();
        }
    }
}

