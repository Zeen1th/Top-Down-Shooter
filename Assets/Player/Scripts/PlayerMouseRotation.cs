using UnityEngine;

public class PlayerMouseRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 15f;
    [SerializeField] private LayerMask groundLayer;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;


    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, groundLayer))
            {
                Vector3 targetPosition = hitInfo.point;
                Vector3 direction = (targetPosition - transform.position);
                direction.y = 0f;

                if (direction.sqrMagnitude > 0.01f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
            }
        }
    }
}
