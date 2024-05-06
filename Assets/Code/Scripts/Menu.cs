using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour {

    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] TextMeshProUGUI healthUI;
    [SerializeField] TextMeshProUGUI roundUI;

    private void OnGUI() {
        currencyUI.text = LevelManager.main.currency.ToString();
        healthUI.text = LevelManager.main.health.ToString();
        roundUI.text = LevelManager.main.round.ToString();
    }

}
