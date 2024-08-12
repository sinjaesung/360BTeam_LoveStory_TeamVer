using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField]
    protected EnemyProjectile projectilePrefab;//�߻�ü ������
    [SerializeField]
    protected Transform projectileSpawnPoint;//�߻�ü ������ġ
    public Transform attacktarget;//���� ���� ���

    [SerializeField] public bool attacking = false;
    [SerializeField]
    protected float attackDistance = 10;
    [SerializeField]
    protected float attackRate = 1;
    protected float lastAttackTime = 0;//���� �ֱ� ���� ����

    private void Awake()
    {
        attacktarget = FindObjectOfType<Player3d_Planet>().transform;
    }

    private void Start()
    {
        StopCoroutine("AttackExe");
        StartCoroutine("AttackExe");
    }

    private void AttackReset()
    {
        StopCoroutine("AtackExe");
    }

    private void OnDisable()
    {
        //���� ��Ȱ��ȭ�� �� ���� ������� ���¸� �����ϰ�, ���¸� None���� ����
        AttackReset();
        StopAllCoroutines();
        Debug.Log("Enemy Disable");
    }
    
    private IEnumerator AttackExe()
    {
        while (true)
        {
            if (attacktarget)
            {
                //Ÿ�� ���� �ֽ�
                transform.LookAt(attacktarget.position);
                if (Time.time - lastAttackTime > attackRate)
                {
                    attacking = true;

                    //RaycastHit hit
                    //�����ֱⰡ �Ǿ� ������ �� �ֵ��� �ϱ� ���� ���� �ð� ����
                    lastAttackTime = Time.time;

                    if(attacktarget.GetComponent<Player3d_Planet>().HeartCount_ <= 0)
                    {
                        Debug.Log("ĳ���Ͱ� �����߿� �׾����� ������ �ߴ�!");
                        StopCoroutine("AttackExe");
                        AttackReset();
                        yield break;
                    }
                    Debug.Log("Enemy��� AttackExe �ڷ�ƾ ���ݽõ�>>" + attacktarget.position);
                    EnemyProjectile projectileObject = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
                    projectileObject.Setup((attacktarget.position), attackDistance);
                }
            }
            yield return null;
        }
    }
}

