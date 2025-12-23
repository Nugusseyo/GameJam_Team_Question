using csiimnida.CSILib.SoundManager.RunTime;
using CSILib.SoundManager.RunTime;
using System;
using System.Collections;
using UnityEngine;

public class BossEndingManager : PMonoSingleton<BossEndingManager>
{
    [SerializeField] private GameObject hideThings;
    
    public void BossEnding()
    {
        ClearDetector.Instance.ClearThisGame();
        Time.timeScale = 0.2f;
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.PlaySound("GameClear");
        hideThings.SetActive(true);
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0f;
    }
}
