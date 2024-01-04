using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    #region Attributes

    [Title("Scripts component")]
    [SerializeField]
    private ContextSolver _movementDirectionSolver;
    [SerializeField]
    private AIData _aiData;

    [Title("Status")]
    [SerializeField]
    private float _detectionDelay = 0.05f;
    [SerializeField]
    private float _aiUpdateDelay = 0.06f;
    [SerializeField]
    private float _attackDelay = 1f;
    [SerializeField]
    private float _attackDistance = 0.5f;
    [SerializeField]
    private float _targetGiveupRange = 15;
    [SerializeField]
    private float _timeToMoveRandomly = 5f;
    [SerializeField]
    private float _timeMoveWhileNotChasing = 2f;
    [SerializeField]
    private float _timeRestWhileNotChasing = 2f;


    [Title("Checking input")]
    [SerializeField]
    private Vector2 _movementInput;

    [Title("Component List")]
    [SerializeField]
    private List<SteeringBehaviour> _steeringBehaviours;
    [SerializeField]
    private List<Detector> _detectors;

    [Title("Events")]
    public UnityEvent OnAttackPressed;
    public UnityEvent<Vector2> OnMovementInput, OnPointerInput;

    private bool _isFollowing = false;
    private float _countTimeToMoveRandomly;
    private bool _isResting = false;

    #endregion

    #region Unity's methods

    private void Start()
    {
        InvokeRepeating("PerformDetection", 0, _detectionDelay);
    }

    private void Update()
    {
        if (_aiData.CurrentTarget != null)
            if (Vector2.Distance(transform.position, _aiData.CurrentTarget.transform.position) >= _targetGiveupRange)
            {
                Debug.Log("It's time to give-up");
                _aiData.Targets = null;
                _aiData.CurrentTarget = null;
                return;
            }

        StartCoroutine(ResTillNextMove());

        if (_aiData.CurrentTarget != null)
        {
            OnPointerInput?.Invoke(_aiData.CurrentTarget.position);
            if (!_isFollowing)
            {
                _isFollowing = true;
                StartCoroutine(ChaseAndAttack());
            }
        }
        else if (_aiData.GetTargetsCount() > 0)
        {
            _aiData.CurrentTarget = _aiData.Targets[0];
        }

        OnMovementInput?.Invoke(_movementInput);
    }

    #endregion

    private IEnumerator ResTillNextMove()
    {
        if (_aiData.CurrentTarget != null) yield break;

        _countTimeToMoveRandomly += Time.deltaTime;
        if (_countTimeToMoveRandomly >= _timeToMoveRandomly && !_isResting)
        {
            _isResting = true;
            _movementInput = GetRandomWanderForce(1);
            yield return new WaitForSeconds(_timeMoveWhileNotChasing);
            _movementInput = Vector2.zero;
            yield return new WaitForSeconds(_timeRestWhileNotChasing);
            _countTimeToMoveRandomly = 0;
            _isResting = false;
        }
    }

    private void PerformDetection()
    {
        foreach (var detector in _detectors)
        {
            detector.Detect(_aiData);
        }
    }

    private Vector2 GetRandomWanderForce(float circleRadius)
    {
        Vector2 velocity = Random.insideUnitCircle;
        var circleCenter = velocity.normalized;
        var randomPoint = Random.insideUnitCircle;

        var displacement = new Vector2(randomPoint.x, randomPoint.y) * circleRadius;
        displacement = Quaternion.LookRotation(velocity) * displacement;

        var wanderForce = circleCenter + displacement;
        return wanderForce;
    }

    private IEnumerator ChaseAndAttack()
    {
        if (_aiData.CurrentTarget == null)
        {
            Debug.Log("Stopping");
            _movementInput = Vector2.zero;
            _isFollowing = false;
            yield break;
        }

        float distance = Vector2.Distance(_aiData.CurrentTarget.position, transform.position);

        if (distance < _attackDistance)
        {
            /// Todo: attack player/target
            _movementInput = Vector2.zero;
            OnAttackPressed?.Invoke();
            yield return new WaitForSeconds(_attackDelay);
            StartCoroutine(ChaseAndAttack());
        }
        else
        {
            _movementInput = _movementDirectionSolver.GetDirectionToMove(_steeringBehaviours, _aiData);
            yield return new WaitForSeconds(_aiUpdateDelay);
            StartCoroutine(ChaseAndAttack());
        }
    }
}