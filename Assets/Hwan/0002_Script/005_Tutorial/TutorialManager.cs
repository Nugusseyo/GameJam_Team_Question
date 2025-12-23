namespace Hwan
{
    using DG.Tweening;
    using System;
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
        [SerializeField] private ObstacleSpawner spawner;
        [SerializeField] private GameObject cardSelect;
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
                    break;
                case TutorialType.KillEnemy:
                    GameManager.Instance.TurnManager.Turn = -1;
                    GameManager.Instance.TurnManager.PassTurn();
                    GameManager.Instance.Player.OnStop += GameManager.Instance.TurnManager.PassTurn;
                    break;
                case TutorialType.Obstacle:
                    spawner.SpawnObstacle(-9999999);
                    break;
                case TutorialType.ToolTip:
                    break;
                case TutorialType.Card:
                    cardSelect.SetActive(true);
                    break;
                case TutorialType.Turn:
                    StartCoroutine(WaitForNoIfTuto());
                    break;
                case TutorialType.Boss:
                    StartCoroutine(WaitForNoIfTuto());
                    break;
            }
        }

        private IEnumerator WaitForNoIfTuto()
        {
            yield return new WaitForSeconds(2f);
            TryPassTutorial(TutorialType.Boss);
            TryPassTutorial(TutorialType.Turn);
        }

        private void TutoOff()
        {
            tmpProUGUI.DOFade(0, 0.35f);

            currentTutoNum++;
            if (currentTutoNum >= tutorialSOs.Length)
            {
                EndTuto();
                return;
            }
            StartCoroutine(WaitForNextTutorial(currentTutoSO.WaitTime));
        }

        private void EndTuto()
        {
            SceneManager.Instance.OnLoadScene(0);
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