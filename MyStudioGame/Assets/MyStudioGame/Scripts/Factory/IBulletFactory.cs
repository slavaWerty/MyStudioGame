using UnityEngine;

public interface IBulletFactory
{
    public IBullet CreateBullet(Vector3 direction, Vector3 position, float duration, Transform container);
}

