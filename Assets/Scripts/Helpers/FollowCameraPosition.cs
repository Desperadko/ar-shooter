using UnityEngine;

namespace Game.Helpers
{
    public class FollowCameraPosition : MonoBehaviour
    {
        [SerializeField] private Camera cam;

        private void Awake()
        {
            cam = cam != null ? cam : Camera.current;
        }

        private void LateUpdate()
        {
            transform.position = cam.transform.position;
        }
    }
}
