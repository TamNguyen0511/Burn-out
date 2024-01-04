using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleAvoidanceBehaviour : SteeringBehaviour
{
    [SerializeField]
    private float _radius = 2f, _agentColliderSize = 0.6f;
    [SerializeField]
    private bool _showGizmos = true;

    private float[] _dangersResultTemp = null;

    public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aiData)
    {
        foreach (var obstacleCollider in aiData.Obstacles)
        {
            Vector2 directionToObstacle =
                obstacleCollider.ClosestPoint(transform.position) - (Vector2)transform.position;
            float distanceToObstacle = directionToObstacle.magnitude;

            float weight = distanceToObstacle <= _agentColliderSize ? 1 : (_radius - distanceToObstacle) / _radius;
            Vector2 directionToObstacleNormalized = directionToObstacle.normalized;

            for (int i = 0; i < Directions.eightDirections.Count; i++)
            {
                float result = Vector2.Dot(directionToObstacleNormalized, Directions.eightDirections[i]);
                float valueToPutIn = result * weight;

                if (valueToPutIn > danger[i])
                {
                    danger[i] = valueToPutIn;
                }
            }
        }

        _dangersResultTemp = danger;
        return (danger, interest);
    }

    private void OnDrawGizmos()
    {
        if (!_showGizmos) return;

        if (Application.isPlaying && _dangersResultTemp != null)
        {
            if (_dangersResultTemp != null)
            {
                Gizmos.color = Color.red;
                for (int i = 0; i < _dangersResultTemp.Length; i++)
                {
                    Gizmos.DrawRay(transform.position, Directions.eightDirections[i] * _dangersResultTemp[i]);
                }
            }
        }
        else
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}

public static class Directions
{
    public static List<Vector2> eightDirections = new List<Vector2>
    {
        new Vector2(0, 1).normalized,
        new Vector2(1, 1).normalized,
        new Vector2(1, 0).normalized,
        new Vector2(1, -1).normalized,
        new Vector2(0, -1).normalized,
        new Vector2(-1, -1).normalized,
        new Vector2(-1, 0).normalized,
        new Vector2(-1, 1).normalized
    };
}