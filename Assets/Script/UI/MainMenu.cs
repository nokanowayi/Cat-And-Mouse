using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject QuitMenu;
    public GameObject mainmenu;
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

   

    public void Quit()
    {
        QuitMenu.SetActive(true);
        mainmenu.SetActive(false);
    }


    public void NoQuit()
    {
        QuitMenu.SetActive(false);
        mainmenu.SetActive(true);
    }

    public void YesQuit()
    {
        Application.Quit();
    }
}
