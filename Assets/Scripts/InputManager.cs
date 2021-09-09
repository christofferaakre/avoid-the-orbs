using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] InputActionAsset actionsAsset;

    public static InputManager instance;
    private void Awake()
    {
        if (instance != null) 
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        actionsAsset = GetComponent<PlayerInput>().actions;

    }

    public InputActionMap GetActionMap(string name) 
    {
        return actionsAsset.FindActionMap(name);
    }

    public List<InputActionMap> GetAllActionMaps()
    {
        return actionsAsset.actionMaps.ToList();
    }

    public List<InputActionMap> GetActiveActionMaps() 
    {
        List<InputActionMap> actionMaps = GetAllActionMaps();
        return actionMaps.FindAll(actionMap => actionMap.enabled);
    }
    public void UseActionMaps(List<string> names) 
    {
        foreach (InputActionMap actionMap in GetActiveActionMaps())
        {
            actionMap.Disable();
        }

        EnableActionMaps(names);
    }

    public void UseActionMap(string name) 
    {
        UseActionMaps(new List<string> { name });
    }


    public void EnableActionMap(string name)
    {
        InputActionMap actionMap = actionsAsset.FindActionMap(name);
        actionMap.Enable();
    }

    public void DisableActionMap(string name) 
    {
        InputActionMap actionMap = actionsAsset.FindActionMap(name);
        actionMap.Disable();
    }

    public void EnableActionMaps(List<string> names) 
    {
        foreach (string name in names)
        {
            EnableActionMap(name);
        } 
    }
    public void DisableActionMaps(List<string> names) 
    {
        foreach (string name in names)
        {
            DisableActionMap(name);
        } 
    }
}
