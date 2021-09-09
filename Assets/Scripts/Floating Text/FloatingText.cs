using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText
{
    public bool active;
    public GameObject go;
    public TextMeshProUGUI textMesh;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    public void Show() 
    {
        active = true;
        lastShown = Time.time;
        go.SetActive(true);
    }

    public void Hide() 
    {
        active = false;
        lastShown = Time.time;
        go.SetActive(false);
    }

    public void UpdateFloatingText() 
    {
        if (!active)
            return;

        if (Time.time - lastShown > duration)
            Hide();

        go.transform.position += motion * Time.deltaTime;
    }
}

