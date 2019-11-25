using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehaviour : MonoBehaviour
{
    public float weighting = 7.5f;
    public abstract Vector3 GetForce(AI owner);
}