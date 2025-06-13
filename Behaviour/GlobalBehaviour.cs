using System.Collections;

namespace Core
{
    public class GlobalBehaviour : IBehaviour
    {
        public virtual int Order { get; set; }
        public virtual IEnumerator InitializeCoroutine() { yield break; }
        public virtual void Initialize() { }
        public virtual void PostInitialize() { }

    }
}
