namespace Gamecore
{
    public class MultiPopupBehaviour : PopupBehaviour
    {
        public override void Remove()
        {
            Destroy(gameObject);
        }
    }
}

