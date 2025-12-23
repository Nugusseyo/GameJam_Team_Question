using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct MaxStack
{
    public UpgradeSO UpgradeSO;
    public int Stack;

}

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
        [SerializeField] private MaxStack[] maxStack;
        
        public UpgradeListSO UpgradeListSO;

        private List<UpgradeSO> upgradeList = new();
        private Dictionary<UpgradeSO, int> maxStacks = new();
        private void Awake()
        {
            foreach(MaxStack max in maxStack)
            {
                maxStacks.Add(max.UpgradeSO,max.Stack);
            }
        }

        private void OnEnable()
        {
            upgradeList = new List<UpgradeSO>(UpgradeListSO.Upgrades);
            Dictionary<UpgradeSO, int> currentStacks = new();
            foreach(UpgradeSO card in UpgradeListSO.Upgrades)
            {
                currentStacks.Add(card,0);
            }
            List<UpgradeSO> currentCards = new List<UpgradeSO>();
            foreach(Card card in cards.GetComponentsInChildren<Card>())
            {
                currentCards.Add(card.UpgradeSO);
            }
            foreach (Card card in cards.MyCards)
            {
                if (currentCards.FirstOrDefault(x => x == card.UpgradeSO) == null) continue;
                currentStacks[card.UpgradeSO] = card.GetComponent<Stack>().CurrentStack;
                if (maxStacks[card.UpgradeSO] <= currentStacks[card.UpgradeSO])
                {
                    upgradeList.Remove(card.UpgradeSO);
                    Debug.Log(card.UpgradeSO.Name);
                }
            }
            GetRandomCard();
        }

        private void GetRandomCard()
        {
            first = UnityEngine.Random.Range(0, upgradeList.Count);
            leftCard.UpgradeSO = upgradeList[first];
            leftCardImage.sprite = upgradeList[first].Image;
            leftCardName.text = upgradeList[first].Name;
            leftCardDesc.text = upgradeList[first].Desc;
            second = first;
            while (second == first)
            {
                second = UnityEngine.Random.Range(0, upgradeList.Count);
                middleCard.UpgradeSO = upgradeList[second];
                middleCardImage.sprite = upgradeList[second].Image;
                middleCardName.text = upgradeList[second].Name;
                middleCardDesc.text = upgradeList[second].Desc;
            }
            third = second;
            while (second == third || third == first)
            {
                third = UnityEngine.Random.Range(0, upgradeList.Count);
                rightCard.UpgradeSO = upgradeList[third];
                rightCardImage.sprite = upgradeList[third].Image;
                rightCardName.text = upgradeList[third].Name;
                rightCardDesc.text = upgradeList[third].Desc;
            }
            Time.timeScale = 0f;
        }
    }
}

    
