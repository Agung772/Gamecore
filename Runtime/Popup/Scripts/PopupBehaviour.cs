using System;
using Core;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class PopupBehaviour : MonoBehaviour
{
    [FoldoutGroup("Base")] public bool canMulti;
    [FoldoutGroup("Base")] [SerializeField] private bool autoClose;
    [FoldoutGroup("Base")] [SerializeField, ShowIf("autoClose")] private int closeAfter;
    [FoldoutGroup("Base")] [SerializeField] private Button closeBtn;
    public Packet packet;
    
    public class Packet
    {
        public Action onClose;
    }
    
    public virtual void Initialize(Packet packetPopup = null)
    {
        packet = packetPopup;
        if (closeBtn)
        {
            closeBtn.onClick.AddListener(() => Game.Get<Popup>().Remove(this));
        }

        if (autoClose)
        {
            gameObject.LeanDelayedCall(closeAfter, () =>
            {
                if (canMulti)
                {
                    Destroy(gameObject);
                }
                else
                {
                    Game.Get<Popup>().Remove(this);
                }
            });
        }
    }
    
    public virtual void OnClose()
    {
        if (packet != null)
        {
            packet.onClose?.Invoke();
        }
    }
}