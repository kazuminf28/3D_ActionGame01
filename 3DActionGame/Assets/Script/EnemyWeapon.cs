using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    private EnemyController enemy;
    private bool hasHit = false;
    void Start()
    {
        enemy = GetComponentInParent<EnemyController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(hasHit) return;
        if (other.CompareTag("Player"))
        {
            hasHit = true;
            float damage = enemy.GetDamage();
            other.GetComponent<PlayerController>().Damage(damage);
        }
    }

    public void ResetHit()
    {
        hasHit = false;
    }
}
