using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void disabledAds()
    {
        PlayerPrefs.SetInt("DisabledAds", 1);
    }
}
