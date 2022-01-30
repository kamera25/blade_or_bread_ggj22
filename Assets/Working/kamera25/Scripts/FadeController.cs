using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public Image fadeImage;
    private Color baseColor;

    public bool isFadeIn = false;
    public bool isFadeOut = false;
    public float fadeSpeed = 0.02f;


    // Start is called before the first frame update
    void Start()
    {
        fadeImage.enabled = true;
        isFadeIn = true;
        baseColor = fadeImage.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFadeIn)
        {
			StartFadeIn ();
		}
 
		if (isFadeOut) 
        {
			StartFadeOut ();
		}
    }

    void StartFadeIn()
    {
		float alfa = baseColor.a - fadeSpeed;      
		SetAlpha ( alfa);         

		if(alfa <= 0)
        {                  
			isFadeIn = false;             
			fadeImage.enabled = false;   
		}
	}
 
	public void StartFadeOut()
    {
		fadeImage.enabled = true;  

		float alfa = fadeSpeed + baseColor.a;      
		SetAlpha ( alfa);       

		if(alfa >= 1)
        {          
			isFadeOut = false;
		}
	}
 
	void SetAlpha( float newalfa)
    {
        baseColor.a = newalfa;
		fadeImage.color = baseColor;
	}
}
