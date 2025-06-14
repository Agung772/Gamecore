using System;
using UnityEngine;

namespace Core.Animation
{
    public class RoteteThis : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Vector3 axis;
        private void OnEnable()
        {
            gameObject.LeanRotateAroundLocal(axis, 360, speed).setLoopClamp();
        }

        private void OnDisable()
        {
            gameObject.LeanCancel();
        }
    }
}

