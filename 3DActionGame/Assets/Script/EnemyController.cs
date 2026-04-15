using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private GameObject player;
    private Animator anim;
    public float time;
    
    public float EnemySpeed;
    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        Destroy(this.gameObject, time);
    }

    
    void Update()
    {
        float sqrDistance = (transform.position - player.transform.position).sqrMagnitude;
        if (sqrDistance > 4f)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0f;
            transform.rotation = Quaternion.LookRotation(direction);
            transform.position = Vector3.MoveTowards(
            transform.position,
            player.transform.position,
            Time.deltaTime * 2.0f
            );
            anim.SetBool("IsRun", true);
        } else
        {
            anim.SetBool("IsRun", false);
        }
    }
}
