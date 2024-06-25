using Cinemachine;
using UnityEngine;

[DisallowMultipleComponent]
public class MobileCameraController : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0) return;

        var touch = Input.GetTouch(0);

        //TODO: Update touch to be disable when spell is selected or outside the battlefield

        if (touch.phase == TouchPhase.Moved)
        {
            cam.transform.Translate(-touch.deltaPosition.x * speed * Time.deltaTime, 0, 0);
        }

    }
}
