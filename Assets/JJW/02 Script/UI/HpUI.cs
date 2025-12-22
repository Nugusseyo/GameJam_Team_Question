using System;
using Unity.VisualScripting;
using UnityEngine;

namespace JJW._02_Script.UI
{
    public class HpUI : MonoBehaviour
    {
        [SerializeField] private GameObject[] hpUI;
        [SerializeField] private IntEventChannel intEventChannel;

        private void Awake()
        {
            intEventChannel.OnEvent += UpdateUI;
        }

        private void UpdateUI(int hp)
        {
            for (int i = 0; i < hpUI.Length; i++)
            {
                hpUI[i].SetActive(i < hp);
            }
        }
    }
}
