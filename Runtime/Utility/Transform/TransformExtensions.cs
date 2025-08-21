using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamecore
{
    public static class TransformExtensions
    {
        public static void DestroyAllChildren(this Transform parent, bool immediate = false)
        {
            if (parent == null) return;
            
            foreach (Transform _child in parent)
            {
                if (immediate)
                    Object.DestroyImmediate(_child.gameObject);
                else
                    Object.Destroy(_child.gameObject);
            }
        }
        
        public static void DestroyFirstChild(this Transform parent, bool immediate = false)
        {
            if (parent == null) return;
            if (parent.childCount == 0) return;

            var _child = parent.GetChild(0).gameObject;
            if (immediate)
                Object.DestroyImmediate(_child);
            else
                Object.Destroy(_child);
        }
        
        public static void DestroyLastChild(this Transform parent, bool immediate = false)
        {
            if (parent == null) return;
            if (parent.childCount == 0) return;

            var _child = parent.GetChild(parent.childCount - 1).gameObject;
            if (immediate)
                Object.DestroyImmediate(_child);
            else
                Object.Destroy(_child);
        }
    }
}
