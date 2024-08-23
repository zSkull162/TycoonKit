
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace zSkull162.TycoonKit
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class TriggerEnterRelay : UdonSharpBehaviour
    {
        // [Header("--- TriggerEnterRelay ---")]
        [SerializeField] private UdonBehaviour script;
        [SerializeField] private string eventName;

        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (!player.isLocal) return;

            if (eventName != null)
            {
                script.SendCustomEvent(eventName);
            }
            else
            {
                Logger.Log(this.name, "eventName was null!", LogColor.Red, false);
            }
        }
    }
}