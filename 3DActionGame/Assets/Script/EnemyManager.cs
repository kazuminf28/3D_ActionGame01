using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("敵の種類")]
    public GameObject Slime;
    public GameObject TurtleShell;

    public GameObject Dragon;

    public Transform EnemyPlace1;
    public Transform EnemyPlace2;

    public Transform EnemyPlace3;

    public GameManager GM;
    public static int EnemyCount = 0;

    void Start()
    {
        EnemyCount = 0;
        for(int i = 0; i < 5; i++) Instantiate(Slime, EnemyPlace1.position, Quaternion.identity);
        for(int i = 0; i < 3; i++) Instantiate(TurtleShell, EnemyPlace2.position, Quaternion.identity);
        Instantiate(Dragon, EnemyPlace3.position, Quaternion.identity);
    }

    public void EnemyDead()
    {
        EnemyCount--;
        if (EnemyCount == 0)
        {
            GM.GameClear();
        }
    }
}
