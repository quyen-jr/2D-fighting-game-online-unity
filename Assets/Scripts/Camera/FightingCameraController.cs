using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

[RequireComponent (typeof(Camera))]
public class FightingCameraController : MonoBehaviour
{
    public Transform characterPlayerBody;
    public Transform enemyPlayerBody;
    public Vector3 offset;
    private Vector3 velocity;
    public float smoothTime=0.5f;

    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomLimiter = 50f;
    private Camera cam;
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    //private void Update()
    //{
    //    Move();
    //    Zoom();
    //}
    void LateUpdate()
    {
        Move();
        Zoom();
    }
    void Zoom()
    {
        if (characterPlayerBody == null || enemyPlayerBody == null) return;
        var newZoom= Mathf.Lerp(maxZoom,minZoom,GetGreatestDistance()/zoomLimiter);
        var newZoomHeigth = Mathf.Lerp(maxZoom, minZoom, GetGreatestHeightDistance() / 25);

        if(newZoom<newZoomHeigth) newZoom=newZoomHeigth;
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom,Time.deltaTime*2.5f);
    }
    float GetGreatestDistance()
    {
        var bounds = new Bounds(characterPlayerBody.position, Vector3.zero);
        bounds.Encapsulate(enemyPlayerBody.position);
        return bounds.size.x;
    }
    float GetGreatestHeightDistance()
    {
        var bounds = new Bounds(characterPlayerBody.position, Vector3.zero);
        bounds.Encapsulate(enemyPlayerBody.position);
        return bounds.size.y;
    }
    void Move()
    {
        Vector3 centrePoint = GetCentrePoint();
        if (characterPlayerBody == null || enemyPlayerBody == null) return;

        var newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        var newZoomHeigth = Mathf.Lerp(maxZoom, minZoom, GetGreatestHeightDistance() / 20);
        if (newZoom > newZoomHeigth) offset.y = cam.orthographicSize - maxZoom - 0.1f * (cam.orthographicSize - maxZoom);
        else offset.y = 0;
        Vector3 newPosition = centrePoint+ offset;
        newPosition.z = -1;

        //  check bound for camera
        if (newPosition.x - cam.orthographicSize * 2 <= -10) newPosition.x = (float)-10 + cam.orthographicSize * 2;
        if (newPosition.x + cam.orthographicSize * 2 >= 27) newPosition.x = (float)27 - cam.orthographicSize * 2;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }
    Vector3 GetCentrePoint()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Player");
        if (objectsWithTag.Length < 2) return Vector3.zero; 
        characterPlayerBody = objectsWithTag[0].transform;
        enemyPlayerBody = objectsWithTag[1].transform;
        var  bounds= new Bounds(characterPlayerBody.position,Vector3.zero);
        bounds.Encapsulate(enemyPlayerBody.position);
        return bounds.center;
    }
}
