
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace zSkull162.TycoonKit
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class Collector : UdonSharpBehaviour
    {
        // [Header("--- Collector ---")]
        [Tooltip("<b>Required.</b>\nThe udon behaviour for the Money Manager")]
        [SerializeField] private MoneyManager moneyManager;

        private void OnTriggerEnter(Collider other)
        {
            CurrencyTag cTag = other.GetComponent<CurrencyTag>();
            if (cTag == null) { Logger.Log(this.name, "CurrencyTag not found!", LogColor.Red, false); return; }

            float _money = moneyManager.Money;
            float objectValue = cTag.Value;
            
            _money += objectValue;
            moneyManager.Money = _money;
            moneyManager.RequestSerialization();

            GameObject.Destroy(other.gameObject);

            Logger.Log(this.name, $"Object <b>{other.name}</b> was collected with a value of <i>{objectValue}!</i>", LogColor.Lime, true);
        }

        public MoneyManager MoneyManager
        {
            set { moneyManager = value; }
            get { return moneyManager; }
        }
    }
}
