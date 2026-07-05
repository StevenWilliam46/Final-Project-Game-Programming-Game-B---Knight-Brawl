using System.Collections.Generic;
using UnityEngine;

namespace Apps.Scripts
{
    public class ObjectDetector : MonoBehaviour
    {
        public List<Health> detectedObjects;

        private void Awake()
        {
            detectedObjects = new List<Health>();
        }

        private void OnTriggerEnter(Collider other)
        {
            Health obj = other.gameObject.GetComponent<Health>();
            if (obj is null)
                return;
        
            detectedObjects.Add(obj);
        }

        private void OnTriggerExit(Collider other)
        {
            Health obj = other.gameObject.GetComponent<Health>();
            if (obj is null)
                return;
            detectedObjects.Remove(obj);
        }
    }
}