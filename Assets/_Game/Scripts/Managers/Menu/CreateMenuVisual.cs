﻿using System;
using _Game.Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;
using Redcode.Pools;

namespace _Game.Scripts.Managers.Menu
{
    public class CreateMenuVisual : MonoBehaviour
    {
        private enum CreateMenuState
        {
            ChoosingIngredients,
            ChoosingDisk,
            ConfirmMenu
        }

        [SerializeField]
        private MenuManager _menuManager;
        [SerializeField]
        private AllFoodDatabase _foodDatabase;
        [SerializeField]
        private CreateMenuState _currentMenuState;

        private Pool<Button> _buttonPool;

        #region UI Elements

        [SerializeField]
        private GridLayout _choosePnl;
        [SerializeField]
        private Button _chooserBtnPrefab;
        [SerializeField]
        private Button _nextStageBtn;

        #endregion

        private void ChangeMenuStage(CreateMenuState newState)
        {
            if (_currentMenuState == newState) return;
            _currentMenuState = newState;
            switch (_currentMenuState)
            {
                case CreateMenuState.ChoosingIngredients:
                    ShowIngredients();
                    break;
                case CreateMenuState.ChoosingDisk:
                    break;
                case CreateMenuState.ConfirmMenu:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            void ClearPoolBtn()
            {
                _buttonPool.Get().onClick.RemoveAllListeners();
            }

            void ShowIngredients()
            {
                ClearPoolBtn();
                if (_buttonPool.Count < _foodDatabase.AllIngredients.Count)
                {
                    _buttonPool = Pool.Create(_chooserBtnPrefab,
                        _foodDatabase.AllIngredients.Count - _buttonPool.Count, _choosePnl.transform);
                }

                for (int i = 0; i < _foodDatabase.AllIngredients.Count; i++)
                {
                    Button btn = _buttonPool.Get();
                    btn.GetComponent<Image>().sprite = _foodDatabase.AllIngredients[i]
                        .IngredientStateAndPrefab[IngredientState.Raw].IngredientStateSprite;
                    btn.onClick.AddListener(_menuManager.AddIngredients);
                }
            }
        }
    }
}