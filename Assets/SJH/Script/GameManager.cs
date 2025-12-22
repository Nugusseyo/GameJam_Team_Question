using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private ParticleSystem bounceParticle;

    public Player Player;
    public TurnManager TurnManager;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Player = Instantiate(Player);
        Player.particleP = bounceParticle;
        TurnManager = Instantiate(TurnManager);
    }
}
