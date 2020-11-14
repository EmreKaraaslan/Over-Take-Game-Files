using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player_Car_Controller : MonoBehaviour
{
    public KeyCode Touch;

    float speed_of_player_car  = 20f;
    float speedside = 20f;
    float speed_other_cars = 20f;
    float point;

    new Vector3 HitObject1_Pos, HitObject2_Pos, DeltaPosition;

    GameObject[] Cars_Same_Side;
    Rigidbody[] Rbs_Same_Side;
    Rigidbody[] Rbs_Opposite_Side;
   
    
    
    public TMPro.TextMeshProUGUI PointText;
    public TMPro.TextMeshProUGUI LostText;
    public TMPro.TextMeshProUGUI RestartText;
    public TMPro.TextMeshProUGUI LevelEndText;
    
  

   
    public void Start()
    {

        point = 0;
        LostText.text = "";
        RestartText.text = "";
        LevelEndText.text="";


        Cars_Same_Side = GameObject.FindGameObjectsWithTag("Other Cars Same Side");
        Rbs_Same_Side = new Rigidbody[Cars_Same_Side.Length];

        for (int i = 0; i < Cars_Same_Side.Length; ++i)
        {
            GameObject Car_Same_Side = Cars_Same_Side[i];
            Rbs_Same_Side[i] = Car_Same_Side.GetComponent<Rigidbody>();
        }


        GameObject[] Cars_Opposite_Side = GameObject.FindGameObjectsWithTag("Other Cars Opposite Side");
        Rbs_Opposite_Side = new Rigidbody[Cars_Opposite_Side.Length];

        for (int i = 0; i < Cars_Opposite_Side.Length; ++i)
        {
            GameObject Car_Opposite_Side = Cars_Opposite_Side[i];
            Rbs_Opposite_Side[i] = Car_Opposite_Side.GetComponent<Rigidbody>();
        }


    }


   
    void Update()
    {
            
        SpeedofPlayerCar();
        SpeedofOtherCars();
        TakeOver();
        SetPointText();
      
    }



    public void TakeOver()
    {
        bool held = Input.GetKey(Touch);

        if (held && transform.position.x > -2f)
        {

            transform.Translate(0, 0, speedside * Time.deltaTime);

        }

        if (!held && transform.position.x < 2.25f)
        {

            transform.Translate(0, 0, -speedside * Time.deltaTime);

        }


    }


    public void SpeedofOtherCars()
    {
        foreach (Rigidbody Rb1 in Rbs_Same_Side)
        {
            Rb1.transform.Translate(speed_other_cars * Time.deltaTime, 0, 0);

        }

        foreach (Rigidbody Rb2 in Rbs_Opposite_Side)
        {
            Rb2.transform.Translate(speed_other_cars * Time.deltaTime, 0, 0);

        }

    }

    public void SpeedofPlayerCar()
    {
        bool held = Input.GetKey(Touch);

        if (held && transform.position.x < 2.4f)
        {
            transform.Translate(2 * speed_of_player_car * Time.deltaTime, 0, 0);
        }

        else
        {
            transform.Translate(speed_of_player_car * Time.deltaTime, 0, 0);
        }


    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Other Cars Same Side"))
        {


            transform.position = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z + 7);


        }

        if (other.gameObject.CompareTag("Side Collider"))
        {


            point += 10;


        }

        if (other.gameObject.CompareTag("Other Cars Opposite Side"))
        {

            HitObject1_Pos = other.gameObject.transform.position;
            HitObject2_Pos = transform.position;
            DeltaPosition = HitObject2_Pos - HitObject1_Pos;
            float x = DeltaPosition.x;
            float z = DeltaPosition.z;
            transform.position += new Vector3(x, 0, z);
            other.gameObject.transform.position -= new Vector3(x, 0, z);
            GameEnd();
            LoseText();
            InvokeRepeating("Restart", 1f,0.01f);


        }

        if (other.gameObject.CompareTag("Finish Table"))
        {


            GameEnd();
            LevelFinish();
            InvokeRepeating("Restart", 1f, 0.01f);


        }

    }


    public void GameEnd()
    {
        speed_of_player_car = 0;
        speedside = 0;
        speed_other_cars = 0;


    }


    public void Point()
    {

        if (transform.position.z < Cars_Same_Side[0].transform.position.z)
        {
            point += 10;
        }

    }

    public void Restart()
    {
        RestartText.text = "Tap Screen to Restart!";
        if (Input.GetKey(Touch))
        {
            SceneManager.LoadScene("SampleScene");
        }

    }

    public void LevelFinish()
    {
        LevelEndText.text = "Level is Completed!";
    }

    void LoseText()
    {
        LostText.text = "You Lost!";
    }

    void SetPointText()
    {
        PointText.text = "Score: " + point.ToString();
    }

 

}
