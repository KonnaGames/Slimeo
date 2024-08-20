using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public bool IsDie;
    public bool IsDamage = true;
    public eSize SlimeSize;
    public bool _isAttackable;

    public bool IsAttackable 
    {
        get
        {
            return _isAttackable; 
        }
        set
        {
            _isAttackable = value;
            if (_isAttackable)
            {
                _isJump = false;
                _randomMove = false;
                SetTarget();
            }
        } 
    } 

    [SerializeField] private StartAnimationState _startAnimationState;
    [SerializeField] public PlayerController TargetPlayer;  
    
    private Animator _animator;
    private NavMeshAgent _agent;

    [SerializeField] private float _moveSpeed;
    private float _maxOffset = 10f;
    private float _safeDistance = 10f;
    
    private bool _randomMove;
    private bool _targetPointReached;

    private bool _isJump;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>(); 
        _animator = GetComponent<Animator>();
        
        _agent.speed = _moveSpeed;

        SetScaleSizeAtStart();
        StartingAnimation();
    }
    
    private void SetScaleSizeAtStart()
    {
        float scale = PlayerScaleController.Instance.GetScaleByEnum(SlimeSize);
        transform.localScale = new Vector3(scale, scale, scale);
    }

    void Update()
    {
        _animator.SetFloat("speed", _agent.velocity.magnitude);

        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            _targetPointReached = true;
            _agent.velocity = Vector3.zero;
        }
        
        if (_randomMove)
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance)
                StartCoroutine(RandomMove());
        }
    }

    private void StartingAnimation()
    {
        switch (_startAnimationState)
        {
            case StartAnimationState.WALK:
                _randomMove = true;
                StartCoroutine(RandomMove());
                break;
            case StartAnimationState.JUMP:
                _isJump = true;
                StartCoroutine(Jump());
                break;
            default:
                break;
        }
    }

    private IEnumerator Jump()
    {
        while (true)
        {
            if (!_isJump)
                break;

            var rnd = Random.Range(0.2f, 3f);
            yield return new WaitForSeconds(rnd);
            _animator.SetTrigger("Jump");
        }
    }

    private IEnumerator RandomMove()
    {
        if (!IsAttackable)
        {
            _targetPointReached = false;
            _agent.SetDestination(GetRandomPos(_maxOffset));
            yield return new WaitUntil(() => _targetPointReached);
        }
        yield return null;
    }

    private Vector3 GetRandomPos(float maxOffset)
    {
        var rndPosX = Random.Range(transform.position.x - maxOffset, transform.position.x + maxOffset);
        var rndPosZ = Random.Range(transform.position.z - maxOffset, transform.position.z + maxOffset);
        Vector3 pos = new Vector3(rndPosX, transform.position.y, rndPosZ);
        return pos;
    }

    private void SetTarget()
    {
        if (!IsAttackable || TargetPlayer == null)
            return;

        Vector3 destination = transform.position;

        if ((int)SlimeSize <= (int)PlayerScaleController.Instance.SlimeSize)
        {
            Vector3 direction = (transform.position - TargetPlayer.transform.position).normalized;
            destination = transform.position + direction * _agent.stoppingDistance * 3;

            if (Vector3.Distance(transform.position , TargetPlayer.transform.position)  < _safeDistance)
            {
                Vector3 randomDirection;
                do
                {
                    randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
                    destination = transform.position + randomDirection * Random.Range(1f, _safeDistance);
                }
                while (Vector3.Dot(randomDirection, (TargetPlayer.transform.position - transform.position).normalized) > 0);
            }
            else
            {
                destination = transform.position;
            }
        }
        else 
        {
            destination = TargetPlayer.transform.position;
        }

        StartCoroutine(MoveToTarget(destination));
    }

    private IEnumerator MoveToTarget(Vector3 destination)
    {
        _targetPointReached = false;
        _agent.SetDestination(destination);
        yield return new WaitUntil(() => _targetPointReached);
        SetTarget();
    }
}

public enum StartAnimationState
{
    IDLE,
    WALK,
    JUMP,
}