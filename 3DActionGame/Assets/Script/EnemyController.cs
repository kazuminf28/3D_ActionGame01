using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("MaxHP")]
    public float MaxHP;
    private float currentHP;
    [Header("Power")]
    public float Power;
    [Header("Defense")]
    public float Defense;
    [Header("MoveSpeed")]
    public float MoveSpeed;
    [Header("AttackRange")]
    public float AttackRange;
    [Header("SearchRange")]
    public float SearchRange;
    [Header("WaitTimer")]
    public float WaitTimer;
    Vector3 RandomDirection;
    bool IsWait = false;
    private GameObject player;
    private Animator anim;
    float Movetimer;
    float waitTime;
    void Start()
    {
        currentHP = MaxHP;
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        waitTime = WaitTimer;
        SetRandomDirection();
        Movetimer = Random.Range(2f, 4f);
    }

    
    void Update()
    {
        Move();
    }

    void Move()
    {
        float sqrDistance = (transform.position - player.transform.position).sqrMagnitude;

        if (sqrDistance < SearchRange)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0f;
            transform.rotation = Quaternion.LookRotation(direction);
            transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z),
            Time.deltaTime * MoveSpeed
            );
            anim.SetBool("IsRun", true);
        } else //Random移動処理
        {
            if (IsWait)
            {
                waitTime -= Time.deltaTime;
                anim.SetBool("IsRun", false);
                if (waitTime < 0f)
                {
                    IsWait = false;
                    Movetimer = Random.Range(2f, 4f);
                    SetRandomDirection();
                }
                return;
            }
            Movetimer -= Time.deltaTime;
            if(Movetimer <=0f){
                if(Random.value < 0.3f)// 30%
                {
                    IsWait = true;
                    waitTime = WaitTimer;
                    return;
                }
                SetRandomDirection();
                Movetimer = Random.Range(2f, 4f);
            }
            Quaternion targetRotation = Quaternion.LookRotation(RandomDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2f);
            transform.position += RandomDirection * MoveSpeed * 0.5f * Time.deltaTime;
            anim.SetBool("IsRun", true);
        }
    }

    void SetRandomDirection()
    {
        float angle = Random.Range(0f, 360f);
        float rad = angle * Mathf.Deg2Rad;
        RandomDirection = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad));
    }

    IEnumerator  HitStop()
    {
        float originalSpeed = MoveSpeed;
        MoveSpeed = 0f;
        yield return new WaitForSeconds(0.2f);
        MoveSpeed = originalSpeed;
    }

    public void HitDamage(float damage)
    {
        float finaldamage = Mathf.Max(damage - Defense, 1f);
        currentHP -= finaldamage;
        // anim.SetTrigger("IsHit");
        StartCoroutine(HitStop());
        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // anim.SetTrigger("IsDead");
        Destroy(gameObject, 2f);
    }
}
