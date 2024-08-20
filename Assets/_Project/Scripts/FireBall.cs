using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireBall : MonoBehaviour
{
    private int _damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.Damage(_damage);
        }
    }
}