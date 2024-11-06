using UnityEngine;

public class Creatures : MonoBehaviour
{
    [SerializeField]
    GameObject CreaturePrefab;

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

    private void Start()
    {
        
    }
    private void Update()
    {
        Growing();
        if(Energe < MinimumEnerge)
        {
            Destroy(gameObject);
        }
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

    void Divide()
    {
        Vector3 newPos = transform.position + new Vector3(1,0,1) * Random.Range(0.6f,1.4f);
        Generation++;
        GameObject NewCreature = Instantiate(CreaturePrefab, newPos, Quaternion.identity);
        NewCreature.GetComponent<Creatures>().Generation = Generation;
        NewCreature.transform.localScale = Vector3.one * Random.Range(0.4f,1.6f);
    }
}
