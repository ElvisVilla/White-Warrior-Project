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
    public RuneMode RuneMode;
    Animator anim;
    bool charge = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
    }
    
    public void SetCharge()
    {
        RuneMode = RuneMode.Charge;
        anim.CrossFade("RuneCharge", 0.1f);
    }

    public void SetUncharge()
    {
        RuneMode = RuneMode.Uncharge;
        anim.CrossFade("RuneEmpty", 0.1f);
    }
}
