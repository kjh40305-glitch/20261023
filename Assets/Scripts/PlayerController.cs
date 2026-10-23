using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform _muzzlePosition;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Animator _animator;
    [SerializeField] Rigidbody2D _rigidbody;
    [SerializeField] SpriteRenderer _sprRenderer;
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] private int _hp = 0;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _jumpPower = 6;
    Vector2 _direction;
    bool _isGrounded;
    bool _isFalling;

    void CreateBullet()
    {
        var obj = Instantiate(_bulletPrefab);
        var bullet = obj.GetComponent<BulletController>();
        bullet.SetBullet(_muzzlePosition.position, transform.rotation.eulerAngles.y == 0f ? Vector3.right : Vector3.left);
    }
    void OnMove(InputValue value)
    {
        _direction = value.Get<Vector2>();
        if (_direction == Vector2.zero)
        {
            _animator.SetBool("IsWalk", false);
        }
        if (_direction == Vector2.left)
        {
            //gameObject.transform.position += Vector3.left * 0.01f;
            _animator.SetBool("IsWalk", true);
            //_sprRenderer.flipX = false;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        if (_direction == Vector2.right)
        {
            //gameObject.transform.position += Vector3.right * 0.01f;
            _animator.SetBool("IsWalk", true);
            transform.rotation = Quaternion.identity;
            //_sprRenderer.flipX = true;
        }
    }
    void OnReleaseAttackButton(InputAction.CallbackContext context)
    {
        _animator.SetBool("IsShoot", false);
    }
    void OnAttack()
    {
        _animator.SetBool("IsShoot", true);
        //CreateBullet();
    }
    void OnJump()
    {
        _animator.Play("Jump", 0, 0f);
        _animator.SetInteger("JumpState", 1);
        _rigidbody.linearVelocityY = 0f;
        _rigidbody.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);

    }
    void Move()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.leftArrowKey.wasReleasedThisFrame || Keyboard.current.rightArrowKey.wasReleasedThisFrame)
        {
            _animator.SetBool("IsWalk", false);
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            gameObject.transform.position += Vector3.left * 0.01f;
            _animator.SetBool("IsWalk", true);
            _sprRenderer.flipX = false;
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            gameObject.transform.position += Vector3.right * 0.01f;
            _animator.SetBool("IsWalk", true);
            _sprRenderer.flipX = true;
        }
        //if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        //{
        //    _animator.SetBool("IsWalk", false);
        //}
        //if(Input.GetKey(KeyCode.LeftArrow))
        //{
        //    gameObject.transform.position += Vector3.left * 0.01f;
        //    _animator.SetBool("IsWalk", true);
        //    _sprRenderer.flipX = false;
        //}
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    gameObject.transform.position += Vector3.right * 0.01f;
        //    _animator.SetBool("IsWalk", true);
        //    _sprRenderer.flipX = true;
        //}
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        _isGrounded = true;
        _isFalling = false;
        _animator.SetInteger("JumpState", 0);
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        _isGrounded = false;
    }
    void FixedUpdate()
    {
        var nextVelocity = new Vector2(_direction.x, 0) * _speed;
        nextVelocity.y = _rigidbody.linearVelocityY;
        _rigidbody.linearVelocity = nextVelocity;
    }
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _playerInput.actions["Attack"].canceled += OnReleaseAttackButton;
    }
    void OnDestroy()
    {
        _playerInput.actions["Attack"].canceled -= OnReleaseAttackButton;
    }
    // Update is called once per frame
    void Update()
    {
        if (!_isGrounded && !_isFalling)
        {
            if (_rigidbody.linearVelocity.y < 0)
            {
                _animator.SetInteger("JumpState", 2);
                _isFalling = true;
            }
        }
        //   Move();
        //gameObject.transform.position += (Vector3)_direction * _speed * Time.deltaTime;
    }
}