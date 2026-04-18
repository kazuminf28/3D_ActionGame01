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
        if(other.tag == "Enemy")
        {
            float damage = player.AttackDamage();
            other.GetComponent<EnemyController>().HitDamage(damage);
        }
    }
}
