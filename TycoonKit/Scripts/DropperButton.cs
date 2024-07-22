
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace zSkull162.TycoonKit
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.NoVariableSync)]
    public class DropperButton : UdonSharpBehaviour
    {
        // [Header("--- Dropper Button ---")]
        [Tooltip("The spawner that will be activated by this button.")]
        [SerializeField] private Dropper spawner;
        [Tooltip("The time the button will disabled between-clicks.\nSet to <b>0</b> to disable the cooldown.")]
        [SerializeField] private float cooldown = 0.3f;
        [Tooltip("<b>Not Required.</b>\nThe sound that will play when this button is clicked.")]
        [SerializeField] private AudioSource sound;
        private bool useCooldown = true;

        private void Start()
        {
            if (cooldown <= 0) useCooldown = false;
        }

        public override void Interact()
        {
            if (sound != null) { sound.PlayOneShot(sound.clip); }    // PlayOneShot instead of Play, will make the sound overlap instead of cutting out

            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(ButtonClicked));
        }

        public void ButtonClicked()
        {
            spawner.SpawnObject();

            if (useCooldown)
            {
                this.DisableInteractive = true;

                SendCustomEventDelayedSeconds(nameof(EnableInteractive), cooldown);
            }
        }

        public void EnableInteractive()
        {
            this.DisableInteractive = false;
        }
    }
}
