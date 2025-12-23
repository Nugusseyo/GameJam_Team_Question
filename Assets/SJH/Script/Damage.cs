using UnityEngine;

public class Damage : MonoBehaviour
{
    private Player playerC;
    public int DamageIncrease = 1;

    private void Awake()
    {
        playerC = GetComponent<Player>();
    }
}
