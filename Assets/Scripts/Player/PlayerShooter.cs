using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerMover))]
public class PlayerShooter : MonoBehaviour
{
    private static readonly int ShootAnimationHash = Animator.StringToHash("Shoot");

    [Header("Required Settings")]
    [SerializeField] private BulletsHolder bulletsHolder = null;

    [Header("Game Settings")]
    [SerializeField] private float attackDelay = 0.2f;


    private Animator animator = null;
    private Camera mainCamera = null;
    private PlayerMover playerMover = null;

    private float attackInterval = 0f;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMover = GetComponent<PlayerMover>();
    }

    private void Start()
    {
        mainCamera = Camera.main;

        attackInterval = 0f;
    }

    private void Update()
    {
        attackInterval -= Time.deltaTime;

        if (attackInterval > 0f)
            return;

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            attackInterval = attackDelay;

            if (playerMover.CurrentState == PlayerMover.State.Attacking)
                Shoot();
        }
    }


    private void Shoot()
    {
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.touches[0].position), out var hit))
        {
            var bullet = bulletsHolder.GetBullet();
            bullet.Init(from: bulletsHolder.ShootFromPosition, target: hit.point);

            animator.SetTrigger(ShootAnimationHash);
        }
    }
}
