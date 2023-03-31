using UnityEngine;

public class TutorialInfoFollower : MonoBehaviour
{
    public GameObject targetObject;
    public Transform cameraTransform;
    public float distanceToMove = 5f;
    public float speed = .5f;

    public float rotationSpeed = 5.0f; // speed at which to rotate the object towards the target rotation

    private Quaternion targetRotation;

    void Update()
    {
        targetRotation = targetObject.transform.rotation;

        Vector3 cameraForward = cameraTransform.TransformDirection(Vector3.forward);
        Vector3 targetPosition = cameraTransform.position + (cameraForward * distanceToMove);
        targetObject.transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);


        // smoothly rotate the object towards the target rotation
        Vector3 directionToTarget = cameraTransform.transform.position - transform.position;
        targetRotation = Quaternion.LookRotation(-directionToTarget);
        //targetRotation = new Quaternion(targetRotation.x, targetRotation.y, cameraTransform.rotation.z, targetRotation.w);
        Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        Quaternion newRotation1 = Quaternion.Slerp(newRotation, cameraTransform.rotation, rotationSpeed * Time.deltaTime);

        transform.rotation = newRotation1;
    }
}