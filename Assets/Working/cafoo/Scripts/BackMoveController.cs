using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BackMoveController : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.01f;

    //�@�^�����
    [SerializeField]
    private float power = 100;
    //�@�͂������镨�̂܂ł̔��a
    [SerializeField]
    private float radius = 4;
    //�@�͂̉�����
    [SerializeField]
    private ForceMode forceMode = ForceMode.Force;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = 0f;
        float z = 0f;
        if (Keyboard.current.wKey.isPressed)
        {
            x -= speed;
        }
        if( Keyboard.current.sKey.isPressed)
        {
            x += speed;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            z -= speed;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            z += speed;
        }

        //this.GetComponent<Rigidbody>().AddForce(x, 0 , z);

        Vector3 playerPos = new Vector3(x, 0, z);


        if (playerPos.magnitude > 0.1f)
        {
            // �����L�[�i�\���L�[�j�������������Ƀv���C���[�̌�����ύX
            transform.rotation = Quaternion.LookRotation(playerPos);

            // �v���C���[���ړ�
            transform.Translate(Vector3.forward * speed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddExplosionForce(power, transform.position, radius, 0f, forceMode);
        }
    }
}
