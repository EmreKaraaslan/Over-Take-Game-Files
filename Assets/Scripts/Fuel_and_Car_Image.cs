using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Fuel_and_Car_Image : MonoBehaviour
{
    public Image Fuel_bar;
    public Image Car_position_bar;
    public GameObject Car_Image;
  

    float maximum_fuel;
    public float avaible_fuel;
    

    public float speed_car_figure;

    public TMPro.TextMeshProUGUI Fuel_Percentage_Text;
    bool crash;

    public void Start()
    {
        crash = false;
        speed_car_figure = 3.7f;
        avaible_fuel = 1500.0f;
        maximum_fuel = 1500.0f;
    }

    public void Update()
    {
      
        Car_Figure_Position_Change();
    }

    public void FixedUpdate()
    {
        Fuel_Change();
    }

    public void Fuel_Change()
    {
        
        if(crash==false)
        {
            avaible_fuel -= 0.5f;

            Fuel_bar.fillAmount = avaible_fuel / maximum_fuel;
        }
       
    }


    public void Car_Figure_Position_Change()
    {
        Car_Image.transform.Translate(speed_car_figure * Time.deltaTime, 0, 0);
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Side Collider"))
        {

            avaible_fuel += 10f;

        }


        if (other.gameObject.CompareTag("Other Cars Opposite Side"))
        {
            speed_car_figure = 0f;
            crash = true;
        }

        if (other.gameObject.CompareTag("Finish Table"))
        {


            speed_car_figure = 0f;
            crash = true;


        }
    }

   





}
