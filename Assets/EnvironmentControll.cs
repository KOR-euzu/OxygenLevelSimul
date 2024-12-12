using UnityEngine;

public class EnvironmentControll : MonoBehaviour
{
    [SerializeField]
    float OxygenLevel;

    public float GetOxygen() { return OxygenLevel; }
}
