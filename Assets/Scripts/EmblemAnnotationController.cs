using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmblemAnnotationController : MonoBehaviour
{
    [SerializeField] 
    private GameObject annotation;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("MainCamera") || other.CompareTag("Player")) {
            annotation.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("MainCamera") || other.CompareTag("Player")) {
            annotation.SetActive(false);
        }
    }
}