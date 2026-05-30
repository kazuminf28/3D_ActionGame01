using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    [Header("HP")]
    public float MaxHP;
    public float HP;
    public Image HPGage;
    [Header("Power")]
    public float Power;
    [Header("Defense")]
    public float Defense;
    [Header("プレイヤーのスピード")]
    public float PlayerSpeed;

    [Header("ダッシュ時のスピード")]
    public float SprintSpeed;

    public Collider WeaponCollider;
    bool switchMove = true;
    [Header("DashAttack")]
    Vector3 dashVelocity;
    bool isDashAttacking = false;

    public AttackType currentAttack = AttackType.Normal;
    float ComboStep = 0;
    [Header("剣の音")]
    public AudioSource audioSource;
    public AudioClip AttackSE;
    public AudioClip DashAttackSE;
    Rigidbody rb;
    private bool Dead = false;
    private bool Jumping = false;
    [Header("Dodge")]
    public float DodgeSpeed = 10f;
    public float DodgeTime = 0.3f;
    Vector3 DodgeDirection;
    private bool Dodging = false;
    private bool Invincible = false;
    private GameManager GM;
    public enum AttackType
    {
        Normal,
        Dash
    }

    void Start()
    {
        HP = MaxHP;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
       Move();
       Attack();
       Dodge();
       if (isDashAttacking)
        {
            rb.MovePosition(transform.position + dashVelocity * Time.deltaTime);
            dashVelocity *= 0.99f; //減速
        }
    }

#region MoveControls
    void Move()
    {
        if(Dead) return;
        if(!switchMove) return;
        if(Dodging) return;
        //カメラの向き基準の正面方向ベクトル
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        Vector3 x = Camera.main.transform.right * Input.GetAxis("Horizontal") * PlayerSpeed;
        Vector3 z = cameraForward * Input.GetAxis("Vertical") * PlayerSpeed;
        Vector3 move = x + z;
        bool Sprint = Input.GetKey(KeyCode.LeftShift);

        float move_speed = Sprint ? SprintSpeed : PlayerSpeed; 

        if (move.magnitude > 0)
        {
            anim.SetBool("IsRun", true);
            anim.SetBool("IsSprint", Sprint);
            move = move.normalized;
            // 向きを入力方向に
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.15f);
            // 実際に移動
            transform.position += move * Time.deltaTime * move_speed;
        } else
        {
            anim.SetBool("IsRun", false);
            anim.SetBool("IsSprint", false);
        }
    }

    void Dodge()
    {
        if(Dead || !switchMove || Jumping || Dodging) return;
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 x = Camera.main.transform.right * Input.GetAxis("Horizontal");
            Vector3 z = cameraForward * Input.GetAxis("Vertical");
            DodgeDirection = (x + z).normalized;

            if (DodgeDirection == Vector3.zero)
            {
                DodgeDirection = transform.forward;
            }
            StartCoroutine(DodgeCoroutine());
        }
    }

    IEnumerator DodgeCoroutine()
    {
        Dodging = true;
        switchMove = false;
        anim.SetTrigger("IsDodge");
        float dodgetimer = 0;
        if (dodgetimer < DodgeTime)
        {
            Quaternion targetRotation = Quaternion.LookRotation(DodgeDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.15f);
            transform.position += DodgeDirection * DodgeSpeed * Time.deltaTime;
            dodgetimer += Time.deltaTime;
            yield return null;
        }
        switchMove = true;
        Dodging = false;
    }

    public void OnInvincible()
    {
        Invincible = true;
    }

    public void OffInvincible()
    {
        Invincible = false;
    }

    void Jump()
    {
        if(Dead) return;
        if(!switchMove) return;
        if(Jumping) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // anim.SetTrigger("IsJump");
        }
    }
#endregion

#region AttackControls
    void Attack()
    {
        if(Dead) return;
        if(Jumping) return;
        bool Sprint = Input.GetKey(KeyCode.LeftShift);
        if (Input.GetMouseButtonDown(0))
        {
            switchMove = false;
            if (Sprint && anim.GetBool("IsRun"))
            {
                currentAttack = AttackType.Dash;
                anim.SetTrigger("IsDushAttack");
                audioSource.PlayOneShot(DashAttackSE);
                // anim.SetBool("CanCombo", combo.canCombo);
            // } else if (combo.canCombo)
            // {
            //     anim.SetTrigger("IsAttack");
            } else
            {
                currentAttack = AttackType.Normal;
                anim.SetTrigger("IsAttack");
                audioSource.PlayOneShot(AttackSE);
            }
        }
    }

    public void StartDashAttack()
    {
        isDashAttacking = true;
        dashVelocity = transform.forward * 10f;
    }

    public void EndDashAttack()
    {
        isDashAttacking = false;
        dashVelocity = Vector3.zero;
    }

    public void NextCombo()
    {
        ComboStep++;
    }

    public void ResetCombo()
    {
        ComboStep = 0;
    }

    public float AttackDamage()
    {
        float damage = Power;
        switch (ComboStep)
        {
            case 0:
                damage *= 1.1f;
                break;
            case 1:
                damage *= 1.2f;
                break;
            case 2:
                damage *= 1.4f;
                break;
            default:
                damage *= 1.8f;
                break;
        }
        if (currentAttack == AttackType.Dash)
        {
            damage *= 1.5f;
        }

        return damage;
    }
#endregion 

#region Weapon Contoroller
    void OnWeapon()
    {
        WeaponCollider.enabled = true;
    }

    void OffWeapon()
    {
        WeaponCollider.enabled = false;
    }
#endregion 

#region 
    public void CanMove()
    {
        switchMove = true;
    }
#endregion

    public void Damage(float damage)
    {
        if (Dead) return;
        if (Invincible) {
            Debug.Log("回避");
            return;
        }
        HP -= Mathf.Max(damage - Defense, 1f);
        Debug.Log(HP);
        anim.SetTrigger("GetHit");
        float percent = HP / MaxHP;
        HPGage.fillAmount = percent;
        if (HP <= 0)
        {
            Dead = true;
            Die();
        }
    }

    void Die()
    {
        anim.SetBool("IsDead",true);
        GM.GameOver();
    }
}