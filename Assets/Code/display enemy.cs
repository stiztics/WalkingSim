using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayenemy : MonoBehaviour
{
    public static int enemyValue = 0;
    Text kill;
    // Start is called before the first frame update
    void Start()
    {
        kill = GetComponent<Text> ();
    }

    // Update is called once per frame
    void Update()
    {
        kill.text = "Kill " + enemyValue + " Monsters" ;
    }
}
