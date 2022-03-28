  using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// Listens for touch events and performs an AR raycast from the screen touch point.
/// AR raycasts will only hit detected trackables like feature points and planes.
///
/// If a raycast hits a trackable, the <see cref="placedPrefab"/> is instantiated
/// and moved to the hit position.
/// </summary>
[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlaneOnce : MonoBehaviour
{

    /// Flag for repositioning the object in Update()
    private bool isSecondFrame = false;

    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject m_PlacedPrefab;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedObject { get; private set; }

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        // Only on the second frame apply these transformations
        // (transformations operate better if they are on a separate frame from instantiation)
        if (isSecondFrame) {
            rotateObject();

            // It's no longer the second frame
            isSecondFrame = false;
        }

        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            // Raycast hits are sorted by distance, so the first one is the closest hit
            var hitPose = s_Hits[0].pose;

            // Only allow for the placement of one object
            if ((spawnedObject == null))
            {
                // Push the object away from the camera given wherever the camera is curently looking
                Vector3 tempTransform = hitPose.position + Camera.main.transform.forward * 3.5f;
                tempTransform.y = hitPose.position.y;

                // Instantiate the object at the touch position pushed away from the camera
                spawnedObject = Instantiate(m_PlacedPrefab, tempTransform, hitPose.rotation);

                // For the next iteration of Update()
                isSecondFrame = true;
            }
        }
    }

    void rotateObject() {
        float tempY =  spawnedObject.transform.position.y;

        // Rotate the object to match the cameras rotation
        Vector3 cameraPosition = Camera.main.transform.position;
        cameraPosition.y = tempY;
        spawnedObject.transform.LookAt(cameraPosition, spawnedObject.transform.up);

        // Flip said rotation so instead of facing away it faces toward,
        // note that when rotating offcenter prefabs they drift, we account for that drift with 
        // the 0.2 on the x axis and offsetting the 180 flip by 10
        spawnedObject.transform.RotateAround(
            new Vector3(
                spawnedObject.transform.position.x + 0.2f, 
                spawnedObject.transform.position.y, 
                spawnedObject.transform.position.z
            ), 
            transform.up, 
            170f
        );
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            var mousePosition = Input.mousePosition;
            touchPosition = new Vector2(mousePosition.x, mousePosition.y);
            return true;
        }
#else
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
#endif

        touchPosition = default;
        return false;
    }

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    ARRaycastManager m_RaycastManager;
}
