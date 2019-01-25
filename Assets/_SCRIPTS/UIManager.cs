using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    FlockManager fManager;
    Slider[] sliders;

    private void Start() 
    {
        fManager = gameObject.GetComponentInParent<FlockManager>();
        sliders = GetComponentsInChildren<Slider>();
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetNewValue(int _valueID)
    {
        fManager.SetRangeFor(_valueID, sliders[_valueID].value);
    }
}
