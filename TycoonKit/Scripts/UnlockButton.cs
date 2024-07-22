
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
        [Tooltip("<b>Not required.</b>\nThe sound that plays when this button is sucessfully bought.")]
        [SerializeField] private AudioSource soundEffect;
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
        [Tooltip("If true, the Sound Effect will play for all players when this button is bought.\n(Only if a Sound Effect is selected)")]
        [SerializeField] private bool globalSound = true;

        // [Header("--- Unlocks ---")]
        [Tooltip("<b>Required.</b>\nThe objects that will be enabled when this button is sucessfully bought.")]
        [SerializeField] private GameObject[] unlocks;
        [Tooltip("<b>Not Required.</b>\nThe previous level of whatever this button unlocks.\nThis will be disabled when this button is sucessfully bought.")]
        [SerializeField] private GameObject previousObject;
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

            CheckBought();
        }

        public void CheckBought()
        {
            float _money = moneyManager.Money;

            if (_money >= cost)
            {
                bought = true;
                _money -= cost;
                moneyManager.Money = _money;

                if (globalSound)
                {
                    SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(PlaySound));
                }
                else PlaySound();

                RequestSerialization();
                moneyManager.RequestSerialization();

                Apply();
            }

            Logger.Log(this.name, $"bought = {bought}", LogColor.Aqua, false);
        }

        public override void OnDeserialization()
        {
            Logger.Log(this.name, "OnDeserialization called", LogColor.Orange, false);
            Apply();
        }

        public void Apply()
        {
            Logger.Log(this.name, "Apply Called", LogColor.Orange, false);

            foreach (var item in unlocks)
            {
                item.SetActive(bought);
            }

            if (previousObject != null) previousObject.SetActive(!bought);

            containerObject.SetActive(!bought); // Disable the container object, so that you can't buy this button multiple times.
            // The container has to be disabled instead of this object itself, because scripts on disabled objects don't sync for latejoiners.

            Logger.Log(this.name, $"Apply finished, bought = {bought}", LogColor.Lime, true);
        }

        public void PlaySound()
        {
            if (soundEffect != null && bought) { soundEffect.PlayOneShot(soundEffect.clip); }
        }

        #region Getters
        ///////////////////// This allows the custom inspector to access these private variables
        public MoneyManager MoneyManager
        {
            set { moneyManager = value; }
            get { return moneyManager; }
        }
        public GameObject ContainerObject
        {
            set { containerObject = value; }
            get { return containerObject; }
        }
        public GameObject PreviousObject
        {
            get { return previousObject; }
        }
        public UnityEngine.UI.Text TitleText
        {
            get { return titleText; }
        }
        public UnityEngine.UI.Text CostText
        {
            get { return costText; }
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
