using UnityEngine;

namespace Gamecore
{
    public static class ScreenToWorldUtility
    {
        private static Camera MainCamera => Camera.main;
        public static float GetFullScaleX()
        {
            return -MainCamera.ViewportToWorldPoint(new Vector3(0f, 0f)).x * 2;
        }
        public static float GetFullScaleY()
        {
            return -MainCamera.ViewportToWorldPoint(new Vector3(0f, 0f)).y * 2;
        }
        public static float GetSideX(float offset = 0)
        {
            return -MainCamera.ViewportToWorldPoint(new Vector3(0f, 0f)).x + offset;
        }
        public static float GetCenterX(float offset = 0)
        {
            return MainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0f)).x + offset;
        }
        public static int DirectionOutOfSide(float target, float position, float center = 0, float offset = 0)
        {
            if (position > center)
            {
                if (target > GetSideX(offset)) return -1;
            }
            else
            {
                if (target < -GetSideX(offset)) return 1;
            }
            return 0;
        }
    }
}