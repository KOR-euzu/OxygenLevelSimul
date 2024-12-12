using Unity.VisualScripting;
using UnityEngine;

public class Creatures : MonoBehaviour
{
    Rigidbody rigid;
    GameObject Environment;

    //성장용 변수
    [SerializeField]
    float GrowingRate;

    [SerializeField]
    float DivideThreshold;
    [SerializeField]
    float DivideEnerge;

    float MaxEnerge;
    [SerializeField]
    float Energe = 100;
    [SerializeField]
    float MinimumEnerge;

    float Growth;
    int Generation;

    //에너지 관련 변수
    [SerializeField]
    float EnergePerFood;

    float EnergeConsuption;
    [SerializeField]
    float SpeedWeight;
    [SerializeField]
    float VolumeWeight;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        Environment = GameObject.FindGameObjectWithTag("Environment");
    }

    private void Update()
    {
        // 성장 및 분열과 사망
        Growing();
        if(Energe < MinimumEnerge) Destroy(gameObject);
    }

    private void OnEnable()
    {
        ResetEnergeConsuption();
    }

    void Growing()
    {
        Energe -= EnergeConsuption * Time.deltaTime;
        Growth += GrowingRate * Time.deltaTime;
        if(Growth >= DivideThreshold && Energe >= DivideEnerge)
        {
            Divide();
            Growth = 0;
        }
    }

    void Divide()
    {
        Vector3 newPos = transform.position + new Vector3(1,0,1) * Random.Range(0.6f,1.4f);
        Generation++;
        Energe /= 2;
        GameObject NewCreature = Instantiate(gameObject, newPos, Quaternion.identity);
        NewCreature.GetComponent<Creatures>().Generation = Generation;
        NewCreature.transform.localScale = Vector3.one * Random.Range(0.5f,1.5f);
        NewCreature.GetComponent<CreatureMovement>().Speed = GetComponent<CreatureMovement>().Speed * Random.Range(0.5f, 1.5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            Energe += EnergePerFood * GetSurfaceArea.CalculateSurface(gameObject) * VolumeWeight  * Environment.GetComponent<EnvironmentControll>().GetOxygen();
            Destroy(collision.gameObject);
        }
    }

    void ResetEnergeConsuption()
    {
        EnergeConsuption = 
            (SpeedWeight * GetComponent<CreatureMovement>().Speed)
            + (VolumeWeight * GetSurfaceArea.CalculateSurface(gameObject));
    }
}

