using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesSpawnerController : MonoBehaviour
{
    [SerializeField] GameObject _gameController;
    [SerializeField] GameObject _pipe;
    public float ScrollSpeed = 1f;
    public float MaxHeight = 2.5f;
    public float MinHeight = -1.25f;
    public float SpawnInterval = 1f;
    public bool CanMove {get;set;} = true;
    public int PipesCount {get;set;}
    private void Start()
    {
    }
    float _accumulator;
    private void Update()
    {
        GameController gameContr = _gameController.GetComponent<GameController>();
        CanMove = !gameContr.IsGameOver;

        if (CanMove && PipesCount > 0 && gameContr.IsGameBegins)
        {
            _accumulator += Time.deltaTime;

            if (_accumulator >= SpawnInterval)
            {
                _accumulator -= SpawnInterval;
                SpawnPipe();
                PipesCount++;
            }
        }

        if (PipesCount <= 0 && gameContr.IsGameBegins)
        {
            _accumulator = 0f;
            SpawnPipe();
            PipesCount++;
        }
    }
    void SpawnPipe()
    {
        GameObject pipe = Instantiate(_pipe, transform);
        PipeController pipeContr = pipe.GetComponent<PipeController>();
        pipeContr.ScrollSpeed = ScrollSpeed;
        pipeContr.SetSpawnerController(this);

        Vector3 pipePos = pipe.GetComponent<Transform>().transform.position;
        pipePos.y = Random.Range(MinHeight, MaxHeight);
        pipe.transform.position = pipePos;
    }
}
