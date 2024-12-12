using UnityEngine;

public class Creatures : MonoBehaviour
{
    [SerializeField]
    GameObject CreaturePrefab;

    Rigidbody rigid;

    //성장용 변수
    [SerializeField]
    static float GrowingRate;

    [SerializeField]
    float DivideThreshold;
    [SerializeField]
    float DivideEnerge;

    float MaxEnerge;
    float Energe;
    [SerializeField]
    float MinimumEnerge;

    float Growth;
    int Generation;

    [SerializeField]
    float EnergePerFood;

    //추적용 변수
    [SerializeField]
    public float Speed;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // 성장 및 분열과 사망
        Growing();
        if(Energe < MinimumEnerge) Destroy(gameObject);

        // 먹이 획득 및 에너지 관리
        Tracking();
    }

    void Growing()
    {
        Growth += GrowingRate * Time.deltaTime;
        if(Growth >= DivideThreshold || Energe >= DivideEnerge)
        {
            Divide();
            Growth = 0;
        }
    }

    void Tracking()
    {
        Transform FoodPos = FindCloseFood();
        float distance = Vector3.Distance(transform.position, FoodPos.position);
        if (distance >= 0.5)
        {
            transform.LookAt(FoodPos);

            transform.position = Vector3.MoveTowards(transform.position, FoodPos.position, Speed * Time.deltaTime);
        }
    }

    void Divide()
    {
        Vector3 newPos = transform.position + new Vector3(1,0,1) * Random.Range(0.6f,1.4f);
        Generation++;
        GameObject NewCreature = Instantiate(CreaturePrefab, newPos, Quaternion.identity);
        NewCreature.GetComponent<Creatures>().Generation = Generation;
        NewCreature.transform.localScale = Vector3.one * Random.Range(0.4f,1.6f);
    }

    private Transform FindCloseFood()
    {
        GameObject[] Foods = GameObject.FindGameObjectsWithTag("Food");
        float MaxDistance = Mathf.Infinity;
        Transform ClosestTarget = null;
        foreach(GameObject food in Foods)
        {
            float TargetDistance = Vector3.Distance(transform.position, food.transform.position);

            if(TargetDistance < MaxDistance)
            {
                ClosestTarget = food.transform;
                MaxDistance = TargetDistance;
            }
        }
        return ClosestTarget;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            Energe += EnergePerFood;
            Destroy(collision.gameObject);
        }
    }
}
