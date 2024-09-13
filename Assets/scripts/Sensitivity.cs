using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sensitivity : MonoBehaviour
{
    public Slider sensSlider;          // sensitivity slider in Settings panel
    public TextMeshProUGUI sensNumber; // sensitivity ranking (default: 1 to 3)
    public int sensThresholdX;         // how much the x-axis sensitivity will change with slider
    public int sensThresholdY;         // how much the y-axis sensitivity will change with slider  

    // Start is called before the first frame update
    void Start()
    {
        // set the default camera speed to level corresponding to sensNumber = 1
        Cinemachine.CinemachineFreeLook mCameraCM = Camera.main.GetComponent<Cinemachine.CinemachineFreeLook>();
        sensSlider.value = 2;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSensitivity();
    }

    // gets the slider value from Settings and modifies the Cinemachine camera speed
    public void ChangeSensitivity()
    {
        // display the number on settings panel
        sensNumber.text = sensSlider.value.ToString();
        // set the Cinemachine speed by multiplying the slider value with the threshold
        Cinemachine.CinemachineFreeLook mCameraCM = Camera.main.GetComponent<Cinemachine.CinemachineFreeLook>();
        mCameraCM.m_XAxis.m_MaxSpeed = sensSlider.value * sensThresholdX;
        mCameraCM.m_YAxis.m_MaxSpeed = sensSlider.value * sensThresholdY;
        Debug.Log("The max x-axis speed is: " + mCameraCM.m_XAxis.m_MaxSpeed);
        Debug.Log("The max y-axis speed is: " + mCameraCM.m_YAxis.m_MaxSpeed);
    }
}
