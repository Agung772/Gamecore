using UnityEngine;

public class WarningPopup : PopupBehaviour
{
    public class WarningPacket : Packet
    {
        public Vector3 position;
    }

    [SerializeField] private GameObject circle;
    public override void Initialize(Packet packetPopup = null)
    {
        base.Initialize(packetPopup);
        circle.LeanRotateZ(180f, 2f).setLoopClamp();
        gameObject.LeanScale(Vector3.one * 0.9f, 0.5f).setEase(LeanTweenType.easeInQuad).setLoopCount(2);
        if (packetPopup != null)
        {
            var _packet = packetPopup as WarningPacket;
            transform.position = _packet.position;
        }
    }
}
