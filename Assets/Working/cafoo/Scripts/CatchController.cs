using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchController : MonoBehaviour
{
    private int PlayerNo = 0;

    [SerializeField]
    private int CatchCoinCount = 0;

    [SerializeField]
    private GameObject FucusCoinObject = null;

    [SerializeField]
    private GameObject InstantiateCoin = null;

    [SerializeField]
    private GameObject VotBox = null;

    AudioSource throwSE;

    // Start is called before the first frame update
    void Start()
    {
        //�e�̃v���C���[No���擾
        PlayerNo = this.transform.parent.gameObject.GetComponent<MoveController>().GetPlayerNo();
        throwSE = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag( "Enemy"))
        {
            FucusCoinObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag( "Enemy"))
        {
            other.transform.parent = null;
            FucusCoinObject = null;
        }
    }

    public int GetCatchCount()
    {
        return CatchCoinCount;
    }

    public void CatchCoin()
    {
        if (CatchCoinCount == 1) return;

        if (FucusCoinObject != null)
        {
            if (PlayerNo == FucusCoinObject.GetComponent<CoinData>().GetPlayerNo())
            {
                CatchCoinCount++;

                Destroy(FucusCoinObject);
            }
        }
    }

    public void ReleaseCoin(Vector3 pos, Vector2 _veloticy, float _coinForce)
    {
        if (CatchCoinCount <= 0) return;

        CatchCoinCount--;

        GameObject obj = Instantiate(InstantiateCoin, pos, Quaternion.identity);

        obj.GetComponent<CoinInToBox>().donationBoxTrans = VotBox.transform;
        obj.GetComponent<Rigidbody>().AddForce(new Vector3(
                                    _veloticy.y * -1,
                                    0,
                                    _veloticy.x
                                ) * _coinForce);

        throwSE.Play();
    }
}
