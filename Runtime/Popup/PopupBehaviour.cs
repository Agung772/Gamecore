using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Gamecore
{
    public class PopupBehaviour : MonoBehaviour
    {
        [FoldoutGroup("Base")] public bool isGlobal;
        [FoldoutGroup("Base")] [SerializeField] private bool autoClose;
        [FoldoutGroup("Base")] [SerializeField, ShowIf("autoClose")] private int closeAfter;
        [FoldoutGroup("Base")] [SerializeField] private Button closeBtn;
        public PopupPacket popupPacket;
    
        public virtual void Initialize(PopupPacket packet = null)
        {
            popupPacket = packet;
            if (closeBtn)
            {
                closeBtn.onClick.AddListener(() => Game.Get<Popup>().Remove(this));
            }

            if (autoClose)
            {
                gameObject.LeanDelayedCall(closeAfter, () =>
                {
                    Remove();
                });
            }
        }

        public virtual void Remove()
        {
            Game.Get<Popup>().Remove(this);
        }
    
        public virtual void OnClose()
        {
            if (popupPacket != null)
            {
                popupPacket.onClose?.Invoke();
            }
        }
    }
}
