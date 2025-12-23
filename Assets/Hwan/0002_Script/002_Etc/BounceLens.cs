using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class BounceLens : MonoBehaviour
{
    [SerializeField] private float maxDis;
    [SerializeField] private float resetTime;
    private CinemachineCamera cinCam;

    private float originLens;

    private void Awake()
    {
        cinCam = GetComponent<CinemachineCamera>();
        originLens = cinCam.Lens.OrthographicSize;
    }

    public void SetLens(float t)
    {
        cinCam.Lens.OrthographicSize = Mathf.Lerp(originLens, maxDis, t);
    }

    public void ResetLens()
    {
        StartCoroutine(ResetLensCor());
    }

    private IEnumerator ResetLensCor()
    {
        float startLens = cinCam.Lens.OrthographicSize;
        float elapsed = 0f;

        while (elapsed < resetTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / resetTime;

            cinCam.Lens.OrthographicSize =
                Mathf.Lerp(startLens, originLens, t);

            yield return null;
        }

        // 오차 방지용 보정
        cinCam.Lens.OrthographicSize = originLens;
    }

}
