using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIData : MonoBehaviour
{
    public List<Transform> Targets = null;
    public Collider2D[] Obstacles = null;

    public Transform CurrentTarget;

    public int GetTargetsCount() => Targets == null ? 0 : Targets.Count;

}
