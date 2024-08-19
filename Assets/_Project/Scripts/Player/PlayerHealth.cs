using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    public bool IsPlay = true;

    [SerializeField] private int hearthCount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetHearthCount(int count)
    {
        hearthCount = count;
    }

    public void Damage(int value)
    {
        hearthCount -= value;
    }

    public void Die()
    {
        IsPlay = false;
    }
}
