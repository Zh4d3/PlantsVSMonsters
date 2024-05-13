using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text roundsText;

    public void Setup(int score)
    {
        gameObject.SetActive(true);
        roundsText.text = score.ToString();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void ExitButton() {
        SceneManager.LoadScene("Main Menu");
    }
}