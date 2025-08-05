using Gamecore.Tool;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gamecore.Animation
{
    public class MoveLocalThis : MonoBehaviour
    {
        [SerializeField] private bool isFrom;
        [SerializeField, ShowIf(nameof(isFrom)), PickFromScene] private Vector3 from;
        [SerializeField, PickFromScene] private Vector3 to;
        
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
                gameObject.LeanMoveLocal(to, time).setEase(type).setLoopType(loopType);
            }
            else
            {
                gameObject.LeanMoveLocal(to, time).setEase(type);
            }
        }

        private void OnDisable()
        {
            gameObject.LeanCancel();
        }
    }
}
