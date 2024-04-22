using DevelopmentKit.Base.Component;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraComponent : MonoBehaviour, IComponent
{
    private Animator _animator;
    public Animator animator => _animator;
    
    public void Initialize(ComponentContainer componentContainer)
    {
        _animator = GetComponent<Animator>();        
    }
}
