using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public GAME_STATE CurrentGameState { get; private set; }

    [SerializeField]
    GameObject MainMenuUIObject;

    [SerializeField]
    GameObject InGameUIObject;

    private void Awake()
    {
        if(Instance != null)
        {
            UpdateRefs();

            DestroyImmediate(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        Instance = this;
        UpdateState(GAME_STATE.MAIN_MENU);
    }

    private void UpdateRefs()
    {
        Instance.MainMenuUIObject = MainMenuUIObject;
        Instance.InGameUIObject = InGameUIObject;
    }

    public void UpdateState(GAME_STATE newState)
    {
        if (CurrentGameState != newState)
        {
            CurrentGameState = newState;

            ToggleObjectsOnStateSwitch();

            if (ActionManager.GameStateActions.GameStateChange != null)
                ActionManager.GameStateActions.GameStateChange(newState);
        }
        else
            Debug.LogError("Setting the state to same value");
    }


    void ToggleObjectsOnStateSwitch()
    {
        switch(CurrentGameState)
        {
            case GAME_STATE.GAME:
                MainMenuUIObject.SetActive(false);
                InGameUIObject.SetActive(true);
                break;

            case GAME_STATE.MAIN_MENU:
                MainMenuUIObject.SetActive(true);
                InGameUIObject.SetActive(false);
                break;

            case GAME_STATE.LOAD_GAME:
                MainMenuUIObject.SetActive(false);
                InGameUIObject.SetActive(false);
                break;
        }
    }
}

public enum GAME_STATE
{
    NONE,
    MAIN_MENU,
    GAME,
    LOAD_GAME,
    GAME_OVER
}
