using TMPro;
using UnityEngine;

public class HUDMenuScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CoinsTxt;
    [SerializeField] TextMeshProUGUI HealthTxt;
    BaseScripts baseScript;
    private void Awake() 
    {
        baseScript = GameObject.FindGameObjectWithTag("Base").GetComponent<BaseScripts>();
    }
    private void Update() 
    {
        CoinsTxt.text = "Coins: " + baseScript.GetCoins().ToString();
        HealthTxt.text = "Health: " + baseScript.GetHealth().ToString();
    }
    /*
    reference the txt for coins/health
    reference the base with the Get-coins/health
    Update will keep checking and updating
    */
}
