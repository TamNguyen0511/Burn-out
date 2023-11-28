using System;
using _Game.Scripts.Characters;
using _Game.Scripts.Enums;
using _Game.Scripts.Interact;
using _Game.Scripts.ScriptableObjects;
using UnityEngine;

public class Test : MonoBehaviour
{
    public IngredientState IngredientState;
    public IngredientState IngredientStateTest;

    private void Start()
    {
        Debug.Log(IngredientState);
        Debug.Log(IngredientStateTest);
        Debug.Log(IngredientState.HasFlag(IngredientStateTest));
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        //     TestCounter.Interact(this);
    }
}