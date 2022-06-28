using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropdownHandler : MonoBehaviour
{
    TMP_Dropdown m_Dropdown;
    MenuManager menuManager;

    [SerializeField] private bool playersDropdown; 

    void Start()
    {
        menuManager = GameObject.FindObjectOfType<MenuManager>();

        //Fetch the Dropdown GameObject
        m_Dropdown = GetComponent<TMP_Dropdown>();
        //Add listener for when the value of the Dropdown changes, to take action
        m_Dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(m_Dropdown);
        });
    }

    //Ouput the new value of the Dropdown into Text
    void DropdownValueChanged(TMP_Dropdown dropdown)
    {
        if (playersDropdown) menuManager.setNumPlayers(dropdown.value + 2);
        else menuManager.setNumMiniGames(dropdown.value + 1);
    }
}
