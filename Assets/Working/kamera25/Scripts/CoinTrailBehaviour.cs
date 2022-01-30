using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTrailBehaviour : MonoBehaviour
{

    private TrailRenderer trailRenderer;
    private Transform trailTrans;
    private Rigidbody rigidBody;
    // Start is called before the first frame update

    private float velocityAsDamage;
    private float maxStartWidthOfTrail = 0.61f;
    private float minStartWidthOfTrail = 0.05f;

    private Vector3 trailPos = Vector3.zero;


    void Start()
    {
        trailRenderer = this.GetComponentInChildren<TrailRenderer>();
        trailRenderer.emitting = false;
        trailTrans = trailRenderer.transform;

        rigidBody = this.GetComponent<Rigidbody>();
        velocityAsDamage = this.GetComponent<HitDamageController>().VelocityAsDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if( !ThisCoinIsUseInGame())
        {
            trailRenderer.enabled = true;
        }


        if( IsCoinDamage())
        {
            float velocityPercent = rigidBody.velocity.sqrMagnitude / velocityAsDamage * 0.1F;

            trailRenderer.emitting = true;
            trailRenderer.time = Mathf.Clamp( velocityPercent , minStartWidthOfTrail, maxStartWidthOfTrail);
        }
        else
        {
            trailRenderer.emitting = false;
        }
    }

    private bool IsCoinDamage()
    {
        float _coinVelocity = rigidBody.velocity.sqrMagnitude;
        return _coinVelocity > velocityAsDamage;
    }

    private bool ThisCoinIsUseInGame()
    {
        return rigidBody.constraints != RigidbodyConstraints.FreezeAll;
    }
}
