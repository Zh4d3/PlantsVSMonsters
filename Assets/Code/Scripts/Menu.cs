using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] TextMeshProUGUI healthUI;
    [SerializeField] TextMeshProUGUI roundUI;
    [SerializeField] TextMeshProUGUI selectedTowerUI;

    [SerializeField] Button dogButton;
    [SerializeField] Button penguinButton;
    [SerializeField] Button goldfishButton;
    [SerializeField] Button birdButton;
    [SerializeField] Button unicornButton;

    private void Start()
    {
        dogButton.onClick.AddListener(() => SetSelectedTower("Dog"));
        penguinButton.onClick.AddListener(() => SetSelectedTower("Penguin"));
        goldfishButton.onClick.AddListener(() => SetSelectedTower("Goldfish"));
        birdButton.onClick.AddListener(() => SetSelectedTower("Bird"));
        unicornButton.onClick.AddListener(() => SetSelectedTower("Unicorn"));
    }

    private void OnGUI()
    {
        currencyUI.text = LevelManager.main.currency.ToString();
        healthUI.text = LevelManager.main.health.ToString();
        roundUI.text = LevelManager.main.round.ToString();
    }

    private void SetSelectedTower(string towerName)
    {
        selectedTowerUI.text = towerName;
    }
}