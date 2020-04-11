using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube : MonoBehaviour
{
    [SerializeField]
    private Material black;
    [SerializeField]
    private Material light;
    // Start is called before the first frame update
    void Start()
    {
        gameMan.changeMaterial += changeToLight;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void changeToLight()
    {
        GetComponent<MeshRenderer>().material = black;
        transform.GetChild(0).gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        gameMan.changeMaterial -= changeToLight;
    }

}
