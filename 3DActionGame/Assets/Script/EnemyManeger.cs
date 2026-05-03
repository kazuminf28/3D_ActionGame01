using UnityEngine;

public class EnemyManeger : MonoBehaviour
{
    [Header("敵の種類")]
    public GameObject Slime;
    public GameObject TurtleShell;

    public Transform EnemyPlace1;
    public Transform EnemyPlace2;

    float TimeCount = 0;

    void Update()
    {
        TimeCount += Time.deltaTime;
        // if (TimeCount >= 5f)
        // {
        //     Instantiate(Slime, EnemyPlace1.position, Quaternion.identity);
        //     Instantiate(TurtleShell, EnemyPlace2.position, Quaternion.identity);
        //     TimeCount = 0;
        // }
    }
}
