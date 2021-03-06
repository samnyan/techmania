﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class Panel : MonoBehaviour
{
    public static Panel current;
    public Selectable defaultSelectable;
    [Tooltip("Set to true if the panel reconstructs objects on enable.")]
    public bool restoreSelectableOnEnable;
    private GameObject selectedBeforeDisable;

    // Start is called before the first frame update
    void Start()
    {
        selectedBeforeDisable = null;
    }

    private void OnEnable()
    {
        current = this;
        if (restoreSelectableOnEnable && selectedBeforeDisable != null)
        {
            EventSystem.current.SetSelectedGameObject(selectedBeforeDisable);
        }
        else if (defaultSelectable != null)
        {
            EventSystem.current.SetSelectedGameObject(defaultSelectable.gameObject);
        }
    }

    private void OnDisable()
    {
        if (restoreSelectableOnEnable)
        {
            selectedBeforeDisable = EventSystem.current?.currentSelectedGameObject;
        }
    }
}
