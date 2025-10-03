using UnityEngine;

namespace ACore
{
    [DisallowMultipleComponent]
    public class WorldCanvasGroup : MonoBehaviour
    {
        [Range(0f, 1f)]
        [SerializeField] private float m_alpha = 1f;
        public float alpha
        {
            get => m_alpha;
            set
            {
                m_alpha = value;
                ApplyAlpha();
            }
        }
        
        [SerializeField] private bool m_blocksRaycasts = true;
        public bool blocksRaycasts
        {
            get => m_blocksRaycasts;
            set
            {
                m_blocksRaycasts = value;
                ApplyInteractable();
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            ApplyAlpha();
            ApplyInteractable();
        }
#endif

        private void ApplyAlpha()
        {
            var _spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
            if (_spriteRenderers == null) return;
    
            foreach (var _sr in _spriteRenderers)
            {
                if (_sr == null) continue;
                var _c = _sr.color;
                _c.a = m_alpha;
                _sr.color = _c;
            }
        }
    
        private void ApplyInteractable()
        {
            var _colliders = GetComponentsInChildren<Collider2D>(true);
            if (_colliders == null) return;
    
            foreach (var _col in _colliders)
            {
                if (_col == null) continue;
                _col.enabled = m_blocksRaycasts;
            }
        }
    }
}
