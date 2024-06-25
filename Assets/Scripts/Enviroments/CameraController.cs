using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[DisallowMultipleComponent]
public class CameraController : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    CinemachineConfiner confiner;

    [SerializeField] private float moveSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        confiner = GetComponent<CinemachineConfiner>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if (confiner.CameraWasDisplaced(cam))
            return;
        cam.transform.Translate(horizontal * moveSpeed * Time.deltaTime, 0, 0);
    }
}
