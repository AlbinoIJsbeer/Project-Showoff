using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockPointer : MonoBehaviour
{
    [SerializeField] private Transform boat;
    [SerializeField] private Transform dock;

    private Transform pointer;

    private Vector3 tempSize;
    private float minSize = 6f;
    private float maxSize = 10f;
    private float resizer;

    // Start is called before the first frame update
    void Start()
    {
        pointer = transform.GetChild(0).GetComponent<Transform>();
        resizer = 0.025f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = boat.position;
        Resize();
        PointAtDock();
    }

    private void PointAtDock()
    {
        Quaternion targetRotation = Quaternion.LookRotation(dock.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1 * Time.deltaTime);
    }

    private void Resize()
    {
        tempSize = pointer.transform.localScale;
        tempSize.x += resizer;
        //tempSize.y += resizer;
        //tempSize.z += resizer;
        pointer.transform.localScale = tempSize;

        if (tempSize.x <= minSize)
            resizer = 0.025f;
        else if (tempSize.x >= maxSize)
            resizer = -0.025f;
    }
}
