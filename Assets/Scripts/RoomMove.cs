// Code by us
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    public Vector2 maxCameraChange;
    public Vector2 minCameraChange;
    public Vector3 playerChange;
    private CameraMovement cam;

    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;


    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            // change les min et max de la cam
            cam.minPosition.x = minCameraChange.x;
            cam.minPosition.y = minCameraChange.y;
            cam.maxPosition.x = maxCameraChange.x;
            cam.maxPosition.y = maxCameraChange.y;

            other.transform.position += playerChange;

            // affiche le text de la zone avec le coroutine
            if (needText)
            {
                StartCoroutine(placeNameCo());
            } 
        }
    }

    private IEnumerator placeNameCo()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }
}
