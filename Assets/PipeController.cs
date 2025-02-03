using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    PipesSpawnerController _spawnerController;
    public float ScrollSpeed { get; set; }

    // Update is called once per frame
    float _leftCamEdge;
    private void Start()
    {
        Camera cam = Camera.main;
        float orthoWidth = cam.orthographicSize * cam.aspect;
        _leftCamEdge = cam.transform.position.x - orthoWidth;
    }
    public void SetSpawnerController(PipesSpawnerController spawnerController)
    {
        _spawnerController = spawnerController;
    }
    void Update()
    {
        if (_spawnerController.CanMove)
        {
            Vector3 position = transform.position;
            position.x -= ScrollSpeed * Time.deltaTime;
            transform.position = position;

            if (position.x < (_leftCamEdge - 1f))
                Destroy(gameObject);
        }
        
    }
}
