using csiimnida.CSILib.SoundManager.RunTime;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [field: SerializeField] public string SoundName { get; private set; }
    [SerializeField] bool playOnStart = true;
    private AudioSource audioSource;
    private void Start()
    {
        if (playOnStart)
            SoundPlayer_SoundPlay();
    }
    public void SoundPlayer_SoundPlay()
    {
        audioSource = SoundManager.Instance.PlaySoundWithObj(SoundName);
    }
    public void SoundPlayer_SoundStop()
    {
        if(audioSource !=null)
            audioSource.Stop();
    }
}
