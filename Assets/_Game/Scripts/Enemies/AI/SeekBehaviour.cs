using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeekBehaviour : SteeringBehaviour
{
    [SerializeField]
    private float _targetRechedThreshold = 0.5f;
    [SerializeField]
    private bool _showGizmos = true;

    private bool _reachedLastTarget = true;

    ///  Gizmos paramerters
    private Vector2 _targetPositionCached;
    private float[] _interestsTemp;

    public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aiData)
    {
        if (_reachedLastTarget)
        {
            if (aiData.Targets == null || aiData.Targets.Count <= 0)
            {
                aiData.CurrentTarget = null;
                return (danger, interest);
            }
            else
            {
                _reachedLastTarget = false;
                aiData.CurrentTarget = aiData.Targets
                    .OrderBy(target => Vector2.Distance(target.position, transform.position)).First();
            }
        }

        if (aiData.CurrentTarget != null && aiData.Targets != null && aiData.Targets.Contains(aiData.CurrentTarget))
            _targetPositionCached = aiData.CurrentTarget.position;

        if (Vector2.Distance(transform.position, _targetPositionCached) < _targetRechedThreshold)
        {
            _reachedLastTarget = true;
            aiData.CurrentTarget = null;
            return (danger, interest);
        }

        Vector2 directionToTarget = (_targetPositionCached - (Vector2)transform.position);
        for (int i = 0; i < interest.Length; i++)
        {
            float result = Vector2.Dot(directionToTarget.normalized, Directions.eightDirections[i]);

            if (result > 0)
            {
                float valueToPutIn = result;

                if (valueToPutIn > interest[i])
                    interest[i] = valueToPutIn;
            }
        }

        _interestsTemp = interest;
        return (danger, interest);
    }

    private void OnDrawGizmos()
    {
        if (!_showGizmos) return;

        Gizmos.DrawSphere(_targetPositionCached, 0.2f);
        if (Application.isPlaying && _interestsTemp != null)
        {
            if (_interestsTemp != null)
            {
                Gizmos.color = Color.green;
                for (int i = 0; i < _interestsTemp.Length; i++)
                    Gizmos.DrawRay(transform.position, Directions.eightDirections[i] * _interestsTemp[i]);
                if (!_reachedLastTarget)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(_targetPositionCached, 0.1f);
                }
            }
        }
    }
}