using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantherEnemy : BaseEnemies
{
    public GameObject pantherArm;
    private GameObject pantherArmObj;
    public Transform pantherArmLocation;
    
    public GameObject tree;
    private GameObject treeObj;
    public Transform treeLocation;

    public float swipeSpeed = 1f;
    public float swipeArcAngle = 45f;

    // Start is called before the first frame update
    void Start()
    {
        pantherArmObj = Instantiate(pantherArm, pantherArmLocation.transform.position, Quaternion.identity);
        treeObj = Instantiate(tree, treeLocation.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(pantherArmObj);
            DestroyItself();
        }
        else
        {
            pantherArmObj.transform.Rotate(new Vector3(0, 0, swipeArcAngle * Mathf.Sin(swipeSpeed * Time.deltaTime)));
        }
    }
}
