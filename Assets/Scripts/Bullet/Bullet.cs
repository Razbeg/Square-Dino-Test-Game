using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private float timeToDisable = 5f;
    [SerializeField] private float speed = 1f;


    private Rigidbody bulletRigidBody = null;
    private Vector3 direction = Vector3.zero;


    private void Awake() => bulletRigidBody = GetComponent<Rigidbody>();

    private void FixedUpdate() => bulletRigidBody.velocity = direction * speed;

    private void OnTriggerEnter(Collider other) => gameObject.SetActive(false);


    public void Init(Vector3 from, Vector3 target)
    {
        transform.position = from;
        direction = (target - transform.position).normalized;

        transform.LookAt(target);


        gameObject.SetActive(true);

        StartCoroutine(DisableAfterTime());
    }


    private IEnumerator DisableAfterTime()
    {
        yield return new WaitForSeconds(timeToDisable);

        gameObject.SetActive(false);
    }
}
