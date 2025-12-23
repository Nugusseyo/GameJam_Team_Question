using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JJW._02_Script.UI.Card
{
    public class RandomCardSetting : MonoBehaviour
    {
        public int first;
        public int second;
        public int third;
        
        [SerializeField] private Image leftCardImage;
        [SerializeField] private Image rightCardImage;
        [SerializeField] private Image middleCardImage;
        
        [SerializeField] private Card leftCard;
        [SerializeField] private Card rightCard;
        [SerializeField] private Card middleCard;

        [SerializeField] private TextMeshProUGUI leftCardName;
        [SerializeField] private TextMeshProUGUI rightCardName;
        [SerializeField] private TextMeshProUGUI middleCardName;
        
        [SerializeField] private TextMeshProUGUI leftCardDesc;
        [SerializeField] private TextMeshProUGUI middleCardDesc;
        [SerializeField] private TextMeshProUGUI rightCardDesc;
        
        public UpgradeListSO UpgradeListSO;

        private void OnEnable()
        {
            GetRandomCard();
        }

        private void GetRandomCard()
        {
            first = Random.Range(0, UpgradeListSO.Upgrades.Count);
            leftCard.UpgradeSO =  UpgradeListSO.Upgrades[first];
            leftCardImage.sprite = UpgradeListSO.Upgrades[first].Image;
            leftCardName.text = UpgradeListSO.Upgrades[first].Name;
            leftCardDesc.text = UpgradeListSO.Upgrades[first].Desc;
            second = first;
            while (second == first)
            {
                second = Random.Range(0, UpgradeListSO.Upgrades.Count);
                middleCard.UpgradeSO = UpgradeListSO.Upgrades[second];
                middleCardImage.sprite = UpgradeListSO.Upgrades[second].Image;
                middleCardName.text = UpgradeListSO.Upgrades[second].Name;
                middleCardDesc.text = UpgradeListSO.Upgrades[second].Desc;
            }
            third = second;
            while (second == third || third == first)
            {
                third = Random.Range(0, UpgradeListSO.Upgrades.Count);
                rightCard.UpgradeSO = UpgradeListSO.Upgrades[third];
                rightCardImage.sprite = UpgradeListSO.Upgrades[third].Image;
                rightCardName.text = UpgradeListSO.Upgrades[third].Name;
                rightCardDesc.text = UpgradeListSO.Upgrades[third].Desc;
            }
            Time.timeScale = 0f;
        }
    }
}

    
