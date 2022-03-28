using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpenClose : MonoBehaviour
{
    private Animator mAnimator;

    void Start()
    {
        mAnimator = GetComponent<Animator>();

        mAnimator.SetTrigger("TrGateOpen");
    }
}