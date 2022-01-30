using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HowtoScene : MonoBehaviour
{
    Vector2 LeftPos = new Vector2(-763.5582f, -379.2945f);
    Vector2 RightPos = new Vector2(321f, -379.2945f);

    [SerializeField] GameObject JapaneseCursol;
    [SerializeField] GameObject EnglishCursol;
    [SerializeField] GameObject ReturnCursol;

    [SerializeField] GameObject JapaneseStoryTxt;
    [SerializeField] GameObject JapaneseRuleTxt;
    [SerializeField] GameObject JapaneseControlTxt;

    [SerializeField] GameObject EnglishStoryTxt;
    [SerializeField] GameObject EnglishRuleTxt;
    [SerializeField] GameObject EnglishControlTxt;

    float invalidTImes = 0f;
    float waitTime = 0.5f;

    int menuindex = 2;

    string[] menuStr = { "Japanese", "English", "Opening" };

    AudioSource selectSE;

    // Start is called before the first frame update
    void Start()
    {
        selectSE = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        invalidTImes += Time.deltaTime;
        if (invalidTImes <= waitTime)
        {
            return;
        }

        var keyboard = Keyboard.current;

        if (keyboard.leftArrowKey.isPressed || keyboard.aKey.isPressed)
        {
            if (menuindex > 0) 
            {
                menuindex--;
                selectSE.Play();
            }

            invalidTImes = 0f;
            waitTime = 0.15f;
            MenuView();
        }
        else if (keyboard.rightArrowKey.isPressed || keyboard.dKey.isPressed)
        {
            if (menuindex < 2)
            {
                menuindex++;
                selectSE.Play();
            }

            invalidTImes = 0f;
            waitTime = 0.15f;
            MenuView();
        }

        if (keyboard.enterKey.isPressed || keyboard.spaceKey.isPressed)
        {
            if (menuindex == 0)
            {
                JapaneseStoryTxt.SetActive(true);
                JapaneseRuleTxt.SetActive(true);
                JapaneseControlTxt.SetActive(true);

                EnglishStoryTxt.SetActive(false);
                EnglishRuleTxt.SetActive(false);
                EnglishControlTxt.SetActive(false);

                invalidTImes = 0f;
                waitTime = 0.15f;
            }
            else if (menuindex == 1)
            {
                JapaneseStoryTxt.SetActive(false);
                JapaneseRuleTxt.SetActive(false);
                JapaneseControlTxt.SetActive(false);

                EnglishStoryTxt.SetActive(true);
                EnglishRuleTxt.SetActive(true);
                EnglishControlTxt.SetActive(true);

                invalidTImes = 0f;
                waitTime = 0.15f;
            }
            else if (menuindex == 2)
            {
                SceneManager.LoadScene(menuStr[menuindex]);
            }
        }
    }

    private void MenuView()
    {
        if (menuindex == 0)
        {
            JapaneseCursol.SetActive(true);
            EnglishCursol.SetActive(false);
            ReturnCursol.SetActive(false);
        }
        else if (menuindex == 1)
        {
            JapaneseCursol.SetActive(false);
            EnglishCursol.SetActive(true);
            ReturnCursol.SetActive(false);
        }
        else if (menuindex == 2)
        {
            JapaneseCursol.SetActive(false);
            EnglishCursol.SetActive(false);
            ReturnCursol.SetActive(true);
        }
    }
}
