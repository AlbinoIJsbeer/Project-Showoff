using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public delegate void OnClick();
    public static event OnClick OnDockClicked;

    void Update()
    {
        CheckIfDockIsClicked();
    }

    private void CheckIfDockIsClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Dock")
                {
                    OnDockClicked?.Invoke();
                }
            }
        }
    }
}
