using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class gameMan : MonoBehaviour
{
    public static Action changeMaterial;
    System.Random r=new System.Random();
    [SerializeField]
    private LayerMask mask;
    [SerializeField]
    private GameObject lamp;
    [SerializeField]
    private GameObject camera;
    [SerializeField]
    private Material black;
    [SerializeField]
    private Material light;
    float speed = 8f;
    Vector2 maxBorders=new Vector2(65f,65f);
    Vector2 minBorders=new Vector2(-65f,-65f);
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<10;i++)
        {
            Instantiate(lamp, new Vector3(r.Next((int)minBorders.x, (int)maxBorders.x), 0.55f, r.Next((int)minBorders.y, (int)maxBorders.y)),Quaternion.identity);
        }
    }
    bool isHit=true;
    // Update is called once per frame
    void Update()
    {
        
        if(Input.touchCount>0)
        {
            isHit=Shoot(Input.GetTouch(0).position);
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved&&isHit)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            camera.transform.Translate(new Vector3(-touchDeltaPosition.x * speed * Time.deltaTime, 0f, -touchDeltaPosition.y * speed * Time.deltaTime), Space.World);

        }
    }
    bool Shoot(Vector3 touch)
    {
        Ray ray = Camera.main.ScreenPointToRay(touch);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,Mathf.Infinity,mask))
        {
            changeToLight(hit.collider.gameObject);
            return false;
        }
        return true;
    }
    void changeToLight(GameObject obj)
    {
        obj.GetComponent<MeshRenderer>().material = light;
        obj.transform.GetChild(0).gameObject.SetActive(true);
    }
}
