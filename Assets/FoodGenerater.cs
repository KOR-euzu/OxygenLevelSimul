using UnityEngine;

public class FoodGenerater : MonoBehaviour
{
    [SerializeField]
    GameObject pref;
    [SerializeField]
    float SpawnRate;
    [SerializeField]
    float minRange;
    [SerializeField]
    float maxRange;
    [SerializeField]
    float SpawnHeight;

    float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        
        if(timer >= SpawnRate )
        {
            timer = 0f;
            Vector3 SpawnPosition = RangeRandom(minRange, maxRange);

            Instantiate(pref, SpawnPosition, Quaternion.identity);
        }
    }

    Vector3 RangeRandom(float min, float max)
    {
        Vector3 Pos;
        Pos = new Vector3(Random.Range(-max, max), SpawnHeight, Random.Range(-max, max));
        if ((-min < Pos.x && Pos.x < min) && (-min < Pos.y && Pos.y < min)) Pos = RangeRandom(min, max);
        return Pos;
    }
}
