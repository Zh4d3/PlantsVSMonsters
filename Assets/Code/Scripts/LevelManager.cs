using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;

    public int currency;
    public int health;
    public int round;

    private void Awake() {
        main = this;
    }

    private void Start() {
        currency = 200;
        health = 100;
        round = 1;
    }

    public void IncreaseCurrency(int amount) {
        currency += amount;
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

    public void LoseHealth(int amount) {
        if (amount >= health)
        {
            health = 0;
            Debug.Log("Game Over");
        } else
        {
            health -= amount;
        }
    }

    public void IncreaseRound()
    {
        round++;
    }

}
