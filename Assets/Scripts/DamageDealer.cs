using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private float damage = 5f;


    public float Damage => damage;
}
