using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class DartMonkey : MonoBehaviour {

    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 3.2f;
    [SerializeField] private float bps = 1.05f;
    [SerializeField] private int damage = 1;
    [SerializeField] private int baseUpgradeCost = 170;
    [SerializeField] private string towerType = "ground";


    private float bpsBase;
    private float targetingRangeBase;

    private Transform target;
    private float timeUntilFire;

    private int level = 1;

    private void Start() {
        bpsBase = bps;
        targetingRangeBase = targetingRange;

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

    private void OnDrawGizmosSelected() {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }

}
