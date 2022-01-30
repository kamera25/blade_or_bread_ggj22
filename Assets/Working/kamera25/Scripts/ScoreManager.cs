using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class ScoreManager : MonoBehaviour
{

    public enum TYPE
    {
        NONE = 0,
        CITIZEN = 1,
        SOLIDER = 2
    }

    public HitpointGageController soldierVoteCard;
    public HitpointGageController citizenVoteCard;

    public int winVotePoint = 4;
    private int soldierVotePoint;
    private int citizenVotePoint;

    private TYPE winType;

    void Start()
    {
         soldierVotePoint = 0 ;
         citizenVotePoint = 0 ;
         winType = TYPE.NONE;
    }

    /// <summary>
    /// ポイントを追加する時に呼び出し。
    /// </summary>   
    public void AddVotePoint( ScoreManager.TYPE type)
    {
        int _votePoint ;

        if( type == TYPE.CITIZEN) // 市民
        {
            citizenVoteCard.IncreaseHitPoint();
            citizenVotePoint++;
            _votePoint = citizenVotePoint;
        }
        else // 兵士
        {
            soldierVoteCard.IncreaseHitPoint();
            soldierVotePoint++;
            _votePoint = soldierVotePoint;
        }


        if(_votePoint > winVotePoint && winType == TYPE.NONE)
        {
            winType = type;

            var _fadeController = this.GetComponent<FadeController>();
            _fadeController.isFadeOut = true;
            _fadeController.fadeSpeed = 0.004f;


            StartCoroutine ("WaitAndTransitionResultScene");
        }
    } 


    private IEnumerator WaitAndTransitionResultScene() 
    {
        yield return new WaitForSeconds (4.0f);
        TransitionResultScene();
    }

    private void TransitionResultScene()
    {
        SceneManager.sceneLoaded += ResultSceneLoaded;
        SceneManager.LoadScene("Ending");
    }

    /// <summary>
    /// リザルトシーンに送る情報
    /// </summary>
    private void ResultSceneLoaded(Scene next, LoadSceneMode mode)
    {
        var _resultManager= GameObject.FindWithTag("GameController").GetComponent<ResultSceneManager>();

        _resultManager.winType = winType;

        SceneManager.sceneLoaded -= ResultSceneLoaded;
    }
}
