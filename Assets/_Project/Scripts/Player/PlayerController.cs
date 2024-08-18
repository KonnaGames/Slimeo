using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [Header("Sound Effects")] 
    public AudioClip walk;
    public AudioClip jump;
    
    private CharacterController _characterController;
    [SerializeField] private CameraControl _cameraControl;
    [SerializeField] private Animator _animator;

    private PlayerState _currentState;
    
    [Header("Movement")]
    [SerializeField] private float _moveSpeed;

    [Header("Jump")]
    [SerializeField] private bool _isGrounded;
    // [SerializeField] private LayerMask _ignoreLayer;
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
        _isGrounded = CheckIsGrounded();

        EatLogic();
        Gravity();
        Movement();
        Jump();
    }

    private void EatLogic()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Ray ray = new Ray(transform.position, )
        }
    }

    private bool CheckIsGrounded()
    {
        var collisons = Physics.SphereCastAll(_groundChecker.position, _groundCheckerRadius, Vector3.down, 0.01f);

        bool isGrounded = false;
        foreach (var hit in collisons)
        {
            if(hit.transform == transform) continue;
            if(hit.transform.CompareTag("Dialogue Collider")) continue;
            isGrounded = true;
        }

        return isGrounded;
    }
    
    private void Movement()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        
        _animator.SetFloat("speed", new Vector2(horizontal, vertical).magnitude);

        Vector3 move = _cameraControl.cameraForward * vertical + _cameraControl.transform.right * horizontal;

        if (move != Vector3.zero)
        {
            _characterController.Move(move * _moveSpeed * Time.deltaTime);
            
            ChangeState(PlayerState.WALK);
            return;
        }
        
        ChangeState(PlayerState.IDLE);
    }

    

    private void Jump()
    {
        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * (_gravity + 20));
            _animator.SetTrigger("Jump");
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
        if (_currentState == nextState) return;
        _currentState = nextState;
        
        StopAllCoroutines();
        
        switch (_currentState)
        {
            case PlayerState.IDLE:
                //animasyonu oynat
                break;
            case PlayerState.WALK:
                StartCoroutine(PlayWalkSoundEffect());
                //animasyonu oynat
                break;
            case PlayerState.JUMP:
                SoundEffectPlayer.Instance.PlaySoundEffect(jump);
                //animasyonu oynat
                break;
        }
    }
    
    private IEnumerator PlayWalkSoundEffect()
    {
        while (true)
        {
            SoundEffectPlayer.Instance.PlaySoundEffect(walk);
            yield return new WaitForSeconds(walk.length);
        }
    }
}

public enum PlayerState
{
    IDLE,
    WALK,
    JUMP,
}
