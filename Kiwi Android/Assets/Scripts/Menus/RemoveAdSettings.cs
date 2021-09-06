using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemoveAdSettings : MonoBehaviour
{
    public TextMeshProUGUI tmp;

    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerPrefs.GetInt("DisabledAds") == 0)
            tmp.text = "Removed Ads: Off";
        else
            tmp.text = "Removed Ads: On";
    }
}
