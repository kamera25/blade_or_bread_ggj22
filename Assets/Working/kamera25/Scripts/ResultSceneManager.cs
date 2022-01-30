using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSceneManager : MonoBehaviour
{
    public ScoreManager.TYPE winType;
    [SerializeField] GameObject CitizenWin;
    [SerializeField] GameObject SoldierWin;

    // Start is called before the first frame update
    void Start()
    {
        if(winType == ScoreManager.TYPE.CITIZEN)
        {
            CitizenWin.SetActive(true);
            SoldierWin.SetActive(false);
        }
        else // 兵士が勝った
    {
            CitizenWin.SetActive(false);
            SoldierWin.SetActive(true);
        }
    }

}
