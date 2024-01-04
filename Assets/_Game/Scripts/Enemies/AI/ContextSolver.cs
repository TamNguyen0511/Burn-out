using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ContextSolver : MonoBehaviour
{
    [SerializeField]
    private bool _showGizmos = true;

    private float[] _interestGizmos = new float[0];
    private Vector2 _resultDirection = Vector2.zero;
    private float _rayLength = 1;

    private void Start()
    {
        _interestGizmos = new float[8];
    }

    public Vector2 GetDirectionToMove(List<SteeringBehaviour> behaviours, AIData aiData)
    {
        float[] danger = new float[8];
        float[] interest = new float[8];

        foreach (SteeringBehaviour behaviour in behaviours)
            (danger, interest) = behaviour.GetSteering(danger, interest, aiData);

        for (int i = 0; i < 8; i++)
            interest[i] = Mathf.Clamp01(interest[i] - danger[i]);
        
        _interestGizmos = interest;

        Vector2 outputDirection = Vector2.zero;

        for (int i = 0; i < 8; i++)
            outputDirection += Directions.eightDirections[i] * interest[i];
        outputDirection.Normalize();

        _resultDirection = outputDirection;

        return _resultDirection;
    }

    private void OnDrawGizmos()
    {
        if (!_showGizmos) return;
        if (!Application.isPlaying) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, _resultDirection * _rayLength);
    }
}