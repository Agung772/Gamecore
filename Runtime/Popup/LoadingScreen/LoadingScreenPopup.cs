using UnityEngine;

namespace Gamecore
{
    public class LoadingScreenPopup : PopupBehaviour
    {
        public override void Initialize(PopupPacket packet = null)
        {
            base.Initialize(packet);
            Time.timeScale = 0f;
        }

        public override void OnClose()
        {
            base.OnClose();
            Time.timeScale = 1f;
        }
    }
}
