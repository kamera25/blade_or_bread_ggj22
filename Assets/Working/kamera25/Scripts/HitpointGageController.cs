using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HitpointGageController : MonoBehaviour
{
    List<GameObject> gageBlocks = new List<GameObject>();
    public int usingGage = 5;
    public float arrayAlign = 1.2f;

    private int votePoint;

    // Start is called before the first frame update
    void Start()
    {
        gageBlocks.Add(this.gameObject);

        for( int i = 1; i < usingGage; i++)
        {
            GameObject _go = GameObject.Instantiate(this.gameObject);
            Vector3 _pos = this.transform.position;
            _go.transform.position = new Vector3 ( _pos.x, _pos.y, _pos.z) + _go.transform.forward * -1F * arrayAlign * i;
            _go.transform.Rotate(Vector3.up * 90F);

            _go.transform.parent = this.transform.parent;


            Destroy( _go.GetComponent<HitpointGageController>());
            gageBlocks.Add(_go);
            _go.SetActive(false);
        }

        this.GetComponentInChildren<Renderer>().enabled = false;
        votePoint++;
    }


/// <summary>
/// これを呼ぶと、投票数を増やす
/// </summary>
    public void IncreaseHitPoint()
    {
        if( votePoint >= usingGage)
        {
            return;
        }

        int index = votePoint;
        gageBlocks[index].SetActive(true);
        votePoint++;
    }
}
