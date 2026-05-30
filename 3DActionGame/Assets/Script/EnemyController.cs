using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [Header("MaxHP")]
    public float MaxHP;
    public Image HPGage;
    private float currentHP;
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
    [Header("Effect")]
    public GameObject Effect;
    public LineRenderer EffectLine;
    [Header("攻撃SE")]
    public AudioSource audioSource;
    public AudioClip[] AttackSE;

    Vector3 RandomDirection;
    bool IsWait = false;
    private EnemyManager EM;
    private GameObject player;
    private Animator anim;
    float Movetimer;
    float waitTime;
    float sqrDistance;
    bool Dead = false;
    bool Attacking = false;
    // float attackCooldown = 1.5f;
    float Cooltimer = 0f;
    [System.Serializable]
    public class EnemyAttackData
    {
        public string name;
        public float damage;
        public float cooldown;
        public GameObject WeaponObject;
    }
    public EnemyAttackData[] attacks;
    private EnemyAttackData currentAttack;
    void Start()
    {
        EM = FindAnyObjectByType<EnemyManager>();
        EnemyManager.EnemyCount++;
        currentHP = MaxHP;
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        waitTime = WaitTimer;
        SetRandomDirection();
        Movetimer = Random.Range(2f, 4f);
        // Destroy(gameObject, 5f);
    }

    
    void Update()
    {
        // Debug.Log("Attacking: " + Attacking);
        Cooltimer -= Time.deltaTime;
        Move();
        if (!Attacking && Cooltimer <= 0) Attack();
    }

    void Move()
    {
        if (Dead) return;
        if (Attacking)
        {
            Vector3 dir = player.transform.position - transform.position;
            dir.y = 0f;
            transform.rotation = Quaternion.LookRotation(dir);
            return;
        } 
        sqrDistance = (transform.position - player.transform.position).sqrMagnitude;
        if (sqrDistance > SearchRange * SearchRange) //ランダム移動処理
        {
            anim.SetBool("IsBattle", false);
            EffectLine.forceRenderingOff = false;
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
                if(Random.value < 0.5f)// 50%
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
            Vector3 pos = transform.position;
            pos += RandomDirection * MoveSpeed * 0.5f * Time.deltaTime;
            pos.y = transform.position.y;
            transform.position = pos;
            anim.SetBool("IsRun", true);
        } else if(sqrDistance > AttackRange * AttackRange)
        {
            anim.SetBool("IsBattle", true);
            EffectLine.forceRenderingOff = true;
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0f;
            transform.rotation = Quaternion.LookRotation(direction);
            transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z),
            Time.deltaTime * MoveSpeed
            );
            anim.SetBool("IsRun", true);
        } else
        {
            anim.SetBool("IsBattle", true);
            anim.SetBool("IsRun", false);
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0f;
            transform.rotation = Quaternion.LookRotation(direction);
            // transform.position = Vector3.MoveTowards(
            // transform.position,
            // new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z),
            // Time.deltaTime * MoveSpeed
            // );
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
        float percent = currentHP / MaxHP;
        HPGage.fillAmount = percent;
        // Debug.Log("ダメージを受けた");
        // anim.SetTrigger("IsHit");
        StartCoroutine(HitStop());
        if (currentHP <= 0 && Dead == false)
        {
            Dead = true;
            Die();
        }
    }

    void Die()
    {
        anim.SetTrigger("IsDead");
        var effect = Instantiate(Effect, transform.position, Quaternion.identity);
        EM.EnemyDead();
        Destroy(effect, 2f);
        Destroy(gameObject, 2f);
    }

    void Attack()
    {
        if (Cooltimer > 0f) return;
        sqrDistance = (transform.position - player.transform.position).sqrMagnitude;
        if(sqrDistance < AttackRange * AttackRange)
        {
            int rand = Random.Range(0, attacks.Length);
            currentAttack = attacks[rand];
            Cooltimer = currentAttack.cooldown;
            // Debug.Log(rand);
            anim.SetInteger("AttackIndex", rand);
            audioSource.PlayOneShot(AttackSE[rand]);
            anim.SetBool("IsAttack", true);
        }
    }

    public float GetDamage()
    {
        if (currentAttack == null) return 0;
        return currentAttack.damage;
    }

    public void StartAttack()
    {
        Attacking = true;
        // Debug.Log("攻撃開始");
    }

    public void AudioList2()
    {
        audioSource.PlayOneShot(AttackSE[1]);
    }

    public void EnemyCanMove()
    {
        // Debug.Log("動けるようになった");
        Attacking = false;
        anim.SetBool("IsAttack", false);
    }

    public void OnAttackStart()
    {
        currentAttack.WeaponObject.GetComponent<Collider>().enabled = true;
        currentAttack.WeaponObject.GetComponent<EnemyWeapon>().ResetHit();
    }

    public void OnAttackEnd()
    {
        currentAttack.WeaponObject.GetComponent<Collider>().enabled = false;
    }
}