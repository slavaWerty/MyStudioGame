using Infentory;
using UnityEngine;
using VContainer;

public class ItemDetection : MonoBehaviour
{
    [SerializeField] private string _baseInfentoryOwnerId;
    [SerializeField] private string _crystalInfentoryOwnerId;
    [SerializeField] private float _lenght;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private InfentoryService _infentoryService;

    public string BaseInfentoryOwnerId => _baseInfentoryOwnerId;
    public string CrystalInfentoryOwnerId => _crystalInfentoryOwnerId;

    [Inject]
    public void Construct(InfentoryService service)
    {
        _infentoryService = service;
    }

    public void SearchItem()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _lenght, _layerMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            var collider = colliders[i].gameObject;

            if (!collider.TryGetComponent(out Item item))
                continue;

            if (item.ItemData.IsCrystal == true)
            {
                _infentoryService.AddItemConfigToInfentory(_crystalInfentoryOwnerId, item.ItemData);
                _infentoryService.AddItemToInfentory(_crystalInfentoryOwnerId,
                    item.ItemData.ItemID, item.ItemData.Sprite, item.Amount);
            }
            else if (item.ItemData.IsCrystal == false)
            {
                _infentoryService.AddItemConfigToInfentory(_baseInfentoryOwnerId, item.ItemData);
                _infentoryService.AddItemToInfentory(_baseInfentoryOwnerId,
                    item.ItemData.ItemID, item.ItemData.Sprite, item.Amount);
            }

            Destroy(collider, 0.1f);
        }
    }
}
