using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl4_Wave : MonoBehaviour
{
    public Vector2 originalPos;
    public Vector2 risePos;
    public float waveSpeed = 5f;
    public float waveHeight = 3f;

    public static int wavePhase = 0;
    public static float wavePhaseTime = 0;
    /*
     * 1 - Water begins rising to the screen
     * 2 - Water is moving normally like a wave
     * 3 - Water goes down for the new level
     */

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        risePos = new Vector2(originalPos.x, originalPos.y + 7.5f);
        wavePhaseTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (AI_Dir_Generic.currentLevel == 4)
        {
            wavePhaseTime += Time.deltaTime;
            if (wavePhaseTime < 5)
                wavePhase = 1;
            else
                wavePhase = 2;

            if (Level_Transition.gameTime + 3.5f > Level_Transition.staticLevelSwitchTime)
                wavePhase = 3;
        }
        else 
            wavePhase = 3;

        switch (wavePhase)
        {
            case (1):
                //Water rising at the start of the level
                transform.position = Vector2.MoveTowards(transform.position, 
                    new Vector2(risePos.x, risePos.y + (Mathf.Sin(waveSpeed * wavePhaseTime) * waveHeight)), 0.035f);
                break;
            case (2):
                //Sin Wave movement
                transform.position = new Vector2(risePos.x, risePos.y + (Mathf.Sin(waveSpeed * wavePhaseTime) * waveHeight));
                break;
            case (3):
                //Water lowering for the next level
                transform.position = Vector2.Lerp(transform.position, originalPos, 0.01f);
                //if (Vector2.Distance(originalPos, transform.position) <= 0.05f) gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}
