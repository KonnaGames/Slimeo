using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class DragonController : MonoBehaviour
{
    private Transform PlayerTransform;
    [SerializeField] private DragonMeleeAttack _dragonMeleeAttack;

    [SerializeField] public GameObject _fireBallPrefab;
    [SerializeField] public Transform _fireBallSpawnPoint;

    private Animator _animator;
    private NavMeshAgent _agent;

    private bool _isFireAttack = false;
    private bool _isMeleeAttack = false;

    private void Start()
    {
        PlayerTransform = PlayerHealth.Instance.transform;
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();

        StartCoroutine(MoveToTarget());
    }

    private IEnumerator MoveToTarget()
    {
        while (true)
        {
            if (GetDistance() < 5)
            {
                _agent.isStopped = true;
                _agent.velocity = Vector3.zero;
            }

            if (GetDistance() >= 15f  &&  !_isFireAttack)
            {
                _agent.isStopped = true;
                StartCoroutine(FireAttack());
                yield return new WaitForSeconds(1.2f);
            }
            else if (GetDistance() >= 0 && GetDistance() <= 10f && !_isMeleeAttack)
            {
                _agent.isStopped = true;
                StartCoroutine(MeleeAttack());
                yield return new WaitForSeconds(1.2f);
            }
            else
            {
                _agent.isStopped = false;
                _agent.SetDestination(PlayerTransform.position);
            }

            yield return null;
        }
    }

    private IEnumerator MeleeAttack()
    {
        if (!_isMeleeAttack)
        {
            _isMeleeAttack = true;
            _animator.SetTrigger("attack");
            yield return new WaitForSeconds(0.5f);
            _dragonMeleeAttack.IsAttack = true;
        }
        yield return new WaitForSeconds(3f);
        _isMeleeAttack = false;
    }

    private IEnumerator FireAttack()
    {
        if (!_isFireAttack)
        {
            _isFireAttack = true;
            _animator.SetTrigger("fire");

            yield return new WaitForSeconds(1.7f);

            StartCoroutine(SpawnFireball());
        }
        yield return new WaitForSeconds(10f);

        _isFireAttack = false;
    }

    private IEnumerator SpawnFireball()
    {
        GameObject fireBall = Instantiate(_fireBallPrefab);
        fireBall.transform.position = _fireBallSpawnPoint.position;
        fireBall.transform.GetChild(0).GetComponent<ParticleSystem>().Play();

        Vector3 direction = (PlayerTransform.position - _fireBallSpawnPoint.position).normalized;
        fireBall.AddComponent<Rigidbody>().AddForce(direction * 50, ForceMode.Impulse);

        yield return new WaitForSeconds(5f);

        Destroy(fireBall);
    }

    private float GetDistance()
    {
        if (PlayerTransform == null)
        {
            Debug.Log("Target Null");
        }
        
        return Vector3.Distance(transform.position, PlayerTransform.position);
    }
}