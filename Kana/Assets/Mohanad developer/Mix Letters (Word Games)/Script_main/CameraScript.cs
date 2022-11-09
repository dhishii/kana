namespace mainspace
{
    using UnityEngine;

    public class CameraScript : MonoBehaviour
    {

        void Start()
        {

            float xx = 414f;
            float yy = 896f;

            float screenRatio = (float)Screen.width / (float)Screen.height;
            float targetRatio = xx / yy;

            if (screenRatio >= targetRatio)
            {
                Camera.main.orthographicSize = yy / 2;
            }
            else
            {
                float differenceInSize = targetRatio / screenRatio;
                Camera.main.orthographicSize = yy / 2 * differenceInSize;
            }
        }

    }
}