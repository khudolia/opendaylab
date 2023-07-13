using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class DecalProjectorController : MonoBehaviour
{
    public float minSize = 1f; // Minimum size of the projector
    public float maxSize = 5f; // Maximum size of the projector
    public float minPivotZ = -0.5f; // Minimum pivot Z position
    public float maxPivotZ = 0.5f; // Maximum pivot Z position

    private DecalProjector decalProjector;
    private RaycastHit hitInfo;

    private float _aspectRatio;
    private float _aspectRatioZ;

    private void Start()
    {
        decalProjector = GetComponent<DecalProjector>();

        _aspectRatio = decalProjector.size.x / decalProjector.size.y;  
        _aspectRatioZ = decalProjector.size.z / decalProjector.size.y;  
        if (decalProjector == null)
        {
            Debug.LogError("DecalProjector component not found!");
            return;
        }
    }

    private void Update()
    {
        // Send a ray forward from the decal projector
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo))
        {
            float distance = hitInfo.distance;

            // Calculate the normalized distance between the minimum and maximum distance
            float normalizedDistance = Mathf.Clamp01(distance / maxSize);

            // Calculate the new size based on the normalized distance
            float newSize = Mathf.Lerp(minSize, maxSize, normalizedDistance);

            // Update the size of the decal projector
            decalProjector.size = new Vector3(newSize * _aspectRatio, newSize , newSize * _aspectRatioZ);

            // Update the pivot Z position of the decal projector
            decalProjector.pivot = new Vector3(decalProjector.pivot.x, decalProjector.pivot.y, newSize/2 + gameObject.transform.localScale.z /2 + 14f);
        }
    }
}