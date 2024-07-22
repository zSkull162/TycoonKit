
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace zSkull162.TycoonKit
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class Dropper : UdonSharpBehaviour
    {
        // [Header("--- Dropper ---")]
        [Tooltip("The object which will be spawned")]
        [SerializeField] private GameObject objectInstance;
        [Tooltip("Delay between spawns.\nSet to <b>0</b> to disable automatic spawning.")]
        [SerializeField] private float spawnDelay = 1.8f;
        private bool isOn;

        void OnEnable()
        {
            isOn = true;

            if (spawnDelay <= 0) return;    // Do nothing if spawnDelay is less than 0. This makes auto-spawning disable, because it would stop here.
            else SendCustomEventDelayedSeconds(nameof(SpawnObject), spawnDelay);
        }

        private void OnDisable()
        {
            isOn = false;
        }

        public void SpawnObject()
        {
            if (!isOn) return;

            Object.Instantiate(objectInstance, this.transform.position, default);

            if (spawnDelay <= 0) return;    // Check again, so that it doesn't start auto-dropping if activated by a dropper button
            else SendCustomEventDelayedSeconds(nameof(SpawnObject), spawnDelay);
        }
    }
}
