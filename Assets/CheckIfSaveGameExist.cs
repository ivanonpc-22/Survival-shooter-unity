using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfSaveGameExist : MonoBehaviour
{

    private void Awake()
    {
        if (Utils.ReadTextFromFile(GameSaveManager.SAVE_FILE_NAME) != null)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }

}
