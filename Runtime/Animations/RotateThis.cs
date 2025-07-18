using Sirenix.OdinInspector;
using UnityEngine;

namespace Gamecore.Animation
{
    public class RotateThis : MonoBehaviour
    {
        [SerializeField] private Vector3 axis;
        [SerializeField] private float add = 360;
        [SerializeField] private float time;
        [SerializeField] private LeanTweenType type;
        
        [SerializeField] private bool loop;
        [SerializeField, ShowIf(nameof(loop))] private LeanTweenType loopType;
        private void OnEnable()
        {
            if (loop)
            {
                gameObject.LeanRotateAroundLocal(axis, add, time).setEase(type).setLoopType(loopType);
            }
            else
            {
                gameObject.LeanRotateAroundLocal(axis, add, time).setEase(type);
            }
        }

        private void OnDisable()
        {
            gameObject.LeanCancel();
        }
    }
}

