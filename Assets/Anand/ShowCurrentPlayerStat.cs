using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCurrentPlayerStat : MonoBehaviour
{
    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
        text.text = "";
        /*string savedJSON = Utils.ReadTextFromFile(PlayerStatTracker.STAT_FILE);

        if (!string.IsNullOrEmpty(savedJSON))
        {
            PlayerStatModel stat = Utils.DeserializeObject<PlayerStatModel>(savedJSON);

            text.text = string.Format("HI-SCORE: {0}, KILLS: {1}, DEATHS: {2}, BULLETS FIRED: {3}"
                , stat.hiScore, stat.kills, stat.deaths, stat.bullets);
        }*/
    }


}
