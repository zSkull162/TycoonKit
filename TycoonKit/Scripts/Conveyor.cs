
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace zSkull162.TycoonKit
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class Conveyor : UdonSharpBehaviour
    {
        // [Header("--- Conveyor ---")]
        [Tooltip("The name of the objects which the conveyor will affect.\n(It searches for this string when an object is in the trigger area.)")]
        [SerializeField] private string objectName;
        [Tooltip("How much force on what axis will be applied to the objects.\n(Global orientation)")]
        [SerializeField] private Vector3 force;

        private void OnTriggerStay(Collider other)
        {
            if (other.name.Contains(objectName))
            {
                Rigidbody _rigidbody = other.GetComponent<Rigidbody>();
                _rigidbody.AddForce(force);
            }
        }
    }
}
