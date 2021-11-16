using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Click : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void OptionClick()
    {

    }

    public void quitScene()
    {
        Application.Quit();
    }
}
