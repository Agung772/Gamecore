using UnityEngine;

namespace Gamecore
{
    public class LocalBehaviour : MonoBehaviour, IBehaviour
    {
        public virtual void OnAwake() { }
        public virtual void OnStart() { }
    }
}

