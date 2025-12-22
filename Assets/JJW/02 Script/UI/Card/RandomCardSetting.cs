using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JJW._02_Script.UI.Card
{
    public class RandomCardSetting : MonoBehaviour
    {
        [SerializeField] private Image leftMapImage;
        [SerializeField] private Image rightMapImage;
        [SerializeField] private Image middleMapImage;

        [SerializeField] private TextMeshProUGUI leftMapText;
        [SerializeField] private TextMeshProUGUI rightMapText;
        [SerializeField] private TextMeshProUGUI middleMapText;

        private void OnEnable()
        {
            // GetRandomCard();
        }

        // private void GetRandomCard()
        // {
        //     int first = Random.Range(0, mapList.mapCardList.Count);
        //     int second = first;
        //     while (second == first)
        //     {
        //         second = Random.Range(0, mapList.mapCardList.Count);
        //     }
        //
        //     int third = second;
        //     while (second == third || third == first)
        //     {
        //         third = Random.Range(0, mapList.mapCardList.Count);
        //     }
        // }
    }
}

    
