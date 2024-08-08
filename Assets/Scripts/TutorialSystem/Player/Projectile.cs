using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 moveDirection;
    private float moveSpeed = 5.0f;

    [SerializeField] private GameObject ExplosionParticle;
    public void Setup(Vector3 direction)
    {
        Debug.Log("타노스 미니게임 3d Projectile 이동관련>>" + direction);
        moveDirection = direction;
    }

    private void Update()
    {
        Debug.Log("타노스 미니게임 3d Projectile 이동관련>>" + transform.position);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag.Contains("Enemy"))
        {
            Instantiate(ExplosionParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }   
    }
}
