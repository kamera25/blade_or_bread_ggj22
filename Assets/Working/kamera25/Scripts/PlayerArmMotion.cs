using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmMotion : MonoBehaviour
{
    private Animator armAnim;

    [SerializeField]
    private CatchController catchController;

    [SerializeField]
    private GameObject coin;

    // Start is called before the first frame update
    void Start()
    {
        armAnim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if( catchController.GetCatchCount() > 0)
        {
            armAnim.SetBool("IsArmUp", true);
            coin.SetActive(true);
        }
        else
        {
            armAnim.SetBool("IsArmUp", false);
            coin.SetActive(false);
        }

    }
}
