using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer2 : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 localScale;
    //Player2Move obj =gameObject.AddComponent< Player2Move>();

    void Start()
    {
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("hi");
        localScale.x =Player2Move.current;
        transform.localScale = localScale;
    }
}
