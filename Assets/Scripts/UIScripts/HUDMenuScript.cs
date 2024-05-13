using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDMenuScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CoinsTxt;
    [SerializeField] TextMeshProUGUI HealthTxt;
    [SerializeField] BaseScripts baseScript;
    private void Start() 
    {
        if(GameManager.Instance.SceneIsALevelScene())
        {
            baseScript = GameObject.FindGameObjectWithTag("Base").GetComponent<BaseScripts>();
            CoinsTxt = gameObject.transform.Find("HUDUIImage/CoinsTxt").GetComponent<TextMeshProUGUI>();
            HealthTxt = gameObject.transform.Find("HUDUIImage/HealthTxt").GetComponent<TextMeshProUGUI>();
        }
    }
    private void Update() 
    {
        if(GameManager.Instance.SceneIsALevelScene())
        {
            baseScript = GameObject.FindGameObjectWithTag("Base").GetComponent<BaseScripts>();
            CoinsTxt.text = "Coins: " + baseScript.GetCoins().ToString();
            HealthTxt.text = "Health: " + baseScript.GetHealth().ToString();
        }
    }
    /*
    reference the txt for coins/health
    reference the base with the Get-coins/health
    Update will keep checking and updating
    */
}
