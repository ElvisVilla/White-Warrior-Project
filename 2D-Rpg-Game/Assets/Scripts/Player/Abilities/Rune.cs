using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum RuneMode
{
    Charge, Uncharge,
}

public class Rune : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    
    public void SetCharge()
    {
        anim.CrossFade("RuneCharge", 0.1f);
    }

    public void UnCharge()
    {
        anim.CrossFade("RuneEmpty", 0.1f);
    }
}
