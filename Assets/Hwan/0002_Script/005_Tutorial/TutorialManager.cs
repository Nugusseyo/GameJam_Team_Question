namespace Hwan
{
    using System.Collections;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class TutorialManager : MonoBehaviour
    {
        [SerializeField] private TutorialSO[] tutorialSOs;
        [SerializeField] private TutorialSO currentTutoSO;
        [SerializeField] private Image blackImage;
        [SerializeField] private TextMeshProUGUI tmpProUGUI;

        private int currentTutoNum;
        public bool IsUIOpen { get; private set; }
        public bool CanInput { get; private set; }

        private void Awake()
        {
            StartTutorial();
        }

        private void StartTutorial()
        {
            currentTutoNum = 0;
            TutoUIOn();
        }

        private void TutoUIOn()
        {
            currentTutoSO = tutorialSOs[currentTutoNum];

            blackImage.enabled = true;
            tmpProUGUI.enabled = true;
            tmpProUGUI.transform.position = currentTutoSO.Position;
            tmpProUGUI.text = currentTutoSO.Text;
        }

        private void TutoOff()
        {
            blackImage.enabled = false;
            tmpProUGUI.enabled = false;

            currentTutoNum++;
            if (currentTutoNum >= tutorialSOs.Length) return;
            StartCoroutine(WaitForNextTutorial(currentTutoSO.WaitTime));
        }

        private IEnumerator WaitForNextTutorial(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            TutoUIOn();
        }

        public void GetInput(InputType inputType)
        {
            if (inputType == currentTutoSO.NeedInput)
            {
                TutoOff();
            }
        }
    }
}