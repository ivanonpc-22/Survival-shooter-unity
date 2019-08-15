using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            QuickSaveGame();
        }
        else if(Input.GetKeyDown(KeyCode.L))
        {
            QuickLoadGame();
        }
    }

    private void QuickLoadGame()
    {
        
    }

    private void QuickSaveGame()
    {
        
    }
}
