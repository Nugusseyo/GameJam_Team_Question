using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private ParticleSystem bounceParticle;
    [SerializeField] private ParticleSystem bounceParticle2;
  
    public Player Player;
    public TurnManager TurnManager;
    public ToolTip Tooltip;
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
        Player.particleP2 = bounceParticle2;
        TurnManager = Instantiate(TurnManager);
        Tooltip = Instantiate(Tooltip);
    }
}
