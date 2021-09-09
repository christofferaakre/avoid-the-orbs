using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager instance;

    public GameObject textContainer;
    public GameObject textPrefab;

    Queue<FloatingText> floatingTexts = new Queue<FloatingText>();

    private void Awake()
    {
        if (instance != null) 
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private FloatingText GetFloatingText() 
    {
        FloatingText floatingText;
        foreach (FloatingText _floatingText in floatingTexts)
        {
            if (!_floatingText.active)
                return _floatingText;
        }

        floatingText = new FloatingText();
        floatingText.go = Instantiate(textPrefab, parent: textContainer.transform);
        floatingText.textMesh = floatingText.go.GetComponent<TextMeshProUGUI>();
        floatingText.active = true;

        floatingTexts.Enqueue(floatingText);

        return floatingText;

    }

    public void ShowFloatingText(string message,
                                 int fontSize = 16,
                                 Color? color = null,
                                 Vector3? position = null,
                                 Vector3? motion = null,
                                 float duration = 1.0f
                                )
    {
        // Checking if parameters are null because there are no suitable
        // compile time constants that can be used as default arguments in the method
        if (color == null)
            color = Color.white;
        if (position == null)
            position = Vector3.zero;
        if (motion == null)
            motion = Vector3.zero;

        FloatingText floatingText = GetFloatingText();
        floatingText.textMesh.text = message;
        floatingText.textMesh.fontSize = (int) fontSize;
        floatingText.textMesh.color = (Color) color;
        floatingText.go.transform.position = Camera.main.WorldToScreenPoint( (Vector3) position);
        floatingText.duration = duration;
        floatingText.motion = (Vector3) motion;

        floatingText.Show();
    }

    private void Update()
    {
        foreach (FloatingText floatingText in floatingTexts)
        {
            floatingText.UpdateFloatingText();
        }
    }




}
