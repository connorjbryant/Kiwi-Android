using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giraffe : MonoBehaviour
{
    public GameObject giraffeNeck;
    public GiraffeNeck neckScript;
    private GameObject neckObj;
    public GameObject neckPoint;

    public float neckRotationSpeed = 1f;
    public bool hasLowNeck = false;
    public Vector3 highNeckRotation = new Vector3(0, 0, -15f);
    public Vector3 lowNeckRotation = new Vector3(0, 0, 50f);

    // Start is called before the first frame update
    void Start()
    {
        hasLowNeck = false;
        neckPoint = transform.Find("NeckPoint").gameObject;
        neckObj = Instantiate(giraffeNeck, neckPoint.transform.position, Quaternion.identity);
        neckScript = neckObj.transform.Find("Sprite").GetComponent<GiraffeNeck>();
    }

    // Update is called once per frame
    void Update()
    {
        if (neckScript.isHitByKiwi)
        {
            hasLowNeck = true;
            neckScript.isHitByKiwi = false;
        }

        if (hasLowNeck)
        {
            neckObj.transform.localRotation = Quaternion.Lerp(neckObj.transform.localRotation,
                Quaternion.Euler(lowNeckRotation), Time.deltaTime * neckRotationSpeed);
        }
        else
        {
            neckObj.transform.localRotation = Quaternion.Lerp(neckObj.transform.localRotation,
                Quaternion.Euler(highNeckRotation), Time.deltaTime * neckRotationSpeed);
        }

        neckObj.transform.position = neckPoint.transform.position;
    }

    private void FixedUpdate()
    {
        
    }

    //Add this on Giraffe's neck script as well
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "KiwiWeapon")
        {
            hasLowNeck = true;
        }
    }
}
