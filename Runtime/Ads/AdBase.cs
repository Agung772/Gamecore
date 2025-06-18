namespace Core.Ads
{
    public class AdBase
    {
        public virtual void Initialize() { }
        public virtual bool IsCanShow() { return false; }
    }
}