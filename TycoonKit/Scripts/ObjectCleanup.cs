
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace zSkull162.TycoonKit
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ObjectCleanup : UdonSharpBehaviour
    {
        // [Header("--- Object Cleaner ---")]
        [Tooltip("The name which will be looked for before deleting an object.\nInternally, it checks whether the object's name <b>contains</b> this string.")]
        [SerializeField] private string objName;

        private void OnTriggerEnter(Collider other)
        {
            if (other.name.Contains(objName))
            {
                GameObject.Destroy(other.gameObject);
            }
        }
    }
}
