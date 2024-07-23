using System;
using UnityEngine;

public class UsingItem : IDisposable
{
    private SelectedItem _item;
    private Transform _parent;
    private bool _isCrystal;

    public GameObject _currentObject;

    public UsingItem(SelectedItem item, Transform parent, bool isCrystal)
    {
        _item = item;
        _parent = parent;
        _isCrystal = isCrystal;

        _item.SelectedObjectChanged += OnSelectedObjectChanged;
    }

    private void OnSelectedObjectChanged()
    {
        if (_isCrystal)
            return;

        GameObject.Destroy(_currentObject);
        _currentObject = CreateObject(_parent);

        if (_currentObject == null)
        {
            GameObject.Destroy(_currentObject);
            return;
        }
    }

    public void Dispose()
    {
        _item.SelectedObjectChanged -= OnSelectedObjectChanged;
    }

    public GameObject CreateObject(Transform parent)
    {
        if (_item.CurrentSelectedSlot.isEmpty)
        {
            return null;
        }

        var prefap = Resources.Load<GameObject>($"Prefaps/{_item.CurrentSelectedSlot.ItemID}");
        var go = GameObject.Instantiate(prefap) as GameObject;
        go.transform.position = parent.position;
        go.transform.parent = parent;
        return go;
    }
}

