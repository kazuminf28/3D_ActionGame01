using UnityEngine;

public class PlayerAttackComb : MonoBehaviour
{
    public bool canCombo;
    public void EnableCombo()
    {
        canCombo = true;
    }

    public void DisableCombo()
    {
        canCombo = false;
    }
}
