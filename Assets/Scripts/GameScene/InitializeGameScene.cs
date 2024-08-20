using System;
using System.Collections;
using System.Collections.Generic;
using Hero;
using UnityEngine;

public class InitializeGameScene : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    private HeroesManager _heroesManager;
    
    public void Start()
    {
        var hero = FindObjectOfType<HeroesManager>();
        if (hero != null)
        {
            _heroesManager = hero;
            SetCurrentPosition();
        }
    }

    private void SetCurrentPosition()
    {
        _heroesManager.transform.position = _startPoint.transform.position;
        _heroesManager.transform.rotation = _startPoint.transform.rotation;
    }
    
}
