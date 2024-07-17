using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "SwordConfig", menuName = "Game/new SwordConfig")]
    public class SwordConfig : ScriptableObject
    {
        [SerializeField] private DataSword _data;

        public DataSword DataSword => _data;
    }
}
