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
        MainMenu();
        StartCoroutine("ShowObjectCoroutine");
    }

    private void Update()
    {
        blackScreen.color = new Color (blackScreen.color.r, blackScreen.color.g, blackScreen.color.b , Mathf.MoveTowards(blackScreen.color.a, 0f, blackScreenFade * Time.deltaTime));
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public IEnumerable ShowObjectCoroutine()
    {
        yield return new WaitForSeconds(timBetweenShowing);
        textBox.SetActive(true);
    }
}
