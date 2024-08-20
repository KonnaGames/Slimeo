using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public PlayerHealth PlayerHealth;
    [SerializeField] private Transform _spikePos;
    [SerializeField] private Transform Visual;

    [Header("Sound Effects")] 
    public AudioClip walk;
    public AudioClip jump;
    
    private CharacterController _characterController;
    [SerializeField] private CameraControl _cameraControl;
    [SerializeField] private Animator _animator;
    private PlayerState _currentState;
    
    [Header("Movement")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private bool dialoguMovement;

    [Header("Jump")]
    [SerializeField] private bool _isGrounded;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private float _groundCheckerRadius = 0.3f;
    [SerializeField] private float _jumpHeight = 2f;
    [SerializeField] private float _gravity = -9.81f;
    private Vector3 _velocity;


    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        ChangeState(PlayerState.IDLE);

        PlayerHealth.Instance.SetHearthCount(1);
    }

    private void Update()
    {
        _isGrounded = CheckIsGrounded();
        ShootSpike();
        Gravity();
        Movement();
        Jump();
    }

    public void ToggleDialogeMovement(bool toggle)
    {
        dialoguMovement = toggle;
    }

    private void ShootSpike()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!Inventory.Instance.CheckCapacity())
            {
                var item = Inventory.Instance.GetItem();

                if (item != null)
                {
                    item.transform.SetParent(transform);
                    item.transform.position = transform.position;

                    item.transform.DOScale(Vector3.one, 0.2f)
                    .OnComplete(() =>
                    {
                        item.transform.forward = Camera.main.transform.forward;
                        item.transform.DOMove(_spikePos.position, 0.5f)
                           .OnComplete(() =>
                           {
                               Debug.Log("Firlatti");
                               var rigid = item.AddComponent<Rigidbody>();
                               rigid.collisionDetectionMode = CollisionDetectionMode.Continuous;
                               rigid.AddForce(Visual.forward * 50, ForceMode.Impulse);
                               item.transform.parent = null;
                           });
                    });
                }
            }
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
            if(hit.transform.CompareTag("Trigger Box")) continue;
            if(hit.transform.IsChildOf(transform)) continue;
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

        if (move != Vector3.zero && !dialoguMovement)
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
