//MAPNAV Navigation ToolKit v.1.4.0

//MapNav 2D GameObject Resize Tool (Fixes object screen aspect regardless of zoom level)
//Use only if this object mesh is a PLANE on 2D view mode

using UnityEngine;
using System.Collections;

[AddComponentMenu("MAPNAV/FixPlaneAspect")]

public class FixPlaneAspect : MonoBehaviour
{
    private Camera mycam;
    private Vector3 initScale;
    private Transform mytransform;
    private float lastOrthoSize;
    private SetGeolocation setLocation;

    void Awake()
    {
        mycam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mytransform = transform;

        //Check if the SetGeolocation script is being used 
        try
        {
            setLocation = gameObject.GetComponent<SetGeolocation>();
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }

        //Use SetGeolocation scale values as size reference
        if (setLocation != null)
            initScale = new Vector3(setLocation.scaleX, setLocation.scaleY, setLocation.scaleZ);
        else
            initScale = transform.localScale;
    }

    void Update()
    {
        if (mycam.orthographicSize != lastOrthoSize)
        {
            //Resize game object according to camera orthographic size (Reference zoom level is 15). 
            mytransform.localEulerAngles = new Vector3(0, mytransform.localEulerAngles.y, 0);
            float temp = mycam.orthographicSize / 599.35f;
            mytransform.localScale = new Vector3(initScale.x * temp, initScale.y * temp, initScale.z * temp);
        }
        lastOrthoSize = mycam.orthographicSize;
    }
}