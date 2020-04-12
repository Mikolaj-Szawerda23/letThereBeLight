using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
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
    [SerializeField]
    private Text count;
    [SerializeField]
    private Text time;
    [SerializeField]
    private Text score;
    public float speed = 60f;
    float speed2 = 10f;
    Vector2 maxBorders=new Vector2(65f,65f);
    Vector2 minBorders=new Vector2(-65f,-65f);
    private int numberOfCubes=10;
    private int clickedCubes = 0;
    private float timeVal=2f;
    private float scoreVal = 0;

    // Start is called before the first frame update
    void Start()
    {
        numberOfCubes = PlayerPrefs.GetInt("numberofcubes");
        for(int i=0;i<numberOfCubes;i++)
        {
            Instantiate(lamp, new Vector3(r.Next((int)minBorders.x, (int)maxBorders.x), 0.55f, r.Next((int)minBorders.y, (int)maxBorders.y)),Quaternion.identity);
        }
    }
    bool isHit=true;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (timeVal <= 0)
        { PlayerPrefs.SetInt("numberofcubes", numberOfCubes += 5); SceneManager.LoadScene(SceneManager.GetActiveScene().name);  }
        if(Input.touchCount>0)
        {
            isHit=Shoot(Input.GetTouch(0).position);
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved&&isHit)
        {

            moveCamera();
        }
        updateText();
    }

    void updateText()
    {
        count.text = clickedCubes + "/" + numberOfCubes;
        timeVal -=Time.deltaTime;
        time.text = timeVal.ToString("00.00");
        score.text = "Score: " + scoreVal;
    }
    void moveCamera()
    {
        Vector2 TouchDeltaPosition = Input.GetTouch(0).deltaPosition;
        Vector3 smoothed = Vector3.Lerp(camera.transform.position, new Vector3(camera.transform.position.x - TouchDeltaPosition.x, camera.transform.position.y, camera.transform.position.z - TouchDeltaPosition.y),speed*Time.deltaTime);
        //camera.transform.Translate(new Vector3(-TouchDeltaPosition.x,0f, -TouchDeltaPosition.y) *speed,Space.World);
        camera.transform.position = smoothed;
    }
    bool Shoot(Vector3 touch)
    {
        Ray ray = Camera.main.ScreenPointToRay(touch);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,Mathf.Infinity,mask))
        {if(!hit.collider.gameObject.transform.GetChild(0).gameObject.activeSelf)
            {
                changeToLight(hit.collider.gameObject);
                clickedCubes++;
                scoreVal += 100;
            }
            
            
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
