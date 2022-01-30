using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpeningController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var _keyboard = Keyboard.current;

        if (_keyboard.spaceKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Main");
        }   
    }
}
