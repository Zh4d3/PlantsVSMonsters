using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradesMenu : MonoBehaviour
{
    public static UpgradesMenu main;

    [Header("References")]
    [SerializeField] Animator anim;

    private string popCount;

    private bool isUpgradeOpen = false;

    private void Awake()
    {
        main = this;
        isUpgradeOpen = false;
    }

    public void ToggleUpgrade()
    {
        isUpgradeOpen = !isUpgradeOpen;
        anim.SetBool("UpgradeOpen", isUpgradeOpen);
    }
}