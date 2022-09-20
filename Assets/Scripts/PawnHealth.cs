using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider), typeof(Animator))]
public class PawnHealth : MonoBehaviour
{
    private static readonly int DieAnimationHash = Animator.StringToHash("Die");

    [Header("Required Settings")]
    [SerializeField] private Slider healthSlider = null;

    [Header("Game Settings")]
    [SerializeField] private float health = 20f;
    public float Health => health;


    private Collider pawnCollider = null;
    private Animator animator = null;


    private void Awake() 
    {
        pawnCollider = GetComponent<Collider>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<DamageDealer>(out var damageDealer))
            return;

        health -= damageDealer.Damage;
        healthSlider.value = health;

        if (health <= 0f)
            Die();
    }


    private void Die()
    {
        healthSlider.gameObject.SetActive(false);

        pawnCollider.enabled = false;
        animator.SetTrigger(DieAnimationHash);
    }
}
