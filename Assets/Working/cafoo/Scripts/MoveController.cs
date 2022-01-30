using UnityEngine;
using UnityEngine.InputSystem;


public class MoveController : MonoBehaviour
{
    [SerializeField] private float _moveForce = 50;
    [SerializeField] private float _coinForce = 100;

    [SerializeField] private GameObject CatchObject;

    [SerializeField] private int PlayerNo = 1;

    [SerializeField] private Animator _animator;

    private GameObject myPlayerModel;

    private Blade_of_bread _gameInputs;

    private Rigidbody _rigidbody;
    private Vector2 _moveInputValue;

    private Vector2 _velocity;

    private void Awake()
    {
        myPlayerModel = this.gameObject;

        _rigidbody = this.GetComponent<Rigidbody>();

        // Input Action生成
        _gameInputs = new Blade_of_bread();

        // Action設定
        if (PlayerNo == 1)
        {
            _gameInputs.Player.Move.started += OnMove;
            _gameInputs.Player.Move.performed += OnMove;
            _gameInputs.Player.Move.canceled += OnMove;

            _gameInputs.Player.Fire.started += OnHoldStart;
            _gameInputs.Player.Fire.canceled += OnFire;
        }
        else if(PlayerNo == 2)
        {
            _gameInputs.Player.Move2.started += OnMove;
            _gameInputs.Player.Move2.performed += OnMove;
            _gameInputs.Player.Move2.canceled += OnMove;

            _gameInputs.Player.Fire2.started += OnHoldStart;
            _gameInputs.Player.Fire2.canceled += OnFire;
        }


        // Input Action 有効
        _gameInputs.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDestroy()
    {
        // Action設定
        if (PlayerNo == 1)
        {
            _gameInputs.Player.Move.started -= OnMove;
            _gameInputs.Player.Move.performed -= OnMove;
            _gameInputs.Player.Move.canceled -= OnMove;

            _gameInputs.Player.Fire.started -= OnHoldStart;
            _gameInputs.Player.Fire.canceled -= OnFire;
        }
        else if (PlayerNo == 2)
        {
            _gameInputs.Player.Move2.started -= OnMove;
            _gameInputs.Player.Move2.performed -= OnMove;
            _gameInputs.Player.Move2.canceled -= OnMove;

            _gameInputs.Player.Fire2.started -= OnHoldStart;
            _gameInputs.Player.Fire2.canceled -= OnFire;
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        // Move設定
        _moveInputValue = context.ReadValue<Vector2>();

        if(_moveInputValue.x == -1f)
        {
            myPlayerModel.transform.rotation = Quaternion.Euler(0, 180.0f, 0);
            _velocity = _moveInputValue;
        }
        else if (_moveInputValue.x == 1f)
        {
            myPlayerModel.transform.rotation = Quaternion.Euler(0, 0.0f, 0);
            _velocity = _moveInputValue;
        }
        else if (_moveInputValue.y == -1f)
        {
            myPlayerModel.transform.rotation = Quaternion.Euler(0, 90.0f, 0);
            _velocity = _moveInputValue;
        }
        else if (_moveInputValue.y == 1f)
        {
            myPlayerModel.transform.rotation = Quaternion.Euler(0, -90.0f, 0);
            _velocity = _moveInputValue;
        }
        else if (_moveInputValue.x == -0.707107f && _moveInputValue.y == -0.707107f)
        {
            myPlayerModel.transform.rotation = Quaternion.Euler(0, 135.0f, 0);
            _velocity = _moveInputValue;
        }
        else if (_moveInputValue.x == -0.707107f && _moveInputValue.y == 0.707107f)
        {
            myPlayerModel.transform.rotation = Quaternion.Euler(0, 225.0f, 0);
            _velocity = _moveInputValue;
        }
        else if (_moveInputValue.x == 0.707107f && _moveInputValue.y == 0.707107f)
        {
            myPlayerModel.transform.rotation = Quaternion.Euler(0, -45.0f, 0);
            _velocity = _moveInputValue;
        }
        else if (_moveInputValue.x == 0.707107f && _moveInputValue.y == -0.707107f)
        {
            myPlayerModel.transform.rotation = Quaternion.Euler(0, 45.0f, 0);
            _velocity = _moveInputValue;
        }

        if( Mathf.Abs(_moveInputValue.x) > 0.7F || Mathf.Abs(_moveInputValue.y) > 0.7F)
        {
            _animator.SetBool("IsMove", true);
        }
        else
        {
            _animator.SetBool("IsMove", false);
        }
    }

    private void OnHoldStart(InputAction.CallbackContext context)
    {
        CatchObject.GetComponent<CatchController>().CatchCoin();
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        //Debug.Log("Fire");

        Vector3 pos = transform.position + transform.forward * 1.5f;
        pos.y = 0.12f;

        CatchObject.GetComponent<CatchController>().ReleaseCoin(pos , _velocity, _coinForce);

    }

    private void FixedUpdate()
    {
        //移動方向へ力を掛ける
        _rigidbody.AddForce(new Vector3(
            _moveInputValue.y * -1, 0, _moveInputValue.x ) * _moveForce);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public int GetPlayerNo()
    {
        return PlayerNo;
    }
}
