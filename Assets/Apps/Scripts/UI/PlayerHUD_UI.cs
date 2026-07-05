using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD_UI : MonoBehaviour
{
    [SerializeField] private Text playerName;
    [SerializeField] private Slider playerHealthBar;

    
    public void HealthBarUpdate(float value)
    {
        playerHealthBar.value = value;
    }
}
