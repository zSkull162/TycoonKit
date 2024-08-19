
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace zSkull162.TycoonKit
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class UnlockButton : UdonSharpBehaviour
    {
        #region Public Variables
        // Technically they aren't public, but they are accessible from the inspector.
        // [Header("--- System ---")]
        [Tooltip("<b>Required.</b>\nThe udon behaviour for the Money Manager")]
        [SerializeField] private MoneyManager moneyManager;
        [Tooltip("The container object for the button's mesh, trigger, and text.")]
        [SerializeField] private GameObject containerObject;
        [Tooltip("The text that displays the Unlock Name")]
        [SerializeField] private UnityEngine.UI.Text titleText;
        [Tooltip("The text that displays the Cost")]
        [SerializeField] private UnityEngine.UI.Text costText;

        // [Header("--- Main ---")]
        [Tooltip("The text that will be displayed above the cost.")]
        [SerializeField] private string unlockName;
        [Tooltip("How much this button costs to buy.")]
        [SerializeField] private float cost;

        // [Header("--- Unlocks ---")]
        [Tooltip("<b>Required.</b>\nThe objects that will be enabled when this button is sucessfully bought.")]
        [SerializeField] private GameObject[] unlocks;
        [Tooltip("<b>Not Required.</b>\nThe previous level of whatever this button unlocks.\nThis will be disabled when this button is sucessfully bought.")]
        [SerializeField] private GameObject previousObject;

        // [Header("--- Audio ---")]
        [Tooltip("The Audio Source for the sound effects of this button.")]
        [SerializeField] private AudioSource audioSource;
        [Tooltip("The sound that plays when this button is purchased.")]
        [SerializeField] private AudioClip buySound;
        [Tooltip("Whether or not the buy sound should play for everyone.")]
        [SerializeField] private bool isBuySoundGlobal = true;
        [Tooltip("The sound that plays when this button is stepped on, but not purchased.")]
        [SerializeField] private AudioClip errorSound;
        [Tooltip("Whether or not the error sound should play for everyone.")]
        [SerializeField] private bool isErrorSoundGlobal = false;

        // Bools for the inspector
        public bool editorOptions;
        public bool isUpgrade;
        public bool useBuySound;
        public bool useErrorSound;
        #endregion

        [UdonSynced] private bool bought = false;
        private VRCPlayerApi localPlayer;
        private BoxCollider thisTrigger;

        private void Start()
        {
            thisTrigger = this.GetComponent<BoxCollider>();
            localPlayer = Networking.LocalPlayer;

            if (costText != null)
            {
                costText.text = cost.ToString("$#,####.#");
            }

            if (titleText != null)
            {
                titleText.text = string.Format("{0}:", unlockName);
            }
        }

        public void _PlayerTriggerEnter()
        {
            Networking.SetOwner(localPlayer, this.gameObject);
            Networking.SetOwner(localPlayer, moneyManager.gameObject);

            _CheckBought();
        }

        public void _CheckBought()
        {
            float _money = moneyManager.Money;

            if (_money >= cost)
            {
                if (audioSource != null && useBuySound) PlaySound("buy");

                bought = true;
                _money -= cost;
                moneyManager.Money = _money;

                RequestSerialization();
                moneyManager.RequestSerialization();

                _Apply();
            }
            else
            {
                if (audioSource != null && useErrorSound) PlaySound("error");
            }
            Logger.Log(this.name, $"bought = {bought}", LogColor.Aqua, false);
        }

        public override void OnDeserialization()
        {
            Logger.Log(this.name, "OnDeserialization called", LogColor.Orange, false);
            _Apply();
        }

        public void _Apply()
        {
            Logger.Log(this.name, "Apply Called", LogColor.Orange, false);

            foreach (var item in unlocks)
            {
                item.SetActive(bought);
            }
            if (previousObject != null && isUpgrade) previousObject.SetActive(!bought);

            containerObject.SetActive(!bought); // Disable the container object, so that you can't buy this button multiple times.
            // The container has to be disabled instead of this object itself, because scripts on disabled objects don't sync for latejoiners.

            Logger.Log(this.name, $"Apply finished, bought = {bought}", LogColor.Lime, true);
        }

        #region Play Sound
        private void PlaySound(string name)
        {
            if (name == "buy")
            {
                if (isBuySoundGlobal) SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(PlayBuySound));
                else PlayBuySound();
            }
            if (name == "error")
            {
                if (isErrorSoundGlobal) SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(PlayErrorSound));
                else PlayErrorSound();
            }
        }

        public void PlayBuySound()
        {
            audioSource.PlayOneShot(buySound);
        }
        public void PlayErrorSound()
        {
            audioSource.PlayOneShot(errorSound);
        }
        #endregion

        #region Getters
        ///////////////////// This allows the custom inspector to access these private variables
        public MoneyManager MoneyManager
        {
            get { return moneyManager; }
        }
        public GameObject ContainerObject
        {
            get { return containerObject; }
        }
        public AudioSource AudioSource
        {
            get { return audioSource; }
        }
        public GameObject PreviousObject
        {
            get { return previousObject; }
        }
        public UnityEngine.UI.Text TitleText
        {
            set { titleText = value; }
            get { return titleText; }
        }
        public UnityEngine.UI.Text CostText
        {
            set { costText = value; }
            get { return costText; }
        }
        public AudioClip BuySound
        {
            get { return buySound; }
        }
        public AudioClip ErrorSound
        {
            get { return errorSound; }
        }
        public string UnlockName
        {
            get { return unlockName; }
        }
        public float Cost
        {
            get { return cost; }
        }
        #endregion
    }
}
