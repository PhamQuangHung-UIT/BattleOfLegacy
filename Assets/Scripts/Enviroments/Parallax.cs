using UnityEngine;
using UnityEngine.Tilemaps;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 1f)]
    private float parallaxValue = 0;
    private Camera cam;
    public float length;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        length = GetComponent<TilemapRenderer>().bounds.size.x;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 relative_pos = cam.transform.position * parallaxValue;
        Vector3 dist = cam.transform.position - (Vector3) relative_pos;
        if (dist.x > startPos.x + length)
        {
            startPos.x += length;
        }
        if (dist.x < startPos.x - length)
        {
            startPos.x -= length;
        }
        transform.position = startPos + (Vector3) relative_pos;
    }
}
