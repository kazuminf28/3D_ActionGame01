using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("敵の種類")]
    public GameObject Slime;
    public GameObject TurtleShell;

    public GameObject Dragon;

    public Transform EnemyPlace1;
    public Transform EnemyPlace2;

    // public Transform EnemyPlace3;

    private GameManager GM;
    public static int EnemyCount = 0;

    float TimeCount = 0;

    void Start()
    {
        Instantiate(Slime, EnemyPlace1.position, Quaternion.identity);
        Instantiate(TurtleShell, EnemyPlace2.position, Quaternion.identity);
    }

    public void EnemyDead()
    {
        EnemyCount--;
        if (EnemyCount == 0)
        {
            GM.GameClear();
        }
    }

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
