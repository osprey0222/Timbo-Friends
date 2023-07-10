using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    protected enum EMovementState { Idle, Run, Jump, Fall, Death, Yeah }

    protected virtual void Awake()
    {
    }

    protected virtual void Start()
    {
    }
}
