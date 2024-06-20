using UnityEngine;

public class Gun : IWearpon
{
    public float StartTimeBtwShots { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void Attack()
    {
        Debug.Log("Gun");
    }
}

