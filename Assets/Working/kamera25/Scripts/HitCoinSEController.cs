using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCoinSEController : MonoBehaviour
{
    private AudioSource hitCoinSE;
    float noPlayTime = 2f;
    float timeFromBeforePlaySE;

    float maxVolumeOnVelocity = 10f;

    void Start()
    {
        hitCoinSE = this.GetComponent<AudioSource>();
        timeFromBeforePlaySE = noPlayTime;
    }

    void Update()
    {
        timeFromBeforePlaySE -= Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject _hitObject = collision.gameObject;

        if ( IsHitObject(_hitObject) && timeFromBeforePlaySE < 0F)
        {
            // 音の大きさを調整
            if( _hitObject.TryGetComponent<Rigidbody>( out Rigidbody _rigidbody))
            {
                float _percent =  Mathf.Clamp01( _rigidbody.velocity.magnitude / maxVolumeOnVelocity + 0.1f);
                hitCoinSE.volume =  _percent;
            }
            else
            {
                hitCoinSE.volume = 0.3F;
            }


            hitCoinSE.Play();
            timeFromBeforePlaySE = noPlayTime;
        }
    }

    private bool IsHitObject( GameObject _hitObject)
    {
        return _hitObject.CompareTag("Enemy") || _hitObject.CompareTag("Walls") ;
    }
}
