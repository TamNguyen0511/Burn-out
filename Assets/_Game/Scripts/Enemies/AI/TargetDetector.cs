using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class TargetDetector : Detector
{
    [SerializeField]
    private float _targetDectectionRange = 5;

    [SerializeField]
    private LayerMask _obstacleLayerMask, _playerLayerMask;
    [SerializeField]
    private bool _showGizmos = false;

    private List<Transform> _colliders;

    public override void Detect(AIData aiData)
    {
        Collider2D playerCollider =
            Physics2D.OverlapCircle(transform.position, _targetDectectionRange, _playerLayerMask);
        if (playerCollider != null)
        {
            Vector2 direction = (playerCollider.transform.position - transform.position).normalized;
            RaycastHit2D hit =
                Physics2D.Raycast(transform.position, direction, _targetDectectionRange, _obstacleLayerMask);

            if (hit.collider != null && (_playerLayerMask & (1 << hit.collider.gameObject.layer)) != 0)
            {
                Debug.DrawRay(transform.position, direction * _targetDectectionRange, Color.magenta);
                _colliders = new List<Transform>() { playerCollider.transform };
            }
            else
            {
                _colliders = null;
            }

            aiData.Targets = _colliders;
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (!_showGizmos) return;

        Gizmos.DrawWireSphere(transform.position, _targetDectectionRange);

        if (_colliders == null) return;

        Gizmos.color = Color.magenta;
        foreach (var item in _colliders)
        {
            Gizmos.DrawSphere(item.position, 0.3f);
        }
    }
#endif
}