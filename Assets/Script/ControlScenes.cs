using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class ControlScenes : MonoBehaviour
{
    public void Jugar(string nombreEscena)
    {
        StartCoroutine(CambiarEscenaConSonido(nombreEscena));
    }

    IEnumerator CambiarEscenaConSonido(string nombreEscena)
    {
        SoundManager.Instance.PlaySound("ClickButton", 1f);

        yield return new WaitForSeconds(0.2f); 

        SceneManager.LoadScene(nombreEscena);
    }

    public void Salir()
    {
        SoundManager.Instance.PlaySound("ClickButton", 1f);
        Application.Quit();
    }
}
