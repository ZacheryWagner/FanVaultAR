using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpenClose : MonoBehaviour
{
    [SerializeField] 
    private Animator mAnimator;

    [SerializeField] 
    private GameObject annotation;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("MainCamera") || other.CompareTag("Player")) {
            mAnimator.SetTrigger("TrChestOpen");
            annotation.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("MainCamera") || other.CompareTag("Player")) {
            mAnimator.SetTrigger("TrChestClose");
            annotation.SetActive(false);
        }
    }
}