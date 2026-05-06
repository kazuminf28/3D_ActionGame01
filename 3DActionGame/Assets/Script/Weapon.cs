using UnityEngine;

public class Weapon : MonoBehaviour
{
    private PlayerController player;
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        if (player == null) Debug.Log("NULL");
    }
    private void OnTriggerEnter(Collider other)
    {
        EnemyController enemy = other.GetComponentInParent<EnemyController>();
        if(enemy != null)
        {
            float damage = player.AttackDamage();
            enemy.HitDamage(damage);
        }
    }
}
