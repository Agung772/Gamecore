using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace ACore.Animation
{
    public class ColorThis : AnimationBase
    {
        [SerializeField] private bool isFrom;
        [SerializeField, ShowIf(nameof(isFrom))] private Color from = Color.white;
        [SerializeField] private Color to = Color.white;
        
        [SerializeField] private float time = 1;
        [SerializeField] private LeanTweenType type;
        
        private SpriteRenderer spriteRenderer;
        private Image image;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            image = GetComponent<Image>();
            
            if (isFrom && !base.autoPlay)
            {
                if (spriteRenderer) spriteRenderer.color = from;
                else if (image) image.color = from;
            }
        }

        public override void Play()
        {
            base.Stop();
            if (isFrom && base.autoPlay)
            { 
                if (spriteRenderer) spriteRenderer.color = from;
                else if (image) image.color = from;
            }

            if (spriteRenderer) base.descr = gameObject.LeanColor(to, time).setEase(type).setIgnoreTimeScale(base.useUnScaledTime);
            else if (image) base.descr = image.LeanColor(to, time).setEase(type).setIgnoreTimeScale(base.useUnScaledTime);
            
            base.Play();
        }

        public override void ToDefault(bool fasted = false)
        {
            base.Stop();
            base.ToDefault(fasted);
            if (isFrom)
            {
                if (fasted)
                {
                    if (spriteRenderer) spriteRenderer.color = from;
                    else if (image) image.color = from;
                }
                else
                {
                    if (spriteRenderer) gameObject.LeanColor(from, time).setEase(type).setIgnoreTimeScale(base.useUnScaledTime);
                    else if (image) image.LeanColor(from, time).setEase(type).setIgnoreTimeScale(base.useUnScaledTime);
                }
            }
        }
    }
}
