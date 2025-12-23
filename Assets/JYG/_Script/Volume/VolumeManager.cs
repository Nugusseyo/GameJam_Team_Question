using System;
using System.Collections.Generic;
using UnityEngine;

public enum VolumeType
{
    Hit,
    Obstacle
}

public class VolumeManager : PMonoSingleton<VolumeManager>
{
    [SerializeField] private Volumes[] volumes;
    private Dictionary<VolumeType, VolumeValueChanger> volumeDictionary = new();

    private Coroutine defAfterInCoroutine;
    private Player player;
    protected override void Awake()
    {
        base.Awake();

        foreach (Volumes volume in volumes)
        {
            volumeDictionary.Add(volume.VolumeType, volume.VolumeChanger);
        }
    }

    private void Start()
    {
        foreach (VolumeValueChanger volume in volumeDictionary.Values)
        {
            volume.SetWeight(0);
        }
    }

    public void IncreaseVolume(VolumeType type, float time)
    {
        StartCoroutine(volumeDictionary[type].IncreaseWeight(time));
    }

    public void DecreaseVolume(VolumeType type, float time)
    {
        StartCoroutine(volumeDictionary[type].DecreaseWeight(time));
    }

    public void DefAfterInc(VolumeType type, float time)
    {
        if (defAfterInCoroutine != null) StopCoroutine(defAfterInCoroutine);
        defAfterInCoroutine = StartCoroutine(volumeDictionary[type].DecAfterInc(time));
    }

    public void SetVolume(VolumeType type, float value)
    {
        volumeDictionary[type].SetWeight(value);
    }

    public void ObstacleIncrease(float time, Color color)
    {
        volumeDictionary[VolumeType.Obstacle].SetColor(color);
        StartCoroutine(volumeDictionary[VolumeType.Obstacle].IncreaseWeight(time));
    }

    public void ObstacleDecrease(float time, Color color)
    {
        volumeDictionary[VolumeType.Obstacle].SetColor(color);
        StartCoroutine(volumeDictionary[VolumeType.Obstacle].DecreaseWeight(time));
    }
    public void ObstacleDefAfterInc(float time, Color color)
    {
        if (defAfterInCoroutine != null) StopCoroutine(defAfterInCoroutine);
        volumeDictionary[VolumeType.Obstacle].SetColor(color);
        defAfterInCoroutine = StartCoroutine(volumeDictionary[VolumeType.Obstacle].DecAfterInc(time));
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}

[Serializable]
public class Volumes
{
    [field: SerializeField] public VolumeType VolumeType { get; private set; }
    [field: SerializeField] public VolumeValueChanger VolumeChanger { get; private set; }
}

//딸깍 모노싱글톤
public class PMonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<T>();
                if (_instance == null)
                {
                    GameObject singleton = new GameObject(typeof(T).Name);
                    _instance = singleton.AddComponent<T>();
                }
            }

            return _instance;
        }

    }

    protected virtual void Awake()
    {
        T[] managers = FindObjectsByType<T>(FindObjectsSortMode.None);

        if (managers.Length > 1)
            Destroy(gameObject);
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }
}