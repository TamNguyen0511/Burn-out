using System;
using System.Collections.Generic;
using _Game.Scripts.Loot_System.SO;
using Ink.Parsed;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Scripts.Loot_System
{
    public class EnemyBehaviour : MonoBehaviour
    {
        #region Stat

        [Title("Stat")]
        protected string _name;
        protected int _maxHP;
        protected HealthState _currentState;

        protected AttackType _attackType;
        protected float _attackDamage;
        protected float _attackSpeed;
        protected float _meleeAttackRange;
        protected float _rangedAttackRange;

        protected MoveType _moveType;
        protected float _moveSpeed;

        [Title("Detection range")]
        protected float _dangerDetectionRange;

        [Title("Drop rate")]
        protected List<MaterialDataSO> _dropableMaterials = new();

        #endregion
        
        protected virtual void Behaviour()
        {
        }

        protected virtual void SpecialBehaviour()
        {
        }

        protected virtual void StateAction()
        {
            switch (_currentState)
            {
                case HealthState.None:
                    break;
                case HealthState.Healthy:
                    /// Patrolling, hunting for pray and player
                    
                    break;
                case HealthState.Dying:
                    /// Run away, try to hide from all other
                    break;
                case HealthState.Death:
                    /// Leave a body behind for loot and lure
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    [System.Flags]
    public enum AttackType
    {
        None = 1 << 1,
        Melee = 1 << 2,
        Range = 1 << 3
    }

    public enum MoveType
    {
        None,
        Swim,
        Walk,
        Fly,
        Fig
    }

    public enum HealthState
    {
        None,
        Healthy,
        Dying,
        Death
    }
}