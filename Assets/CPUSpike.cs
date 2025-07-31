using UnityEngine;

public class CPUSpike : MonoBehaviour
{
    public GameObject prefab;

    void Update()
    {
        Instantiate(prefab, Vector3.zero, Quaternion.identity);
    }
}
