using UnityEngine;

namespace JJW._02_Script.UI.Card
{
    public class Card : MonoBehaviour
    {
        public UpgradeSO UpgradeSO;
        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }
    }
}