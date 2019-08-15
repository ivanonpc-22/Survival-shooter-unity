using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChangeButton : MonoBehaviour
{
    public GAME_STATE changeStateTo;

    public void OnButtonClicked()
    {
        GameStateManager.Instance.UpdateState(changeStateTo);
    }

}
