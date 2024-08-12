using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField]
    protected EnemyProjectile projectilePrefab;//발사체 프리팹
    [SerializeField]
    protected Transform projectileSpawnPoint;//발사체 생성위치
    public Transform attacktarget;//적의 공격 대상

    [SerializeField] public bool attacking = false;
    [SerializeField]
    protected float attackDistance = 10;
    [SerializeField]
    protected float attackRate = 1;
    protected float lastAttackTime = 0;//공격 주기 계산용 변수

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
        //적이 비활성화될 때 현재 재생중인 상태를 종료하고, 상태를 None으로 설정
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
                //타겟 방향 주시
                transform.LookAt(attacktarget.position);
                if (Time.time - lastAttackTime > attackRate)
                {
                    attacking = true;

                    //RaycastHit hit
                    //공격주기가 되야 공격할 수 있도록 하기 위해 현재 시간 저장
                    lastAttackTime = Time.time;

                    if(attacktarget.GetComponent<Player3d_Planet>().HeartCount_ <= 0)
                    {
                        Debug.Log("캐릭터가 공격중에 죽었으면 공격을 중단!");
                        StopCoroutine("AttackExe");
                        AttackReset();
                        yield break;
                    }
                    Debug.Log("Enemy요소 AttackExe 코루틴 공격시도>>" + attacktarget.position);
                    EnemyProjectile projectileObject = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
                    projectileObject.Setup((attacktarget.position), attackDistance);
                }
            }
            yield return null;
        }
    }
}

