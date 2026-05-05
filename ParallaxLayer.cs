using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [Header("视差设置")]
    [Tooltip("视差速度 - 值越小移动越慢，看起来越远")]
    public Vector2 parallaxSpeed = new Vector2(0.5f, 0.5f);
    
    [Tooltip("是否使用无限滚动")]
    public bool infiniteScroll = false;
    
    [Tooltip("层的宽度（用于无限滚动）")]
    public float layerWidth = 10f;

    private Transform cameraTransform;
    private Vector3 previousCameraPosition;
    private Vector3 startPosition;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
        startPosition = transform.position;
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - previousCameraPosition;
        
        transform.position += new Vector3(
            deltaMovement.x * parallaxSpeed.x,
            deltaMovement.y * parallaxSpeed.y,
            0f
        );

        if (infiniteScroll)
        {
            HandleInfiniteScroll();
        }

        previousCameraPosition = cameraTransform.position;
    }

    void HandleInfiniteScroll()
    {
        float cameraX = cameraTransform.position.x;
        float halfWidth = layerWidth / 2f;

        if (transform.position.x < cameraX - halfWidth - layerWidth)
        {
            transform.position = new Vector3(
                transform.position.x + layerWidth * 2,
                transform.position.y,
                transform.position.z
            );
        }
        else if (transform.position.x > cameraX + halfWidth + layerWidth)
        {
            transform.position = new Vector3(
                transform.position.x - layerWidth * 2,
                transform.position.y,
                transform.position.z
            );
        }
    }
}