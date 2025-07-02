using System;
using UnityEngine;

namespace Gamecore.Animation
{
    public class RotateThis : MonoBehaviour
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

