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
    [SerializeField] private List<Transform> _slimesPos = new List<Transform>();
    [SerializeField] List<GameObject> _spawnedSlimes = new List<GameObject>();

    private bool _isEnemiesAttackableActive;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
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
        {
            if (!PlayerHealth.Instance.IsDie)
            {
                Instantiate(_dragonPrefab, _dragonSpawnTransform.position, Quaternion.identity);
                PlayerHealth.Instance.SetHearthCount(5);
            }
        }
    }
}
