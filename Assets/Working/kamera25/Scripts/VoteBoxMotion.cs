using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoteBoxMotion : MonoBehaviour
{

    private Animator animator;
    private AudioSource coinInSE;

    public AudioSource citizenSE;
    public AudioSource soliderSE;

    public ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        coinInSE = this.GetComponent<AudioSource>();
        scoreManager = GameObject.FindWithTag("GameController").GetComponent<ScoreManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject _hitObject = other.gameObject;

        if( _hitObject.CompareTag("Enemy"))
        {
            animator.SetTrigger("InCoin");
            coinInSE.Play();

            ScoreManager.TYPE _coinType = ScoreManager.TYPE.NONE;
            if( _hitObject.TryGetComponent<CoinData>( out CoinData _coindata))
            {
                _coinType = (ScoreManager.TYPE)_coindata.GetPlayerNo();
            }

            if( _hitObject.TryGetComponent<SphereCollider>( out SphereCollider _collider))
            {
                _collider.enabled = false;
            }

            if( _hitObject.TryGetComponent<CoinInToBox>( out CoinInToBox _coinInToBox))
            {
                _coinInToBox.StopCoinPhysics();
            }



            if( _coinType == ScoreManager.TYPE.CITIZEN)
            {
                citizenSE.Play();
                scoreManager.AddVotePoint(_coinType);
            }
            else // 兵士なら
            {
                soliderSE.Play();
                scoreManager.AddVotePoint(_coinType);
            }
        }
    }

}
