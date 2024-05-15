using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour {

    public void Setup() {
        gameObject.SetActive(true);;
    }

    public void RestartButton() {
        SceneManager.LoadScene("Level 1");
    }

    public void ExitButton() {
        SceneManager.LoadScene("Main Menu");
    }
}