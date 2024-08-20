using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour
{
    private bool _isDamage = false;
    private int _damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out DragonHealthSystem dragonHealthSystem))
        {
            if (_isDamage) return;

            dragonHealthSystem.Damage(_damage);
            StartCoroutine(DestroyObjetc());
        }
    }

    private IEnumerator DestroyObjetc()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}