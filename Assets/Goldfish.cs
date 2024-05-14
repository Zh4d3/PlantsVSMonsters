using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Events;

public class Goldfish : MonoBehaviour {

    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    /*[SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeDamageButton;
    [SerializeField] private Button upgradeSpeedButton;
    [SerializeField] private Button upgradeRangeButton;
    [SerializeField] private Button sellTower;
    [SerializeField] TextMeshProUGUI damageUpgradePrice;
    [SerializeField] TextMeshProUGUI damageUpgradeDescription;
    [SerializeField] TextMeshProUGUI speedUpgradePrice;
    [SerializeField] TextMeshProUGUI speedUpgradeDescription;
    [SerializeField] TextMeshProUGUI rangeUpgradePrice;
    [SerializeField] TextMeshProUGUI rangeUpgradeDescription;
    [SerializeField] TextMeshProUGUI selectedTowersPopCount;*/

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 4f;
    [SerializeField] private float bps = 1.43f;
    [SerializeField] private int damage = 1;
    [SerializeField] private int towerBasePrice = 200;
    [SerializeField] private string towerType = "water";


    /*private int damageDone;
    private int towerPrice;
    private int attackLvl;
    private int speedLvl;
    private int rangeLvl;*/

    private Transform target;
    private float timeUntilFire;
    //private bool isSpeedUp = false;

    private void Start() {
        /*damageDone = 0;
        towerPrice = towerBasePrice;
        attackLvl = 0;
        speedLvl = 0;
        rangeLvl = 0;
        damageUpgradePrice.text = "Sharp Shots (" + 120.ToString() + ")";
        damageUpgradeDescription.text = "Damages 1 extra per shot.";
        speedUpgradePrice.text = "Quick Shots (" + 85.ToString() + ")";
        speedUpgradeDescription.text = "Shoots 15% faster.";
        rangeUpgradePrice.text = "Long Range Shots (" + 75.ToString() + ")";
        rangeUpgradeDescription.text = "Gains +8 range.";*/

        //upgradeButton.onClick.AddListener(Upgrade);
    }

    private void Update() {
        if (target == null) {
            FindTarget();
            return;
        }

        if (!CheckTargetIsInRange()) {
            target = null;
        } else {

            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / bps) {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot() {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target, damage);
        //damageDone += damage;
        //SetPopCount(damageDone);
    }

    private void FindTarget() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0) 
        {
            if (towerType == "ground") 
            {
                if (hits[0].collider.transform.GetComponent<EnemyMovement>().enemyType == "flying") return;
            }
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange() {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    /*public void OpenUpgradeUI() {
        upgradeUI.SetActive(true);
        //upgradeUI.UpgradeOpen = true;
    }

    public void CloseUpgradeUI() {
        // Play closing animation
        upgradeUI.SetActive(false);
        //upgradeUI.UpgradeOpen  = false;
    }

    public void UpgradeDamage() {
        if (attackLvl == 0) { // Sharp Shots
            int UpgradePrice = 120;
            if (UpgradePrice > LevelManager.main.currency) return;
            LevelManager.main.SpendCurrency(UpgradePrice);
            damageUpgradePrice.text = "Razor Sharp Shots (" + 185.ToString() + ")";
            damageUpgradeDescription.text = "Damages 2 extra per shot.";
            damage += 1;
            attackLvl++;
            towerPrice += UpgradePrice;
        } else if (attackLvl == 1) { // Razor Sarp Shots
            int UpgradePrice = 185;
            if (UpgradePrice > LevelManager.main.currency) return;
            LevelManager.main.SpendCurrency(UpgradePrice);
            damageUpgradePrice.text = "Super Razor Sharp Shots (" + 255.ToString() + ")";
            damageUpgradeDescription.text = "Damages 2 extra per shot & Range is increased by 15%.";
            damage += 2;
            attackLvl++;
            towerPrice += UpgradePrice;
        } else if (attackLvl == 2) { // Spike-O-Pult
            int UpgradePrice = 255;
            if (UpgradePrice > LevelManager.main.currency) return;
            LevelManager.main.SpendCurrency(UpgradePrice);
            damageUpgradePrice.text = "Extremely Super Razor Sharp Shots (" + 1530.ToString() + ")";
            damageUpgradeDescription.text = "Damages 2 extra per shot & Speed is incresead by 5%.";
            damage += 2;
            targetingRange *= 1.15f;
            attackLvl++;
            towerPrice += UpgradePrice;
        } else if (attackLvl == 3) { // Juggernaut
            int UpgradePrice = 1530;
            if (UpgradePrice > LevelManager.main.currency) return;
            LevelManager.main.SpendCurrency(UpgradePrice);
            damageUpgradePrice.text = "Too Extremely Super Razor Sharp Shots (" + 12750.ToString() + ")";
            damageUpgradeDescription.text = "Damages 7 extra per shot.";
            damage += 2;
            bps *= 1.05f;
            attackLvl++;
            towerPrice += UpgradePrice;
        } else if (attackLvl == 4) { // Ultra-Juggernaut
            int UpgradePrice = 12750;
            if (UpgradePrice > LevelManager.main.currency) return;
            LevelManager.main.SpendCurrency(UpgradePrice);
            damageUpgradePrice.text = "(MAX LEVEL)";
            damageUpgradeDescription.text = "";
            damage += 7;
            attackLvl++;
            towerPrice += UpgradePrice;
        } else {
            Debug.Log("Attack Damage is Max Upgraded!");
        }
    }

    public void UpgradeSpeed() {
        if (speedLvl == 0) { // Quick Shots
            int UpgradePrice = 85;
            if (UpgradePrice > LevelManager.main.currency) return;
            LevelManager.main.SpendCurrency(UpgradePrice);
            speedUpgradePrice.text = "Very Quick Shots (" + 160.ToString() + ")";
            speedUpgradeDescription.text = "Shoots 17.6% faster.";
            bps *= 1.15f;
            speedLvl++;
            towerPrice += UpgradePrice;
        } else if (speedLvl == 1) { // Very Quick Shots
            int UpgradePrice = 160;
            if (UpgradePrice > LevelManager.main.currency) return;
            LevelManager.main.SpendCurrency(UpgradePrice);
            speedUpgradePrice.text = "Super Very Quick Shots (" + 340.ToString() + ")";
            speedUpgradeDescription.text = "Shoots 200% faster.";
            bps *= 1.176f;
            speedLvl++;
            towerPrice += UpgradePrice;
        } else if (speedLvl == 2) { // Triple Shot
            int UpgradePrice = 340;
            if (UpgradePrice > LevelManager.main.currency) return;
            LevelManager.main.SpendCurrency(UpgradePrice);
            speedUpgradePrice.text = "Extremely Super Very Quick Shots (" + 6800.ToString() + ")";
            speedUpgradeDescription.text = "Shoots 50% faster.";
            bps *= 3f;
            speedLvl++;
            towerPrice += UpgradePrice;
        } else if (speedLvl == 3) { // Super Monkey Fan Club
            int UpgradePrice = 6800;
            if (UpgradePrice > LevelManager.main.currency) return;
            LevelManager.main.SpendCurrency(UpgradePrice);
            speedUpgradePrice.text = "Too Extremely Super Very Quick Shots (" + 38250.ToString() + ")";
            speedUpgradeDescription.text = "Shoots 100% faster & 2 extra damage.";
            bps *= 1.5f;
            speedLvl++;
            towerPrice += UpgradePrice;
        } else if (speedLvl == 4) { // Plasma Monkey Fan Club
            int UpgradePrice = 38250;
            if (UpgradePrice > LevelManager.main.currency) return;
            LevelManager.main.SpendCurrency(UpgradePrice);
            speedUpgradePrice.text = "(MAX LEVEL)";
            speedUpgradeDescription.text = "";
            bps *= 2f;
            damage += 2;
            speedLvl++;
            towerPrice += UpgradePrice;
        } else {
            Debug.Log("Attack Speed is Max Upgraded!");
        }
    }

    public void UpgradeRange() {
        if (rangeLvl == 0) // Long Range Darts
        {
            int UpgradePrice = 75;
            if (UpgradePrice > LevelManager.main.currency) return;
            LevelManager.main.SpendCurrency(UpgradePrice);
            rangeUpgradePrice.text = "Very Long Range (" + 170.ToString() + ")";
            rangeUpgradeDescription.text = "Gains +8 range & Shoots 10% faster.";
            targetingRange += 0.8f;
            rangeLvl++;
            towerPrice += UpgradePrice;
        } else if (rangeLvl == 1) // Enhanced Eyesught
        {
            int UpgradePrice = 170;
            if (UpgradePrice > LevelManager.main.currency) return;
            LevelManager.main.SpendCurrency(UpgradePrice);
            rangeUpgradePrice.text = "Super Very Long Range (" + 530.ToString() + ")";
            rangeUpgradeDescription.text = "Gains +8 range & Damages 3 extra per shot.";
            targetingRange += 0.8f;
            bps *= 1.1f;
            rangeLvl++;
            towerPrice += UpgradePrice;
        } else if (rangeLvl == 2) // Crossbow
        {
            int UpgradePrice = 530;
            if (UpgradePrice > LevelManager.main.currency) return;
            LevelManager.main.SpendCurrency(UpgradePrice);
            rangeUpgradePrice.text = "Extremely Super Very Long Range (" + 1700.ToString() + ")";
            rangeUpgradeDescription.text = "Gains +4 range & Damages 6 extra per shot & Shoots 58% faster.";
            targetingRange += 0.8f;
            damage += 3;
            rangeLvl++;
            towerPrice += UpgradePrice;
        } else if (rangeLvl == 3) // Sharp Shooter
        {
            int UpgradePrice = 1700;
            if (UpgradePrice > LevelManager.main.currency) return;
            LevelManager.main.SpendCurrency(UpgradePrice);
            rangeUpgradePrice.text = "Too Extremely Super Very Long Range (" + 18275.ToString() + ")";
            rangeUpgradeDescription.text = "Gains +20 range & Shoots 275% faster.";
            targetingRange += 0.4f;
            damage += 6;
            bps *= 1.58f;
            rangeLvl++;
            towerPrice += UpgradePrice;
        } else if (rangeLvl == 4) // Crossbow Master
        {
            int UpgradePrice = 18275;
            if (UpgradePrice > LevelManager.main.currency) return;
            LevelManager.main.SpendCurrency(UpgradePrice);
            rangeUpgradePrice.text = "(MAX LEVEL)";
            rangeUpgradeDescription.text = "";
            targetingRange += 0.4f;
            bps *= 3.75f;
            rangeLvl++;
            towerPrice += UpgradePrice;
        } else {
            Debug.Log("Attack Range is Max Upgraded!");
        }
    }

    public void SellTower() {
        int sellPrice = (int)(towerPrice * 0.7);

        LevelManager.main.IncreaseCurrency(sellPrice);

        UpgradeUIHandler upgradeUIHandler = GetComponentInChildren<UpgradeUIHandler>();
        upgradeUIHandler.OnTowerSell();

        Destroy(gameObject);
    }

    */private void OnDrawGizmosSelected() {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }

    /*public void ToggleSpeedUp() {
        isSpeedUp = !isSpeedUp;
        if (isSpeedUp) {
            bps /= 2; // Shoot twice as fast
        } else {
            bps = bpsBase; // Return to normal speed
        }
    }*/

    /*public void SetPopCount(int damageDone) {
        selectedTowersPopCount.text = damageDone.ToString();
    }*/
}
