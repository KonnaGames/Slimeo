using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [SerializeField] private List<AIController> _enemyList = new List<AIController>();

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
            enemy.IsAttackable = false;
    }

    public void ActivateEnemiesAttack()
    {
        if (_isEnemiesAttackableActive)
            return;

        _isEnemiesAttackableActive = true;
        foreach (var enemy in _enemyList)
            enemy.IsAttackable = true;
    }
}
