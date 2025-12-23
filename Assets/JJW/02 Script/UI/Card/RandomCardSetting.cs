using NUnit.Framework;
using System.Collections.Generic;
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

        [SerializeField] private Cards cards;
        [SerializeField] private Dictionary<UpgradeSO, int> maxStacks;
        
        public UpgradeListSO UpgradeListSO;

        private List<UpgradeSO> upgradeList = new();
        private void Awake()
        {
            upgradeList = UpgradeListSO.Upgrades;
        }

        private void OnEnable()
        {
            Dictionary<UpgradeSO, int> currentStacks = new();
            foreach (Card card in cards.MyCards)
            {
                currentStacks[card.UpgradeSO]++;
                if (maxStacks[card.UpgradeSO] == currentStacks[card.UpgradeSO])
                {
                    upgradeList.Remove(card.UpgradeSO);
                }
            }
            GetRandomCard();
        }

        private void GetRandomCard()
        {
            first = Random.Range(0, upgradeList.Count);
            leftCard.UpgradeSO = upgradeList[first];
            leftCardImage.sprite = upgradeList[first].Image;
            leftCardName.text = upgradeList[first].Name;
            leftCardDesc.text = upgradeList[first].Desc;
            second = first;
            while (second == first)
            {
                second = Random.Range(0, upgradeList.Count);
                middleCard.UpgradeSO = upgradeList[second];
                middleCardImage.sprite = upgradeList[second].Image;
                middleCardName.text = upgradeList[second].Name;
                middleCardDesc.text = upgradeList[second].Desc;
            }
            third = second;
            while (second == third || third == first)
            {
                third = Random.Range(0, upgradeList.Count);
                rightCard.UpgradeSO = upgradeList[third];
                rightCardImage.sprite = upgradeList[third].Image;
                rightCardName.text = upgradeList[third].Name;
                rightCardDesc.text = upgradeList[third].Desc;
            }
            Time.timeScale = 0f;
        }
    }
}

    
