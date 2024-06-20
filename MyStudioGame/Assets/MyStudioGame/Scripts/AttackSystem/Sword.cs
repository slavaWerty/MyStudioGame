using UnityEngine;

public class Sword : IWearpon
{
    public float StartTimeBtwShots { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void Attack()
    {
        Debug.Log("Sword");
    }
}

