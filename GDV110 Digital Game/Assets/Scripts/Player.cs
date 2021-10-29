using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed_current;
    public float moveSpeed_default;

    [Header("States")]
    public bool isGrappling;
}
