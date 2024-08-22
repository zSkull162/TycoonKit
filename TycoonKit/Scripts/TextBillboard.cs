using UnityEngine;
using UdonSharp;
using VRC.SDKBase;

namespace zSkull162.TycoonKit
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class TextBillboard : UdonSharpBehaviour
    {
        private VRCPlayerApi localPlayer;
        private Quaternion head;

        private void Start()
        {
            localPlayer = Networking.LocalPlayer;
        }

        void LateUpdate()
        {
            head = localPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).rotation;
            transform.LookAt(transform.position + head * Vector3.forward, Vector3.up);
        }
    }
}