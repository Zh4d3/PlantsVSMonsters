using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] public string enemyType = "ground";
    //[SerializeField] private float moveSpeedMultiplier = 1;

    private Transform target;
    private int pathIndex = 0;
    private int health = 100;
    private bool isSpeedUp;
    private float baseSpeed;

    public void Start() {
        isSpeedUp = false;
        baseSpeed = moveSpeed;
        target = LevelManager.main.path[pathIndex];
    }

    private void Update() {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f) {
            pathIndex++;

            if (pathIndex == LevelManager.main.path.Length) {
                LevelManager.main.LoseHealth(attackDamage);
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            } else {
                target = LevelManager.main.path[pathIndex];
            }

        }
    }

    private void FixedUpdate() {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * moveSpeed /* * moveSpeedMultiplier*/;
    }

    public void UpdateSpeed(float newSpeed) {
        moveSpeed = newSpeed;
    }

    public void ResetSpeed() {
        moveSpeed = baseSpeed;
    }

    /*public void ToggleSpeedUp() {
        isSpeedUp = !isSpeedUp;
        SpeedUp();
    }

    public void SpeedUp() {
        if (isSpeedUp) {
            Debug.Log("Enemies sped up");
            moveSpeedMultiplier = 2;
        } else {
            Debug.Log("Enemies sped down");
            moveSpeedMultiplier = 1;
        }
        UpdateExistingEnemiesSpeed();
    }

    private void UpdateExistingEnemiesSpeed() 
    {
        EnemyMovement[] enemies = FindObjectsOfType<EnemyMovement>();
        foreach (EnemyMovement enemy in enemies) {
            enemy.moveSpeedMultiplier = moveSpeedMultiplier;
        }
    }*/
}
