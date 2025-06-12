using System;
using System.Collections.Generic;
using UnityEngine;

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

        public PopupBehaviour Show<T>(PopupBehaviour.Packet packet = null) where T : PopupBehaviour
        {
            var _prefab = resources[typeof(T)];
            var _popup = GameObject.Instantiate(_prefab).GetComponent<PopupBehaviour>();
            _popup.Initialize(packet);
            if (!_popup.canMulti) active.Add(typeof(T), _popup);
            return _popup;
        }
        
        public bool Remove<T>() where T : PopupBehaviour 
        {
            if (TryGet<T>(out var _popup))
            {
                _popup.OnClose(); 
                GameObject.Destroy(_popup.gameObject);
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
                GameObject.Destroy(popup.gameObject);
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

