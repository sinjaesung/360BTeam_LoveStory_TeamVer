using UnityEngine;
using UnityEngine.UI;

public class Player3d_Planet : MonoBehaviour
{
    [SerializeField]
    private KeyCode keyCodeFire = KeyCode.Space;
    [SerializeField]
    private GameObject projectilePrefab;
    private float moveSpeed = 3;
    private Vector3 lastMoveDirection = Vector3.right;

    public bool IsMoved { set; get; } = true;  // 이동 가능 여부
    public bool IsAttacked { set; get; } = false;   // 공격 가능 여부

    [SerializeField] private new Camera camera;

    [SerializeField] private float fov = 0;

    [SerializeField] private float wheelSpeed = 10f;

    [SerializeField] private float roll;
    [SerializeField] private float pitch;
    [SerializeField] private float mouseSpeed = 10f;

    [SerializeField] private int loveScore_ = 0;
    [SerializeField] private int HeartCount = 3;
    [SerializeField] public Slider HealthSlider;
    public bool isGameOver = false;

    public LoveGameManager lovegameManager;

    public int LoveScore
    {
        get
        {
            return loveScore_;
        }
        set
        {
            loveScore_ = value;
        }
    }

    public int HeartCount_
    {
        get { return HeartCount; }

        set
        {
            HeartCount = value;
        }
    }
    //Reset 관련 정보들
    private void Awake()
    {
        fov = camera.fieldOfView;
        lovegameManager = FindObjectOfType<LoveGameManager>();

        loveScore_ = 0;//명시적으로 해당 개체는 씬별 개체로써,각 행성 도달시마다 사랑점수는 초기화
        HeartCount = 3;//행성도달시마다 목숨은 초기화
        isGameOver = false;//캐릭터 죽었나 살았나 여부 또한 초기화
    }
    public void SetHealth(int amount)
    {
        HeartCount += amount;

        if(HeartCount <= 0)
        {
            if (!isGameOver)
            {
                lovegameManager.GameOver();
            }
            isGameOver = true;
        }
    }
    public void UpdateHealthSlider()
    {
        HealthSlider.value = HeartCount;
    }

    private void Update()
    {
        UpdateHealthSlider();
        Debug.Log("Player3d LoveScore Setup>>" + loveScore_);
        //MonsterAnim.SetFloat("LoveScore", loveScore_);
        if (IsMoved == true)
        {
            //마우스 스크롤휠을 통한 카메라 화면 줌인아웃효과
            float wheel = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(wheel) > 0)
            {
                Debug.Log("Wheel");
                fov -= wheel * wheelSpeed;
                fov = Mathf.Clamp(fov, 25, 80);//25~80범위
                camera.fieldOfView = fov;
            }

            //마우스를 누르고 있는 상태에서 좌우로 움직여서 회전한다.카메라를 회전해야한다.
            if (Input.GetMouseButton(1))
            {

                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");

                roll -= mouseY * Time.deltaTime * mouseSpeed;//카메라 x축 회전(위아래회전)
                pitch -= mouseX * Time.deltaTime * mouseSpeed;//카메라 좌우 회전

                roll = Mathf.Clamp(roll, -30, 60);//위아래 회전 범위
            }       

            if (pitch >= 360f)
            {
                pitch -= 360f;
            }
            else
            {
                pitch += 360f;
            }

            camera.transform.eulerAngles = new Vector3(roll, pitch, 0);//x축회전,y축회전 반영
        }

        if (IsAttacked == true)
        {
            // 스페이스 키를 눌러 발사체 생성
            if (Input.GetKeyDown(keyCodeFire))
            {
                GameObject clone = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                clone.GetComponent<Projectile>().Setup(lastMoveDirection);
            }
        }
    }
}

