using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class BounceLens : MonoBehaviour
{
    [SerializeField] private float maxDis;
    [SerializeField] private float resetTime;
    [SerializeField] private float overshootAmount = 0.3f; // 안쪽으로 더 들어가는 정도
    [SerializeField] private float overshootRatio = 0.6f;  // 전체 시간 중 1차 구간 비율 (0~1)

    private CinemachineCamera cinCam;

    private float originLens;

    private void Awake()
    {
        cinCam = GetComponent<CinemachineCamera>();
        originLens = cinCam.Lens.OrthographicSize;
    }

    public void SetLens(float t)
    {
        if (resetCor != null)
            StopCoroutine(resetCor);
        cinCam.Lens.OrthographicSize = Mathf.Lerp(originLens, maxDis, t);
    }

    private Coroutine resetCor;

    public void ResetLens()
    {
        if (resetCor != null)
            StopCoroutine(resetCor);

        resetCor = StartCoroutine(ResetLensCor());
    }


    private IEnumerator ResetLensCor()
    {
        float startLens = cinCam.Lens.OrthographicSize;

        float targetInner = originLens - overshootAmount;

        float time1 = resetTime * overshootRatio;
        float time2 = resetTime * (1f - overshootRatio);

        float elapsed = 0f;

        while (elapsed < time1)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / time1;

            cinCam.Lens.OrthographicSize =
                Mathf.Lerp(startLens, targetInner, t);

            yield return null;
        }

        elapsed = 0f;

        while (elapsed < time2)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / time2;

            cinCam.Lens.OrthographicSize =
                Mathf.Lerp(targetInner, originLens, t);

            yield return null;
        }

        cinCam.Lens.OrthographicSize = originLens;
    }
}
