using Sirenix.OdinInspector;
using UnityEngine;

namespace Gamecore.Animation
{
    public class ScaleThis : MonoBehaviour
    {
        [SerializeField] private bool isFrom;
        [SerializeField, ShowIf(nameof(isFrom))] private Vector3 from;
        [SerializeField] private Vector3 to;
        
        [SerializeField] private float time = 1;
        [SerializeField] private LeanTweenType type;
        
        [SerializeField] private bool loop;
        [SerializeField, ShowIf(nameof(loop))] private LeanTweenType loopType;
        private void OnEnable()
        {
            if (isFrom)
            {
                transform.localPosition = from;
            }
            
            if (loop)
            {
                gameObject.LeanScale(to, time).setEase(type).setLoopType(loopType);
            }
            else
            {
                gameObject.LeanScale(to, time).setEase(type);
            }
        }

        private void OnDisable()
        {
            gameObject.LeanCancel();
        }
    }
}
