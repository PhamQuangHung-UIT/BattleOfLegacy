using Cinemachine;
using UnityEngine;

public class CustomCameraConfiner : MonoBehaviour
{
    [SerializeField] Collider2D confiner;
    private CinemachineVirtualCamera vcam;
    private float halfViewLength;
    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        halfViewLength = vcam.m_Lens.OrthographicSize;
    }

    void LateUpdate()
    {
        float ratio = (float) Screen.width / Screen.height;
        float minXPos = confiner.bounds.min.x + ratio * halfViewLength;
        float maxXPos = confiner.bounds.max.x - ratio * halfViewLength;
        float xPos = Mathf.Clamp(transform.position.x, minXPos, maxXPos);
        transform.position = new(xPos, transform.position.y, transform.position.z);
    }
}
