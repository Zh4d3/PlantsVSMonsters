using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;
    public GameOverScreen GameOverScreen;

    public double currency;
    public int health;
    public int round;

    private void Awake() {
        main = this;
    }

    private void Start() {
        currency = 650;
        health = 100;
        round = 1;
    }

    public void IncreaseCurrency(int amount) {
        if (round <= 50)
        {
            currency += amount;
        } else if (round <= 60)
        {
            currency += amount * 0.5;
        } else if (round <= 85)
        {
            currency += amount* 0.2;
        } else if (round <= 100) 
        {
            currency += amount * 0.1;
        } else if (round <= 120) {
            currency += amount * 0.05;
        } else
        {
            currency += amount * 0.02;
        }
    }

    public bool SpendCurrency(int amount) {
        if (amount <= currency) {
            currency -= amount;
            return true;
        } else {
            Debug.Log("You do not have enough to purchase this item");
            return false;
        }
    }

    public void LoseHealth(int amount) 
    {
        if (amount >= health)
        {
            health = 0;
            GameOverScreen.Setup(round);
        } else
        {
            health -= amount;
        }
    }

    public void IncreaseRound()
    {
        round++;
    }

    public void IncreaseCurrencyAfterRound()
    {
        currency += (100 + round);
    }
}