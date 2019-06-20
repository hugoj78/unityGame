// not code by us
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;

    public Vector2 maxPosition;
    public Vector2 minPosition;

    void Start()
    {
        // Prend la position de la target pour placer la camera dessus
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    void LateUpdate()
    {
        // si la position de la camera est differente de la target (ici le joueur)
        // La camera suit le joueur
        if (transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            // Centre la camera sur le joueur selon le min et le max definie avant pour
            // Pas depasser de la map
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y); 

            // Permet un mouvement de camera plus propre et jolie sur le joueur
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);


        }
    }
}
