using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private PlayerAttackComb combo;
    [Header("プレイヤーのスピード")]
    public float PlayerSpeed;

    [Header("ダッシュ時のスピード")]
    public float SprintSpeed;

    public Collider WeaponCollider;
    bool switchMove = true;
    [Header("DashAttack")]
    Vector3 dashVelocity;
    bool isDashAttacking = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        combo = GetComponent<PlayerAttackComb>();
    }

    void Update()
    {
       Move();
       Attack();
       if (isDashAttacking)
        {
            transform.position += dashVelocity * Time.deltaTime;
            dashVelocity *= 0.99f; //減速
        }
    }

#region MoveControls
    void Move()
    {
        if(!switchMove) return;
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
#endregion

#region AttackControls
    void Attack()
    {
        bool Sprint = Input.GetKey(KeyCode.LeftShift);
        if (Input.GetMouseButtonDown(0))
        {
            switchMove = false;
            if (Sprint && anim.GetBool("IsRun"))
            {
                anim.SetTrigger("IsDushAttack");
                anim.SetBool("CanCombo", combo.canCombo);
            // } else if (combo.canCombo)
            // {
            //     anim.SetTrigger("IsAttack");
            } else
            {
                anim.SetTrigger("IsAttack");
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
}