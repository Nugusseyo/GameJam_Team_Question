namespace Hwan
{
    using DG.Tweening;
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    public class TutorialManager : MonoBehaviour
    {
        #region Singleton
        public static TutorialManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            tmpRectTrm = tmpProUGUI.GetComponent<RectTransform>();

            if (DoTuto == false) return;
            StartTutorial();
        }
        #endregion

        [field: SerializeField] public bool DoTuto { get; private set; } = false;
        [SerializeField] private TutorialSO[] tutorialSOs;
        [SerializeField] private TextMeshProUGUI tmpProUGUI;
        private RectTransform tmpRectTrm;
        private TutorialSO currentTutoSO;

        private int currentTutoNum;
        public bool IsUIOpen { get; private set; }
        public bool CanInput { get; private set; }

        private void StartTutorial()
        {
            currentTutoNum = 0;
            tmpRectTrm.gameObject.SetActive(true);
            TutoUIOn();
        }

        private void TutoUIOn()
        {
            currentTutoSO = tutorialSOs[currentTutoNum];

            tmpProUGUI.DOFade(0.65f, 0.35f);
            tmpProUGUI.text = currentTutoSO.Text;
            tmpRectTrm.anchoredPosition = currentTutoSO.Position;

            switch (currentTutoSO.TutoType)
            {
                case TutorialType.None:
                    break;
                case TutorialType.Throw:
                    break;
                case TutorialType.Cancle:
                    GameManager.Instance.TurnManager.Turn = -1;
                    GameManager.Instance.TurnManager.PassTurn();
                    break;
                case TutorialType.KillEnemy:
                    break;
                case TutorialType.Obstacle:
                    break;
                case TutorialType.ToolTip:
                    break;
                case TutorialType.Card:
                    break;
                case TutorialType.Turn:
                    break;
                case TutorialType.Boss:
                    break;
            }
        }

        private void TutoOff()
        {
            tmpProUGUI.DOFade(0, 0.35f);

            currentTutoNum++;
            if (currentTutoNum >= tutorialSOs.Length) return;
            StartCoroutine(WaitForNextTutorial(currentTutoSO.WaitTime));
        }

        private IEnumerator WaitForNextTutorial(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            TutoUIOn();
        }

        public void TryPassTutorial(TutorialType tutoType)
        {
            if (DoTuto == false) return;

            if (currentTutoSO.TutoType == tutoType)
            {
                TutoOff();
            }
        }
    }
}