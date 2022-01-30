using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    Vector2 LeftPos = new Vector2(-763.5582f,-379.2945f);
    Vector2 RightPos = new Vector2(321f, -379.2945f);

    [SerializeField] GameObject StartCursol;
    [SerializeField] GameObject HowtoCursol;
    [SerializeField] GameObject CreditCursol;

    [SerializeField] AudioSource pushButtonSE;

    float invalidTImes = 0f;
    float waitTime = 0.5f;

    int menuindex = 1;

    string[] menuStr = { "Howto", "Main", "Credit" };

    string transitionFromMenuSceneName = "";

    AudioSource selectSE;

    // Start is called before the first frame update
    void Start()
    {

        selectSE = this.GetComponent<AudioSource>();

        invalidTImes = 0f;
        MenuView();
    }

    // Update is called once per frame
    void Update()
    {
        invalidTImes += Time.deltaTime;
        if (invalidTImes <= waitTime )
        {
            return;
        }

        var keyboard = Keyboard.current;

        if (keyboard.leftArrowKey.isPressed || keyboard.aKey.isPressed)
        {
            if(menuindex > 0)
            {
                selectSE.Play();
                menuindex--;
            }

            invalidTImes = 0f;
            waitTime = 0.15f;
            MenuView();
        }
        else if (keyboard.rightArrowKey.isPressed || keyboard.dKey.isPressed)
        {
            if(menuindex < 2)
            {
                selectSE.Play();
                menuindex++;
            }

            invalidTImes = 0f;
            waitTime = 0.15f;
            MenuView();
        }

        if ((keyboard.enterKey.isPressed || keyboard.spaceKey.isPressed) && transitionFromMenuSceneName == "")
        {
            pushButtonSE.Play();

            transitionFromMenuSceneName = menuStr[menuindex];

            // フェードイン処理
            var _fadeController = this.GetComponent<FadeController>();
            _fadeController.isFadeOut = true;
            StartCoroutine ("WaitAndTransitionNextScene");
        }

    }

    private void MenuView()
    {
        if (menuindex == 0)
        {
            StartCursol.SetActive(false);
            HowtoCursol.SetActive(true);
            CreditCursol.SetActive(false);
        }
        else if (menuindex == 1)
        {
            StartCursol.SetActive(true);
            HowtoCursol.SetActive(false);
            CreditCursol.SetActive(false);
        }
        else if (menuindex == 2)
        {
            StartCursol.SetActive(false);
            HowtoCursol.SetActive(false);
            CreditCursol.SetActive(true);
        }
    }

    private IEnumerator WaitAndTransitionNextScene() 
    {
        yield return new WaitForSeconds (0.8f);
        SceneManager.LoadScene(transitionFromMenuSceneName);
    }
}
