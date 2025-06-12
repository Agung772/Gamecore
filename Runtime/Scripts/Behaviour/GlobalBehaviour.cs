namespace Core
{
    public class GlobalBehaviour : IBehaviour
    {
        public virtual int Order { get; set; }
        public virtual void Initialize() { }
    }
}
