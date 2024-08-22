
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace zSkull162.TycoonKit
{
    public enum UpgradeType
    {
        Add,
        Multiply
    }

    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class Upgrader : UdonSharpBehaviour
    {
        // [Header("--- Upgrader ---")]
        [Tooltip("Whether the object's value will be added to or multiplied by the Upgrade Amount.")]
        [SerializeField] private UpgradeType type;
        [Tooltip("The number by which the object's value will be added to or multiplied by.")]
        [SerializeField] private float upgradeAmount;
        [Tooltip("<b>Not required.</b>\nThe text which will display the Upgrade Amount.")]
        [SerializeField] private UnityEngine.UI.Text displayText;

        // [Header("--- Effects --")]
        [Tooltip("<b>Not required.</b>\nThe particle system that will play when an object is upgraded.")]
        [SerializeField] private ParticleSystem upgradeParticles;

        private void Start()
        {
            if (displayText == null) return;

            if (type == UpgradeType.Add)
            {
                displayText.text = upgradeAmount.ToString("+#,###.#");
            }
            else if (type == UpgradeType.Multiply) {
                displayText.text = upgradeAmount.ToString("x#,###.#");
            }
        }

        private void OnEnable()    // If for whatever reason you want a negative upgrade amount, just delete the entire OnEnable funtion
        {
            if (upgradeAmount < 0)
            {
                upgradeAmount = 0;
                Logger.Log(this.name, $"Upgrade amount can't be negative! Upgrade amount reset to <i>{upgradeAmount}</i>", LogColor.Red, false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            CurrencyTag cTag = other.GetComponent<CurrencyTag>();    // I return immediately here if there's no currency tag, so we don't run any excess code on a non-currency object
            if (cTag == null) { Logger.Log(this.name, "CurrencyTag not found!", LogColor.Red, false); return; }

            float newValue = cTag.Value;

            if (type == UpgradeType.Add) { newValue += upgradeAmount; }
            if (type == UpgradeType.Multiply) { newValue *= upgradeAmount; }

            if (upgradeParticles != null) { upgradeParticles.Play(); }

            cTag.Value = newValue;

            Logger.Log(this.name, $"Object <b>{other.name}</b> was upgraded! Its value is now: <i>{newValue}</i>", LogColor.Aqua, true);
        }
    }
}
