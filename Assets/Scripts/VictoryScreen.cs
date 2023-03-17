using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
    public string mainMenuScene;
    public float timBetweenShowing;
    public GameObject textBox;
    public Image blackScreen;
    public float blackScreenFade = 2f;

    private void Start()
    {
        StartCoroutine("ShowObjectCoroutine");
    }

    private void MainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public IEnumerable ShowObjectsCoroutine()
    {
        yield return new WaitForSeconds(timBetweenShowing);
        textBox.SetActive(true);
    }
}
