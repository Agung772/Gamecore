using Sirenix.OdinInspector;
using UnityEngine;

namespace Gamecore.Animation
{
    public class MoveLocalThis : MonoBehaviour
    {
        [SerializeField] private Vector3 to;
        [SerializeField] private float time;
        [SerializeField] private LeanTweenType type;
        
        [SerializeField] private bool loop;
        [SerializeField, ShowIf(nameof(loop))] private LeanTweenType loopType;
        private void OnEnable()
        {
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
