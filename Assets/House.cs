using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class House : MonoBehaviour {

    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 3.2f;
    [SerializeField] private float bps = 1.05f;
    [SerializeField] private float bpsMultiplier = 1;
    [SerializeField] private int damage = 1;
    [SerializeField] private int baseUpgradeCost = 170;
    [SerializeField] private string towerType = "flying";


    private float bpsBase;
    private float targetingRangeBase;
    private int damageDone;

    private Transform target;
    private float timeUntilFire;

    private int level = 1;
    private bool isSpeedUp = false;

    private void Start() {
        bpsMultiplier = 1;
        isSpeedUp = false;
        bpsBase = bps;
        targetingRangeBase = targetingRange;
        damageDone = 0;

        upgradeButton.onClick.AddListener(Upgrade);
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

            if (timeUntilFire >= 1f / (bps * bpsMultiplier)) {
                Shoot();
                timeUntilFire = 0f;
            }

        }
    }

    private void Shoot() {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target, damage);
        damageDone += damage;
    }

    private void FindTarget() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0) {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange() {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    public void OpenUpgradeUI() {
        upgradeUI.SetActive(true);
    }

    public void CloseUpgradeUI() {
        upgradeUI.SetActive(false);
        UIManager.main.SetHoveringState(false);
    }

    public void Upgrade() {
        if (CalculateCost() > LevelManager.main.currency) return;

        LevelManager.main.SpendCurrency(CalculateCost());

        level++;

        bps = CalculateBPS();
        targetingRange = CalculateRange();

        CloseUpgradeUI();
        Debug.Log("New BPS: " + bps);
        Debug.Log("New BPS: " + targetingRange);
        Debug.Log("New Cost: " + CalculateCost());
    }

    private int CalculateCost() {
        return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(level, 0.8f));
    }

    private float CalculateBPS() {
        return bpsBase * Mathf.Pow(level, 0.6f);
    }

    private float CalculateRange() {
        return targetingRangeBase * Mathf.Pow(level, 0.4f);
    }


    public void ToggleSpeedUp() {
        isSpeedUp = !isSpeedUp;
        SpeedUp();
    }

    public void SpeedUp() {
        if (isSpeedUp) {
            Debug.Log("Sped up");
            bpsMultiplier = 2;
        } else {
            Debug.Log("Sped down");
            bpsMultiplier = 1;
        }
    }
}
// Lucas: Doesnt speed down correctly 