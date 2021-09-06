using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowAds1()
    {
        //Remove Ad function
        print("Called remove ad function in ShowAds");
        PlayerPrefs.SetInt("DisabledAds", 0);
    }
}
