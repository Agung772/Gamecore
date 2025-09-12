namespace Gamecore
{
    public class MultipopupBehaviour : PopupBehaviour
    {
        public override void Remove()
        {
            Destroy(gameObject);
        }
    }
}

