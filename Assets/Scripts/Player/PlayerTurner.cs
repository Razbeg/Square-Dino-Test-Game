using UnityEngine;

public class PlayerTurner : MonoBehaviour
{
    private void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Moved)
            transform.Rotate(0f, Input.touches[0].deltaPosition.x * 5f * Time.deltaTime, 0f);
    }
}
