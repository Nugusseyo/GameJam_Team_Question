using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JJW._02_Script.UI.Card
{
    public class RandomCardSetting : MonoBehaviour
    {
        [SerializeField] private Image leftCardImage;
        [SerializeField] private Image rightCardImage;
        [SerializeField] private Image middleCardImage;

        [SerializeField] private TextMeshProUGUI leftCardName;
        [SerializeField] private TextMeshProUGUI rightCardName;
        [SerializeField] private TextMeshProUGUI middleCardName;
        
        [SerializeField] private TextMeshProUGUI leftCardDesc;
        [SerializeField] private TextMeshProUGUI middleCardDesc;
        [SerializeField] private TextMeshProUGUI rightCardDesc;
        
        [SerializeField] private UpgradeListSO upgradeListSO;

        private void OnEnable()
        {
            GetRandomCard();
        }

        private void GetRandomCard()
        {
            int first = Random.Range(0, upgradeListSO.Upgrades.Count);
            leftCardImage.sprite = upgradeListSO.Upgrades[first].Image;
            leftCardName.text = upgradeListSO.Upgrades[first].Name;
            leftCardDesc.text = upgradeListSO.Upgrades[first].Desc;
            int second = first;
            while (second == first)
            {
                second = Random.Range(0, upgradeListSO.Upgrades.Count);
                middleCardImage.sprite = upgradeListSO.Upgrades[second].Image;
                middleCardName.text = upgradeListSO.Upgrades[second].Name;
                middleCardDesc.text = upgradeListSO.Upgrades[second].Desc;
            }
            int third = second;
            while (second == third || third == first)
            {
                third = Random.Range(0, upgradeListSO.Upgrades.Count);
                rightCardImage.sprite = upgradeListSO.Upgrades[third].Image;
                rightCardName.text = upgradeListSO.Upgrades[third].Name;
                rightCardDesc.text = upgradeListSO.Upgrades[third].Desc;
            }
        }
    }
}

    
