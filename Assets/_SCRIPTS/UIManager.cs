using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    FlockManager fManager;
    Slider slider;

    private void Start() 
    {
        fManager = gameObject.GetComponentInParent<FlockManager>();
        slider = GetComponentInChildren<Slider>();
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetNewValue(int _valueID)
    {
        //TODO: SWITCH STATEMENT
        if(_valueID == 0)
        {
            fManager.SetRange(slider.value);
        }
        else if(_valueID == 1)
        {

        }
    }
}
