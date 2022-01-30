using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinInToBox : MonoBehaviour
{

    public Transform donationBoxTrans;
    public float time = 10F;
    public float accelerationY = 1f;

    private float nowTime;
    //private Vector3 accelerationVec;
    private Transform thisTrans;



    public Renderer coinRenderer;

    private float startYPos;
    private Vector3 coinTorque;
    private float torqueDump = 300F;

    // Start is called before the first frame update
    void Start()
    {
        nowTime = time;
        thisTrans = this.transform;


        //this.enabled = false;
        // Debug
        RunCoinIntoDonationBox();
    }

    // Update is called once per frame
    void Update()
    {
        if( nowTime < 0F)
        {
            Destroy(this.gameObject, 4F);
            return;
        }
        else if( nowTime < 0.1F)
        {
            coinRenderer.enabled = false;
        }
        nowTime -= Time.deltaTime;

        /* Coin Position */
        Vector3 _accelerationVec  = CalcAcceleration( nowTime);
        Vector3 _vec = thisTrans.position + _accelerationVec * Time.deltaTime;

        // Y Axis Calculation
        float _timeFromStart = time - nowTime;
        _vec.y += 1 / 2 * accelerationY * _timeFromStart + startYPos;
        thisTrans.position = _vec;

        /* Coin Torque */
        Vector3 _torqueVelocity = coinTorque * Time.deltaTime * torqueDump;
        thisTrans.Rotate(_torqueVelocity);

    }

    private Vector3 CalcAcceleration( float time)
    {
        Vector3 _distVec = donationBoxTrans.position - thisTrans.position;
        Vector3 _accelerationVec = _distVec / time;
        return _accelerationVec;
    }

    public void RunCoinIntoDonationBox()
    {
        // コインをボックスにいれるためのセットアップ
        const int LAYER_IGNORE_WALLS = 8;

        this.enabled = true;
        this.gameObject.layer = LAYER_IGNORE_WALLS;

        // 位置に関するセットアップ
        startYPos = thisTrans.position.y; 
        float _torqueX = Mathf.Abs(Random.Range( 0f, 0.5f)+ 0.5F);
        coinTorque = new Vector3 ( _torqueX, 0F, 0F);

        // 物理挙動をContinuousSpeculativeに変更する
        Rigidbody _rigidBody = this.GetComponent<Rigidbody>();
        _rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
    }

    public void StopCoinPhysics()
    {
        Rigidbody _rigidBody = this.GetComponent<Rigidbody>();
        _rigidBody.constraints = RigidbodyConstraints.FreezeAll;
    }
}
