
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace zSkull162.TycoonKit
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class InteractionRelay : UdonSharpBehaviour
    {
        // [Header("--- InteractionRelay ---")]
        [SerializeField] private UdonBehaviour script;
        [SerializeField] private string eventName;

        public override void Interact()
        {
            script.SendCustomEvent(eventName);
        }
    }
}
