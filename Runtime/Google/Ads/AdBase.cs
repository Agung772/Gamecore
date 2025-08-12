namespace Gamecore.Ads
{
    public class AdBase
    {
        public virtual void Initialize() { }
        public virtual bool IsCanShow() { return false; }
    }
}