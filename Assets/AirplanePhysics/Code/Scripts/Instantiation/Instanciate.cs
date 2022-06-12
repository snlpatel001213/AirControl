using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instanciate : MonoBehaviour
{
    public GameObject myPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(myPrefab, new Vector3(0, 100, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
