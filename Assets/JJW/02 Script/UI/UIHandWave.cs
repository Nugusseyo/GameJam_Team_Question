using UnityEngine;

namespace JJW._02_Script.UI
{
    public class UIHandWave : MonoBehaviour
    {
        [Header("Wave Settings")]
        [SerializeField] private float angle = 15f;     
        [SerializeField] private float speed = 5f;        
        [SerializeField] private float startAngle = 0f;  

        private RectTransform rect;
        private float timeOffset;

        private void Awake()
        {
            rect = GetComponent<RectTransform>();
            timeOffset = Random.Range(0f, Mathf.PI * 2f);
        }

        private void Update()
        {
            float z =
                startAngle +
                Mathf.Sin(Time.time * speed + timeOffset) * angle;

            rect.localRotation = Quaternion.Euler(0, 0, z);
        }
    }
}