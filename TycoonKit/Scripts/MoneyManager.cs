
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace zSkull162.TycoonKit
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Continuous)]
    public class MoneyManager : UdonSharpBehaviour
    {
        // [Header("--- Money Manager ---")]
        [Tooltip("<b>Not required.</b>\nThe text which will display the current money value.")]
        [SerializeField] private UnityEngine.UI.Text displayText;
        [Tooltip("How much money will be added on start")]
        [SerializeField] private float startingMoney = 0f;

        [UdonSynced, FieldChangeCallback(nameof(Money))] private float money;


        private void Start() { Money = startingMoney; }

        public void ResetMoney() { Money = 0f; }

        public float Money
        {
            set
            {
                money = value;
                if (money < 0f) { ResetMoney(); }

                if (displayText != null) { displayText.text = money.ToString("$#,####"); }
            }
            get { return money; }
        }
    }
}
