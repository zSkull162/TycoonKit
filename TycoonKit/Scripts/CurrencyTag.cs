
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace zSkull162.TycoonKit
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Continuous)]
    public class CurrencyTag : UdonSharpBehaviour
    {
        // [Header("--- Currency Tag ---")]
        [Tooltip("How much this object is worth")]
        [SerializeField] private float objectValue;

        private void OnEnable()    // If for whatever reason you want a negative currency value, just delete the entire OnEnable funtion
        {
            if (Value < 0)
            {
                Value = 0;
                Logger.Log(this.name, $"Value can't be negative! Value reset to <i>{objectValue}</i>", LogColor.Red, false);
            }
        }

        public float Value
        {
            set { objectValue = value; }
            get { return objectValue; }
        }
    }
}
