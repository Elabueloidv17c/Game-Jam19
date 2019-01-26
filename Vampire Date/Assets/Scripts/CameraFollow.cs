using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Camera Cam;

    // Variables
    public Transform target;
    public Vector3 offset;

    // The speed of the camera
    public float m_smoothTime = 0.5f;

    /* Zoom Variables */
    private float m_minZoom = 40f;
    private float m_maxZoom = 10f;
    private float m_zoomLimiter = 50f;

    /* Track the Velocity */
    private Vector3 m_velocity;

    // Use this for initialization
    void Start()
    {
        Cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x + offset.x, offset.y, offset.z - 10);
        Zoom();
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(m_maxZoom, m_minZoom, GetGreatesDistance() / m_zoomLimiter); // This interprets between 2 values depending on a 3rd value
        Cam.fieldOfView = Mathf.Lerp(Cam.fieldOfView, newZoom, Time.deltaTime);
    }

    float GetGreatesDistance()
    {
        var bounds = new Bounds(target.position, Vector3.zero);
        bounds.Encapsulate(target.position);

        // We just want to return the width
        return bounds.size.x;
    }
}
