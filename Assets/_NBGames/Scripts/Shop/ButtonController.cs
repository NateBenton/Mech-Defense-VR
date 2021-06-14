using System;
using System.Collections;
using System.Collections.Generic;
using _NBGames.Scripts.Managers;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject[] _navigationButtons;

    private void OnEnable()
    {
        EventManager.onShowBuyOptions += EnableNavigationButtons;
    }

    private void OnDisable()
    {
        EventManager.onShowBuyOptions -= EnableNavigationButtons;
    }

    private void EnableNavigationButtons()
    {
        foreach (var button in _navigationButtons)
        {
            button.SetActive(true);
        }
    }
}
