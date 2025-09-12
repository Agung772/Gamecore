using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Gamecore
{
    public class PopupBehaviour : MonoBehaviour
    {
        [FoldoutGroup("Base")] public bool isGlobal;
        
        [FoldoutGroup("Base")] public bool setOrder;
        [FoldoutGroup("Base"), ShowIf(nameof(setOrder)), InfoBox("Default Order = 1")] public int sortOrder = 2;

        [FoldoutGroup("Base")] [SerializeField] private bool autoClose;
        [FoldoutGroup("Base")] [SerializeField] private bool useUnScaledTime;
        [FoldoutGroup("Base")] [SerializeField, ShowIf("autoClose")] private int closeAfter;
        [FoldoutGroup("Base")] [SerializeField] private Button closeBtn;
        
        public Action onClose;
    
        public virtual void Initialize()
        {
            if (closeBtn)
            {
                closeBtn.onClick.AddListener(Close);
            }

            if (autoClose)
            {
                gameObject.LeanDelayedCall(closeAfter, Close).setIgnoreTimeScale(useUnScaledTime);;
            }
        }
    
        public virtual void Close()
        {
            onClose?.Invoke();
            Game.Get<Popup>().active.Remove(GetType());
            Destroy(gameObject);
        }
    }
}
