using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class CarLights : MonoBehaviour
{
  public enum Side
    {
        Front,
        Back
    }
    [System.Serializable]  
    public  struct Light
    {
        public GameObject lightObj;
        public Material lightMat;
        public Side side;
    }

    public Toggle lightToggle;

    public bool isFrontLightOn;
    public bool isBackLightOn;

    public List<Light> lights;

    void Start()
    {
        isFrontLightOn = lightToggle.isOn;
        isBackLightOn = false;
    }

    public void OperateFrontLights()
    {
        isFrontLightOn = !isFrontLightOn;

        if(isFrontLightOn)
        {
            //Turn on lights;
            foreach (var light in lights)
            {
                if (light.side == Side.Front && light.lightObj.activeInHierarchy == false)
                {
                    light.lightObj.SetActive(true);
                }
            }
            lightToggle.gameObject.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            //Turn off lights;
            foreach (var light in lights)
            {
                if(light. side == Side.Front && light.lightObj.activeInHierarchy == true)
                {
                    light.lightObj.SetActive(false);
                }
            }
            lightToggle.gameObject.GetComponent<Image>().color = Color.white;
        }
    }

    public void OperateBackLights()
    {
        if (isBackLightOn)
        {

            //Turn on lights;
            foreach (var light in lights)
            {
                if (light.side == Side.Back && light.lightObj.activeInHierarchy == false)
                {
                    light.lightObj.SetActive(true);
                }
            }
            lightToggle.gameObject.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            //Turn off lights;
            foreach (var light in lights)
            {
                if (light.side == Side.Back && light.lightObj.activeInHierarchy == true)
                {
                    light.lightObj.SetActive(false);
                }
            }
            lightToggle.gameObject.GetComponent<Image>().color = Color.white;
        }
    }
}
