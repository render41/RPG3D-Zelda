using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Cameras Actions")]
    [SerializeField] private GameObject camB = null;

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "CamTrigger":
                camB.SetActive(true);
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "CamTrigger":
                camB.SetActive(false);
                break;
        }
    }
}
