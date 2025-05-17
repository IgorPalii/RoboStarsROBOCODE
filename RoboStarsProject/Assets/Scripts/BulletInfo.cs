using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BulletData", order = 1)]
public class BulletInfo : ScriptableObject
{
    public GameObject render;
    public float speed;
    public int damage;
}
