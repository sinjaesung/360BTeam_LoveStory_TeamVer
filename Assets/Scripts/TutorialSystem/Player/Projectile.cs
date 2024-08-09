using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 moveDirection;
    private float moveSpeed = 5.0f;

    [SerializeField] private GameObject ExplosionParticle;
    public float projectileDistance = 30;
    public Player3d_Planet player;
    public void Setup(Vector3 direction)
    {
        player = FindObjectOfType<Player3d_Planet>();
        Debug.Log("Ÿ�뽺 �̴ϰ��� 3d Projectile �̵�����>>" + direction);
        moveDirection = direction;
    }

    private void Update()
    {
        Debug.Log("Ÿ�뽺 �̴ϰ��� 3d Projectile �̵�����>>" + transform.position);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        if (transform.position.y <= -999)
        {
            Debug.Log("Projectile ���� transformY��ġ�� -999���Ϸ� �͹��Ͼ��� �۰� ������ �ڽŻ���" + gameObject);
            //memoryPool.DeactivatePoolItem(gameObject);
            Destroy(gameObject);
        }
        float originfromDistance = Vector3.Magnitude(new Vector3(transform.position.x, transform.position.y, transform.position.z) - new Vector3(0, 0, 0));
        if (originfromDistance >= 800)
        {
            Debug.Log("Projectile ���� transform��ġ�� �������κ��� �͹��Ͼ��� �ָ� �ڽŻ���" + gameObject + "transformposition:" + transform.position);
            //memoryPool.DeactivatePoolItem(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag.Contains("Enemy"))
        {
            Instantiate(ExplosionParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(collision.gameObject);
            player.LoveScore += 3;
        }
        else if(collision.tag.Contains("Wall"))
        {
            Debug.Log("Projectile Wall Collision>>" + collision.transform.name);
            Destroy(gameObject);
        }  
    }
}
