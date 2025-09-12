using UnityEngine;

namespace Gamecore
{
    public class LoadingScreenPopup : PopupBehaviour
    {
        public override void Initialize()
        {
            base.Initialize();
            Time.timeScale = 0f;
        }

        public override void Close()
        {
            base.Close();
            Time.timeScale = 1f;
        }
    }
}
