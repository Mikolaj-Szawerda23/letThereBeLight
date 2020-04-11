using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class gameMan : MonoBehaviour
{
    public static Action changeMaterial;
    [SerializeField]
    private LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount>0)
        {
            Shoot(Input.GetTouch(0).position);
        }
    }
    void Shoot(Vector3 touch)
    {
        Ray ray = Camera.main.ScreenPointToRay(touch);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,mask))
        {
            changeMaterial?.Invoke();
        }
    }
}
