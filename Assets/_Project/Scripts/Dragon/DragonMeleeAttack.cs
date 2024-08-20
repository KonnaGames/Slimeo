using UnityEngine;

public class DragonMeleeAttack : MonoBehaviour
{
    public bool IsAttack = false;
    private int Damage = 1;

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.TryGetComponent(out PlayerHealth playerHealth) && IsAttack)
        {
            playerHealth.Damage(1);
            IsAttack = false;
        }
    }
}