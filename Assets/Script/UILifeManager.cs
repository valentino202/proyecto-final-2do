using UnityEngine;
using UnityEngine.UI;

public class UILifeManager : MonoBehaviour
{
    public Image[] lifeIcons; 
    public Player player;

    private void Update()
    {
        int vidaActual = player.Health; 
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].enabled = i < vidaActual;
        }
    }
}
