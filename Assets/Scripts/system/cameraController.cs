using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [Header("Components")]
    public Camera cameraObj;

    [Header("image")]
    public GameObject target;
    public int zoom;

    public void Update()
    {
        cameraObj.transform.position = target.transform.position;
    }

    public void Changetarget(GameObject newTarget)
    {
        target = newTarget;
    }
}
