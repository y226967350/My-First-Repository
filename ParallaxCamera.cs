using UnityEngine;

public class ParallaxCamera : MonoBehaviour
{
    [Header("摄像头设置")]
    [Tooltip("跟随目标")]
    public Transform target;
    
    [Tooltip("跟随平滑度")]
    public float smoothSpeed = 0.125f;
    
    [Tooltip("视角偏移")]
    public Vector3 offset;
    
    [Tooltip("是否限制X轴移动")]
    public bool clampX = false;
    public float minX = -10f;
    public float maxX = 10f;
    
    [Tooltip("是否限制Y轴移动")]
    public bool clampY = false;
    public float minY = -5f;
    public float maxY = 5f;

    [Header("视差层预设")]
    [Tooltip("远景层速度（最慢）")]
    public Vector2 farLayerSpeed = new Vector2(0.1f, 0.1f);
    
    [Tooltip("中景层速度")]
    public Vector2 midLayerSpeed = new Vector2(0.3f, 0.3f);
    
    [Tooltip("近景层速度（最快）")]
    public Vector2 nearLayerSpeed = new Vector2(0.6f, 0.6f);

    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        
        if (clampX)
        {
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
        }
        
        if (clampY)
        {
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);
        }

        Vector3 smoothedPosition = Vector3.SmoothDamp(
            transform.position, 
            desiredPosition, 
            ref velocity, 
            smoothSpeed
        );

        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }

    [ContextMenu("创建视差层结构")]
    public void CreateParallaxLayers()
    {
        Transform parent = transform.parent ?? transform;
        
        CreateLayer(parent, "FarLayer", farLayerSpeed, -30f);
        CreateLayer(parent, "MidLayer", midLayerSpeed, -20f);
        CreateLayer(parent, "NearLayer", nearLayerSpeed, -10f);

        Debug.Log("已创建视差层结构，请在各层添加背景元素");
    }

    void CreateLayer(Transform parent, string name, Vector2 speed, float z)
    {
        GameObject layerObj = new GameObject(name);
        layerObj.transform.SetParent(parent);
        layerObj.transform.position = new Vector3(0, 0, z);

        ParallaxLayer parallax = layerObj.AddComponent<ParallaxLayer>();
        parallax.parallaxSpeed = speed;
        parallax.infiniteScroll = true;
        parallax.layerWidth = Camera.main.orthographicSize * Camera.main.aspect * 2;

        GameObject container = new GameObject("Content");
        container.transform.SetParent(layerObj.transform);
        container.transform.localPosition = Vector3.zero;
    }
}