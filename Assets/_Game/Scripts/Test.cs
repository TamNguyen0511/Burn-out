using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts;
using _Game.Scripts.Characters;
using _Game.Scripts.Interact;
using _Game.Scripts.Kitchen;
using UnityEngine;

public class Test : Interactor
{
    public CounterBase TestCounter;
    public CharacterInteractHandle TestPlayer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            TestCounter.Interact(this);
    }
}