using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour {

    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    public GameObject towerObj;
    public Dog dog;
    /*public Penguin penguin;
    public BlueBird blueBird;
    public Goldfish goldfish;
    public Unicorn unicorn;*/
    private Color startColor;
    private UpgradesMenu upgradesMenu;

    private void Start() {
        startColor = sr.color;
        upgradesMenu = UpgradesMenu.main;
    }

    private void OnMouseEnter() {
        sr.color = hoverColor;
    }

    private void OnMouseExit() {
        sr.color = startColor;
    }

    private void OnMouseDown() 
    {
        if (UIManager.main.IsHoveringUI()) return;

        if (towerObj != null) 
        {
            dog.OpenUpgradeUI();
        } else
        {
            Tower towerToBuild = BuildManager.main.GetSelectedTower();


            if (towerToBuild.towerType == "water") {
                Debug.Log("Tower not suitable for terrain");
                return;
            }

            if (towerToBuild.cost > LevelManager.main.currency) {
                Debug.Log("You can't afford this tower");
                return;
            }

            LevelManager.main.SpendCurrency(towerToBuild.cost);

            towerObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
            dog = towerObj.GetComponent<Dog>();
        }
    }
}
