using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronoBombe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMesh>().text = ""+((int)GameManager.instance.timeBeforeEnd);
    }
}
