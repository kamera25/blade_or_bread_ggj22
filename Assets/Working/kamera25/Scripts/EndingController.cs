using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class EndingController : MonoBehaviour
{

    float invalidTImes = 0f;
    float waitTime = 0.5f;
    AudioSource pushButtonSE;

    // Start is called before the first frame update
    void Start()
    {
        pushButtonSE = this.GetComponent<AudioSource>();
        invalidTImes = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        invalidTImes += Time.deltaTime;
        if (invalidTImes <= waitTime)
        {
            return;
        }

        var _keyboard = Keyboard.current;

        if (_keyboard.enterKey.isPressed || _keyboard.spaceKey.isPressed)
        {
            var _fadeController = this.GetComponent<FadeController>();
            _fadeController.isFadeOut = true;
            pushButtonSE.Play();
            StartCoroutine ("WaitAndTransitionOpenningScene");
        }  


        
    }

    private IEnumerator WaitAndTransitionOpenningScene() 
    {
        yield return new WaitForSeconds (0.8f);
        SceneManager.LoadScene("Opening");
    }


}
