using UnityEngine;

public class DeathPosition : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player =
                other.GetComponent<PlayerController>();

            if (player != null)
            {
                player.Damage(100000);
            }
        }
    }
}
