namespace mapspace
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MainCameraScroll : MonoBehaviour
    {
        float panSpeed = 50f;

        void Update()
        {
            if (Input.GetMouseButton(0)) // right mouse button
            {
                var newPosition = new Vector3();
                newPosition.y = Input.GetAxis("Mouse Y") * panSpeed * Time.fixedDeltaTime;
                // translates to the opposite direction of mouse position.
                transform.Translate(-newPosition);
            }
        }
    }
}
