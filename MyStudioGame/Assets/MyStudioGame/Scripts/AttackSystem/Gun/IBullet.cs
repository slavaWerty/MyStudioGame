using UnityEngine;

public interface IBullet
{
    public float Speed { get; set; }
    public int Damage { get; set; }
    public Vector3 Direction { get; set; }
    public float Duration { get; set; }
}

