using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [SerializeField] private GameObject _dragonPrefab;
    [SerializeField] private GameObject _playerPrefab;

    [SerializeField] private Transform _dragonSpawnTransform;
    [SerializeField] private Transform _playerSpawnTranform;

    [SerializeField] private List<GameObject> _enemyList = new List<GameObject>();
    private List<Vector3> _slimesPos = new List<Vector3>();

    private EnemeyStates _enemeyStates = EnemeyStates.DRAGON;
    private bool _isEnemiesAttackableActive;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        foreach (var enemy in _enemyList)
        {
            enemy.GetComponent<AIController>().IsAttackable = false;
            _slimesPos.Add(enemy.transform.position);
        }
    }

    public void ActivateEnemiesAttack()
    {
        if (_isEnemiesAttackableActive)
            return;

        _isEnemiesAttackableActive = true;

        foreach (var enemy in _enemyList)
            enemy.GetComponent<AIController>().IsAttackable = true;
    }

    public void CheckFightState()
    {
        int count = 0;
        foreach (var item in _enemyList)
        {
            if (item.GetComponent<AIController>().IsDie)
                count++;
        }

        if (count >= _enemyList.Count)
            _enemeyStates = EnemeyStates.DRAGON;

        if (!PlayerHealth.Instance.IsDie)
        {
            _playerSpawnTranform = PlayerHealth.Instance.transform.parent.transform;
            Instantiate(_dragonPrefab, _dragonSpawnTransform.position, Quaternion.identity);
            PlayerHealth.Instance.transform.parent.position = _playerSpawnTranform.position;
            return;
        } 
        RestartFight();
    }

    public void RestartFight()
    {
        switch (_enemeyStates)
        {
            case EnemeyStates.SLIME:
                SpawnSlimes();
                break;
            case EnemeyStates.DRAGON:
                SpawnDragon();
                break;
        }
    }

    private void SpawnSlimes()
    {
        for (int i = 0; i < _enemyList.Count; i++)
        {
            AIController slime = Instantiate(_enemyList[i]).GetComponent<AIController>();
            slime.IsAttackable = true;
            slime.transform.position = _slimesPos[i];
        }
        Instantiate(_playerPrefab,_playerSpawnTranform.position,Quaternion.identity);
    }

    private void SpawnDragon()
    {
        Instantiate(_dragonPrefab, _dragonSpawnTransform.position, Quaternion.identity);
        PlayerHealth playerHealth = Instantiate(_playerPrefab, _playerSpawnTranform.position, Quaternion.identity).GetComponent<PlayerController>().PlayerHealth;
        playerHealth.SetHearthCount(5);
    }

}
public enum EnemeyStates
{
    SLIME,
    DRAGON
}
