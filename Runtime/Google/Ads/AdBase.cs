#if GOOGLE_MOBILE

namespace Gamecore.Google
{
    public class AdBase
    {
        public virtual void Initialize() { }
        public virtual bool CanShow() { return false; }
    }
}

#endif