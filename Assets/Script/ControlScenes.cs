using UnityEngine;
using UnityEngine.SceneManagement;
public class ControlScenes : MonoBehaviour
{

    public void Jugar(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }

   
    public void Salir()
    {
        Application.Quit();
    }

}
