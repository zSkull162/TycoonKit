
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace zSkull162.TycoonKit
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class MoneyGiver : UdonSharpBehaviour
    {
        #region PublicVariables
        // [Header("--- System ---")]
        [Tooltip("<b>Required.</b>\nThe udon behaviour for the Money Manager")]
        [SerializeField] private MoneyManager moneyManager;
        [Tooltip("<b>Not Required.</b>\nThe sound effect which plays when a player enters this trigger")]
        [SerializeField] private AudioSource soundEffect;
        [Tooltip("Where the player teleports after hitting this trigger.\nIf left unassigned, they will respawn instead.")]
        [SerializeField] private Transform teleportPosition;
        [Tooltip("<b>Not Required.</b>\nThe text that will display the win amount.")]
        [SerializeField] private UnityEngine.UI.Text displayText;
        // [Header("--- Main ---")]
        [Tooltip("How much the player wins for hitting this trigger")]
        [SerializeField] private float winAmount;
        [Tooltip("If true, the Sound Effect will play for all players\n(Only if a Sound Effect is selected)")]
        [SerializeField] private bool globalSound = true;
        #endregion
        private VRCPlayerApi localPlayer;

        private void Start()
        {
            localPlayer = Networking.LocalPlayer;

            if (winAmount < 0)
            {
                winAmount = 0;
                Logger.Log(this.name, "Win amount cannot be negative!", LogColor.Red, false);
            }

            if (displayText != null) displayText.text = winAmount.ToString("Win: $#,###.#");
        }

        public void _PlayerTriggerEnter()
        {
            Networking.SetOwner(localPlayer, moneyManager.gameObject);    // If a player wants to change the Money variable, they need to be the owner of the Money Manager.

            AddMoney();
        }

        public void AddMoney()
        {
            float _money = moneyManager.Money;

            _money += winAmount;
            moneyManager.Money = _money;

            moneyManager.RequestSerialization();
            Logger.Log(this.name, $"Obby complete! Amount gained: <i>{_money}</i>", LogColor.Lime, true);

            TeleportPlayer();

            if (globalSound)
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(PlaySound));
            }
            else PlaySound();
        }

        public void PlaySound()
        {
            if (soundEffect == null) return;    // Do nothing if there's no selected sound effect

            soundEffect.transform.position = this.transform.position;

            soundEffect.Play();
        }

        private void TeleportPlayer()
        {
            if (teleportPosition != null)
            {
                localPlayer.TeleportTo(teleportPosition.position, teleportPosition.rotation);
            }
            else
            {
                localPlayer.Respawn();
            }
        }

        #region Getters
        ///////////////////// This allows the custom inspector to access these private variables
        public MoneyManager MoneyManager
        {
            set { moneyManager = value; }
            get { return moneyManager; }
        }
        public float WinAmount
        {
            get { return winAmount; }
        }
        public UnityEngine.UI.Text DisplayText
        {
            get { return displayText; }
        }
        #endregion
    }
}