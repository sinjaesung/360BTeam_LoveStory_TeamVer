using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private MovementTransform movement;
    public float projectileDistance = 30;//�߻�ü �ִ� �߻�Ÿ�
    [SerializeField] private int damage = 5;//�߻�ü ���ݷ�

    [SerializeField] private GameObject hit_effect_prefab;

    public void Awake()
    {
        
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    public void Setup(Vector3 position,float attack_distance)
    {
        movement = GetComponent<MovementTransform>();
        projectileDistance = attack_distance;

        StartCoroutine("OnMove", position);
    }
    private void Update()
    {
        if (transform.position.y <= -999)
        {
            Debug.Log("EnemyProjectile ���� transformY��ġ�� -999���Ϸ� �͹��Ͼ��� �۰� ������ �ڽŻ���" + gameObject);
            //memoryPool.DeactivatePoolItem(gameObject);
            Destroy(gameObject);
        }
        float originfromDistance = Vector3.Magnitude(new Vector3(transform.position.x, transform.position.y, transform.position.z) - new Vector3(0, 0, 0));
        if (originfromDistance >= 99999)
        {
            Debug.Log("EnemyProjectile ���� transform��ġ�� �������κ��� �͹��Ͼ��� �ָ� �ڽŻ���" + gameObject + "transformposition:" + transform.position);
            //memoryPool.DeactivatePoolItem(gameObject);
            Destroy(gameObject);
        }
    }
    private IEnumerator OnMove(Vector3 targetPosition)
    {
        Vector3 start = transform.position;

        movement.MoveTo((targetPosition - transform.position).normalized);

        while (true)
        {
            if (Vector3.Distance(transform.position,start) >= projectileDistance)
            {
                if(transform != null)
                {
                    Debug.Log("EnemyProjectile]]�ִ� ��Ÿ� ����� ����");
                    Instantiate(hit_effect_prefab, transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
                yield break;
            }
            yield return null;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            Debug.Log("EnemyProjectileHit����" + other.transform.name + "," + other.transform.tag);

            if (other.CompareTag("Player"))
            {
                other.GetComponent<Player3d_Planet>().SetHealth(-damage);
                Debug.Log("EnemyProjectile onTriggerEnter Collider target Player Damage>>" + other.name+","+damage);

                if (transform != null)
                {
                    Instantiate(hit_effect_prefab, transform.position, Quaternion.identity);
                }
            }

            if(!other.CompareTag("Enemy") && !other.CompareTag("PlayerProjectile") && !other.CompareTag("EnemyProjectile")
                && !other.CompareTag("EnemyProjectileExplosion") && !other.CompareTag("PlayerProjectileExplosion"))
            {
                Debug.Log("Enemy��ҳ� Enemy,PlayerBullet,EnemyProjectileExplosion,PlayerProjectileExplosion�� �����ѰͿ� �ε������ enemyProjectile����");
                Destroy(gameObject);
            }
        }
    }

}
