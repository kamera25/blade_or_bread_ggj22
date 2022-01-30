using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDamageController : MonoBehaviour
{
    private CoinInToBox coinInVoteBox;
    private CoinData coinData;
    public float VelocityAsDamage = 35F;

    public AudioClip damageSE;
    private AudioSource audioSource;

    private void Start()
    {
        coinInVoteBox = this.GetComponent<CoinInToBox>();
        coinData = this.GetComponent<CoinData>();
        audioSource = this.GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject _hitObject = collision.gameObject;
        if ( _hitObject.CompareTag("Player") )
        {
            // コインが自分のものかチェック。
            if(_hitObject.TryGetComponent<MoveController>( out MoveController _player))
            {
                if(_player.GetPlayerNo() == coinData.GetPlayerNo())
                {
                    return;// 抜ける
                }
            }      

            // 自分のリジットボディをチェック
            if( this.TryGetComponent<Rigidbody>( out Rigidbody _rigidbody))
            {
                Debug.Log( $"Hit player and coin / velocity :  { _rigidbody.velocity.sqrMagnitude } ");
                if( _rigidbody.velocity.sqrMagnitude > VelocityAsDamage)
                {
                     PutVoteToBox();
                }
            }
        }
    }

    private void PutVoteToBox()
    {
        audioSource.clip = damageSE;
        audioSource.volume = 1F;
        audioSource.spatialBlend = 0F;
        audioSource.Play();
        
        coinInVoteBox.enabled = true;
    }
}
