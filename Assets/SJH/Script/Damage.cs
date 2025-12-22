using UnityEngine;

public class Damage : MonoBehaviour
{
    private Player playerC;
    private int damage = 0;

    public int DamageIncrease = 1;

    private void Awake()
    {
        playerC = GetComponent<Player>();
        playerC.OnBump += IncreaseDamage;
        playerC.OnStop += () => damage = 0;
    }

    private void IncreaseDamage()
    {
        damage += DamageIncrease;
        Debug.Log(damage);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.)
        //{
        //    damage = 0;
        //}
    }
}
