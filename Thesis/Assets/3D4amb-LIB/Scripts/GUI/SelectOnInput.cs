using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// A simple script to select an object with the EventSystem
/// </summary>
public class SelectOnInput : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject selectedObject;

    private bool buttonSelected = false;

	void Update ()
    {
		if(Input.GetAxisRaw("Vertical")!= 0 && buttonSelected==false)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
	}

    private void OnDisable()
    {
        buttonSelected = false;
    }
}
