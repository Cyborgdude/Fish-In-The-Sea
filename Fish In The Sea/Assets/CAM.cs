using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAM : MonoBehaviour
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    public class MatchWidth : MonoBehaviour
    {

        // Set this to the in-world distance between the left & right edges of your scene.
        public float sceneWidth = 17.6f;

        Camera camera;
        void Start()
        {
            camera = GetComponent<Camera>();
        }

        // Adjust the camera's height so the desired scene width fits in view
        // even if the screen/window size changes dynamically.
        void Update()
        {
            float unitsPerPixel = sceneWidth / Screen.width;

            float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;

            camera.orthographicSize = desiredHalfHeight;
        }
    }
}
