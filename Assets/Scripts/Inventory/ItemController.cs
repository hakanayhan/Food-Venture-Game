using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    public ItemObject item;
    [SerializeField] private Image _icon;

    private void Start()
    {
        _icon.sprite = item.icon;
    }
}
