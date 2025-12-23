using CSILib.SoundManager.RunTime;
using UnityEngine;

public class ClearDetector : PMonoSingleton<ClearDetector>
{
    [SerializeField] private SoundPlayer mainSoundPlayer;
    [SerializeField] private SoundPlayer clearSoundPlayer;

    [ContextMenu("ClearThisGame")]
    public void ClearThisGame()
    {
        mainSoundPlayer.SoundPlayer_SoundStop();
        clearSoundPlayer.SoundPlayer_SoundPlay();
    }
    
}
