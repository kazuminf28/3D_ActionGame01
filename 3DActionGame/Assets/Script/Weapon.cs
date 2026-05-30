using UnityEngine;

public class Weapon : MonoBehaviour
{
    private PlayerController player;
    [Header("剣の音")]
    public AudioSource audioSource;
    public AudioClip HitSE;
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
            audioSource.PlayOneShot(HitSE);
            float damage = player.AttackDamage();
            enemy.HitDamage(damage);
        }
    }
}
