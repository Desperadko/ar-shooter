using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

namespace Game.UI
{
    public class ScanManager : MonoBehaviour
    {
        public ARPlane MainPlane { get; private set; }
        
        [SerializeField] private Button startButton;
        [SerializeField] private ARPlaneManager planeManager;
        [SerializeField] private ARSession arSession;
        [SerializeField] private TextMeshProUGUI terrainAmountScannedText;
        [SerializeField] private float minimumArea;

        private float totalArea = 0f;
        private TextMeshProUGUI startButtonTextMesh;

        private void Awake()
        {
            GameManager.OnInitialized += DisableManager;
        }

        void OnEnable()
        {
            GameManager.OnScan += EnableManager;
            GameManager.OnGameStarted += StopAndLockLargestPlane;
            GameManager.OnMainMenuOpened += DisableManager;
            GameManager.OnMainMenuOpened += ClearPlanes;

            planeManager.trackablesChanged.AddListener(OnPlanesChanged);

            startButtonTextMesh = startButton.GetComponentInChildren<TextMeshProUGUI>();
            startButtonTextMesh.text = "Scanning..";
            startButton.interactable = false;
        }

        void OnDisable()
        {
            GameManager.OnScan -= EnableManager;
            GameManager.OnGameStarted -= StopAndLockLargestPlane;
            GameManager.OnMainMenuOpened -= DisableManager;
            GameManager.OnMainMenuOpened -= ClearPlanes;

            planeManager.trackablesChanged.RemoveListener(OnPlanesChanged);
        }

        private void OnDestroy()
        {
            GameManager.OnInitialized -= DisableManager;
        }

        private void EnableManager()
        {
            ClearPlanes();

            totalArea = 0f;
            terrainAmountScannedText.text = $"Amount scanned: 0.0m² | Minimum: {minimumArea}m²";
            startButtonTextMesh.text = "Scanning..";
            startButton.interactable = false;

            planeManager.enabled = true;
        }

        private void DisableManager()
        {
            planeManager.enabled = false;
        }

        private void LockLargestPlane()
        {
            ARPlane largest = null;
            float largestArea = 0f;

            foreach (var plane in planeManager.trackables)
            {
                if(!plane) continue; 

                float area = CalculatePolygonArea(plane.boundary);

                if (area > largestArea)
                {
                    largestArea = area;
                    largest = plane;
                }
            }

            foreach (var plane in planeManager.trackables)
            {
                if(!plane) continue;

                if (plane == largest)
                {
                    if (!plane.TryGetComponent<MeshCollider>(out _))
                        plane.gameObject.AddComponent<MeshCollider>();
                }
                else
                {
                    Destroy(plane.gameObject);
                }
            }

            MainPlane = largest;
        }

        private void StopAndLockLargestPlane()
        {
            DisableManager();
            LockLargestPlane();
        }

        private void ClearPlanes()
        {
            MainPlane = null;

            foreach (var plane in planeManager.trackables)
            {
                if (!plane) continue;

                Destroy(plane.gameObject);
            }

            arSession.Reset();
        }

        private void OnPlanesChanged(ARTrackablesChangedEventArgs<ARPlane> args)
        {
            totalArea = CalculateTerrain();

            terrainAmountScannedText.text = $"Amount scanned: {totalArea:F1}m² | Minimum: {minimumArea}m²";
        
            if(totalArea >= minimumArea)
            {
                startButton.interactable = true;
                startButtonTextMesh.text = "Start";
            }
        }

        private float CalculateTerrain()
        {
            float area = 0f;

            foreach (var plane in planeManager.trackables)
            {
                area += CalculatePolygonArea(plane.boundary);
            }

            return area;
        }

        private float CalculatePolygonArea(NativeArray<Vector2> boundary)
        {
            int count = boundary.Length;
            if (count < 3) return 0f;

            float area = 0f;

            for (int i = 0; i < count; i++)
            {
                Vector2 current = boundary[i];
                Vector2 next = boundary[(i + 1) % count];

                area += current.x * next.y;
                area -= next.x * current.y;
            }

            return Mathf.Abs(area) / 2f;
        }
    }
}
