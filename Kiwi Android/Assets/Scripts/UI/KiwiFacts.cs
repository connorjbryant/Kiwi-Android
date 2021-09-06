using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KiwiFacts : MonoBehaviour
{
    private TextMeshProUGUI kiwiFactText;
    public string[] facts;
    public int fact_Index;
    public float cycleRate = 5f;
    private float tempCycleRate;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        tempCycleRate = cycleRate;
        fact_Index = Random.Range(0, facts.Length);
        kiwiFactText = GetComponent<TextMeshProUGUI>();
        cycleRate = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        cycleRate -= Time.deltaTime;
        if (cycleRate <= 0)
        {
            kiwiFactText.text = facts[fact_Index++];
            if (fact_Index >= facts.Length)
            {
                fact_Index = 0;
            }
            cycleRate = tempCycleRate;
        }
    }

    public void nextFact()
    {
        cycleRate = 0f;
        audioSource.Play();
    }
}
