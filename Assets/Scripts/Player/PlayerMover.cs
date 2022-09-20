using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class PlayerMover : MonoBehaviour
{
    private static readonly int VelocityAnimationHash = Animator.StringToHash("Velocity");

    private NavMeshAgent agent = null;
    private Animator animator = null;

    public State CurrentState => state;
    private State state = State.Idle;

    private Vector3 targetPosition = Vector3.zero;
    private bool finish = false;
    private float velocity = 0f;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        finish = false;
        transform.position = Waypoints.StartPosition;
        agent.isStopped = true;

        state = State.Idle;
    }

    private void Update()
    {
        AnimationPlay();

        switch (state)
        {
            case State.Idle:
                OnIdleState();
                break;
            case State.Moving:
                OnMovingState();
                break;
            case State.Attacking:
                OnAttackingState();
                break;
        }
    }

    private void OnIdleState()
    {
        if (finish || !GameController.StartGame)
            return;

        SetDestination(Waypoints.NextPosition);
        state = State.Moving;
    }

    private void OnMovingState()
    {
        if (agent.remainingDistance < 0.1f)
        {
            agent.isStopped = true;

            if (finish)
            {
                GameController.Finish();

                state = State.Idle;
                return;
            }

            EnemyController.EnableColliderForEnemies(Waypoints.Index);

            state = State.Attacking;
        }
    }

    private void OnAttackingState()
    {
        if (EnemyController.IsEnemyOnPlatformAlive(Waypoints.Index))
            return;

        if (Waypoints.IsGointToFinishPoint)
        {
            finish = true;

            SetDestination(Waypoints.FinishPosition);
            state = State.Moving;
            return;
        }

        state = State.Idle;
    }


    private void AnimationPlay()
    {
        velocity = agent.velocity.sqrMagnitude / (agent.speed * agent.speed);

        animator.SetFloat(VelocityAnimationHash, velocity);
    }

    private void SetDestination(Vector3 target)
    {
        targetPosition = target;
        agent.isStopped = false;

        agent.SetDestination(target);
    }


    public enum State
    {
        Idle,
        Moving,
        Attacking
    }
}
