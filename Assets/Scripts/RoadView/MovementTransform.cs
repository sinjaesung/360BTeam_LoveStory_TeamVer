using UnityEngine;

public class MovementTransform : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.0f;
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;

    [SerializeField]
    public bool isEnd = false;
    private void Update()
    {
        if (!isEnd)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
        else
        {
            Debug.Log("MovementTransform isEndµµ´Þ>" + isEnd);
        }
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
}