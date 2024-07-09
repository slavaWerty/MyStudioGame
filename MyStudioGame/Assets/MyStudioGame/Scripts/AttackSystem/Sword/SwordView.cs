using UnityEngine;

public class SwordView : MonoBehaviour
{
    private Transform _attackPoint;

    public Transform AttackPoint => _attackPoint;

    public void Initzialize()
    {
        _attackPoint = transform.GetChild(0);
    }
}

