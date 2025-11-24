using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameManager : MonoBehaviour
{
    [Header("Timer Settings")]
    public float tiempoInicial = 90f; 
    private float tiempoRestante;

    [Header("UI References")]
    public TMP_Text timerText;
    public TMP_Text killText;

    [Header("Game Settings")]
    public int killsParaGanar = 10; 
    private int killsActuales = 0;

    public Player player; 

    private bool juegoTerminado = false;

    [System.Obsolete]
    private void Start()
    {
        tiempoRestante = tiempoInicial;

        if (player == null)
            player = Object.FindFirstObjectByType<Player>();

        ActualizarUI();
    }

    private void Update()
    {
        if (juegoTerminado) return;

        // Timer
        tiempoRestante -= Time.deltaTime;
        if (tiempoRestante <= 0)
        {
            tiempoRestante = 0;
        
            TerminarJuego(killsActuales >= killsParaGanar);
        }

        
        else if (player != null && player.Health <= 0)
        {
            TerminarJuego(false);
        }

        ActualizarUI();
    }

  
    public void AñadirKill()
    {
        killsActuales++; 
    }

    private void ActualizarUI()
    {
        // Timer
        if (timerText != null)
        {
            int minutos = Mathf.FloorToInt(tiempoRestante / 60);
            int segundos = Mathf.FloorToInt(tiempoRestante % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        }

        // Kills
        if (killText != null)
        {
            killText.text = "Kills: " + killsActuales;
        }
    }

    private void TerminarJuego(bool victoria)
    {
        juegoTerminado = true;

        if (victoria)
            SceneManager.LoadScene("ganaste");
        else
            SceneManager.LoadScene("perdiste");
    }
}
