using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{
    public class Popup : GlobalBehaviour
    {
        private Dictionary<Type, PopupBehaviour> active = new();
        private Dictionary<Type, PopupBehaviour> resources = new();

        public override void Initialize()
        {
            var _popups = Resources.LoadAll<PopupBehaviour>("");
            foreach (var _popup in _popups)
            {
                resources.Add(_popup.GetType(), _popup);
            }
        }

        public PopupBehaviour Show<T>(PopupPacket popupPacket = null) where T : PopupBehaviour
        {
            var _prefab = resources[typeof(T)];
            PopupBehaviour _popup;
            if (popupPacket is PopupWorldPacket) _popup = Object.Instantiate(_prefab).GetComponent<PopupBehaviour>();
            else _popup = Object.Instantiate(_prefab, Game.Manager.Canvas).GetComponent<PopupBehaviour>();
            _popup.Initialize(popupPacket);
            if (_popup is not MultipopupBehaviour) active.Add(typeof(T), _popup);
            return _popup;
        }
        
        public bool Remove<T>() where T : PopupBehaviour 
        {
            if (TryGet<T>(out var _popup))
            {
                _popup.OnClose(); 
                Object.Destroy(_popup.gameObject);
                active.Remove(typeof(T));
                return true;
            }
            
            return false;
        }
        
        public bool Remove(PopupBehaviour popup) 
        {
            if (active.ContainsKey(popup.GetType()))
            {
                popup.OnClose(); 
                Object.Destroy(popup.gameObject);
                active.Remove(popup.GetType());
                return true;
            }
            
            return false;
        }
        
        public bool IsActive<T>() where T : PopupBehaviour
        {
            return active.ContainsKey(typeof(T));
        }

        public bool TryGet<T>(out T popup) where T : PopupBehaviour
        {
            if (IsActive<T>())
            {
                popup = active[typeof(T)] as T;
                return true;
            }
            
            popup = null;
            return false;
        }
        
        public T Get<T>() where T : PopupBehaviour
        {
            TryGet<T>(out var _popup);
            return _popup;
        }
    }
}

