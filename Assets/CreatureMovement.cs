using UnityEngine;

public class CreatureMovement : MonoBehaviour
{
    //추적용 변수
    [SerializeField]
    public float Speed;

    private void Update()
    {
        FoodTracking();
    }

    void FoodTracking()
    {
        Transform FoodPos = FindCloseFood();
        float distance = Vector3.Distance(transform.position, FoodPos.position);
        if (distance >= 0.5)
        {
            transform.LookAt(FoodPos);

            transform.position = Vector3.MoveTowards(transform.position, FoodPos.position, Speed * Time.deltaTime);
        }
    }

    private Transform FindCloseFood()
    {
        GameObject[] Foods = GameObject.FindGameObjectsWithTag("Food");
        float MaxDistance = Mathf.Infinity;
        Transform ClosestTarget = null;
        foreach (GameObject food in Foods)
        {
            float TargetDistance = Vector3.Distance(transform.position, food.transform.position);

            if (TargetDistance < MaxDistance)
            {
                ClosestTarget = food.transform;
                MaxDistance = TargetDistance;
            }
        }
        return ClosestTarget;
    }
}