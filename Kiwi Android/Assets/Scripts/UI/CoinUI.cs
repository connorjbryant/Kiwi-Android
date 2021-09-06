using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    private TextMeshProUGUI coinUI;

    // Start is called before the first frame update
    void Start()
    {
        coinUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        coinUI.text = "Coins: " + PlayerPrefs.GetInt("numCoins");
    }
}
