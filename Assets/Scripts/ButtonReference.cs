using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonReference : MonoBehaviour
{
    public enum EventType
    {
        MENU,
        SOLO,
        MULTI
    }

    [SerializeField] public EventType Type;

    public UnityAction OnClick = null;

    private void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(() => OnClick?.Invoke());
    }
}
