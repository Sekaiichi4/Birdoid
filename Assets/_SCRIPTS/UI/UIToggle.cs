using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToggle : MonoBehaviour
{
    public GameObject flockUI, skyUI, camUI;
    Toggle toggle;

    private void Start() 
    {
        toggle = GetComponent<Toggle>();
    }

    public void ToggleUIControls()
    {
        flockUI.SetActive(toggle.isOn);
        skyUI.SetActive(toggle.isOn);
        camUI.SetActive(toggle.isOn);
    }
}
