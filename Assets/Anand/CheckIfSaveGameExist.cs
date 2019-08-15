using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfSaveGameExist : MonoBehaviour
{

    private void Awake()
    {
        if (!string.IsNullOrEmpty(Utils.ReadTextFromFile(GameSaveManager.SAVE_FILE_NAME)))
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }

}
