using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;
    [SerializeField] private Animator _animator;

    private PlayerState _currentState;
    
    [Header("Movement")]
    [SerializeField] private float _moveSpeed;

    [Header("Jump")]
    [SerializeField] private bool _isGrounded;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private float _groundCheckerRadius = 0.3f;
    [SerializeField] private float _jumpHeight = 2f;
    [SerializeField] private float _gravity = -9.81f;
    private Vector3 _velocity;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        ChangeState(PlayerState.IDLE);
    }

    private void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundChecker.position, _groundCheckerRadius, _groundLayer);

        Gravity();
        Movement();
        Jump();
    }
    
    private void Movement()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * vertical + transform.right * horizontal;

        if (move != Vector3.zero)
        {
            _characterController.Move(move * _moveSpeed * Time.deltaTime);
            
            if (_currentState != PlayerState.WALK)
                ChangeState(PlayerState.WALK);
        }
    }

    private void Jump()
    {
        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            ChangeState(PlayerState.JUMP);
        }
    }

    private void Gravity()
    {
        if (_isGrounded && _velocity.y < 0)
            _velocity.y = -2f;

        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }

    private void ChangeState(PlayerState nextState)
    {
        _currentState = nextState;

        switch (_currentState)
        {
            case PlayerState.IDLE:
                //animasyonu oynat
                break;
            case PlayerState.WALK:
                //animasyonu oynat
                break;
            case PlayerState.JUMP:
                //animasyonu oynat
                break;
        }
    }
}

public enum PlayerState
{
    IDLE,
    WALK,
    JUMP,
}
