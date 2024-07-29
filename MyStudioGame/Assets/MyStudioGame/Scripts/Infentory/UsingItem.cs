using Infentory;
using System;
using UnityEngine;

public class UsingItem : IDisposable
{
    private SelectedItem _item;
    private ThrowItem _throwItem;
    private Transform _parent;
    private bool _isCrystal;

    private InfentoryService _service;
    private Coroutines _coroutines;
    private Camera _camera;

    private GameObject _currentObject;

    public GameObject CurrentObject => _currentObject;

    public UsingItem(SelectedItem item, Transform parent, bool isCrystal,
        InfentoryService service, Coroutines coroutines, Camera camera)
    {
        _item = item;
        _parent = parent;
        _isCrystal = isCrystal;
        _service = service;
        _coroutines = coroutines;
        _camera = camera;

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
            return null;

        GameObject go;

        var prefap = Resources.Load<GameObject>($"Prefaps/Using{_item.CurrentSelectedSlot.ItemID}");

        if (prefap != null)
            go = GameObject.Instantiate(prefap);
        else
        {
            Debug.Log("Item not Prefap Object");
            return null;
        }

        if(go.TryGetComponent(out IInitzializer initzializer))
            initzializer.Initzialize(_service, _coroutines, _camera);

        go.transform.position = parent.position;
        go.transform.parent = parent;
        return go;
    }
}

