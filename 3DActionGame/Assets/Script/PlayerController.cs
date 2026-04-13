using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    [Header("プレイヤーのスピード")]
    public float PlayerSpeed;

    [Header("ダッシュ時のスピード")]
    public float SprintSpeed;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
       Move();
    }

#region MoveControls
    void Move()
    {
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

    
}
