using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    [Header("XP Configuration")] 
    [Tooltip("Experience  enemy drop")] [SerializeField]
    private float expDrop;

    public float ExpDrop => expDrop;
}
