using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class CreateACube : MonoBehaviour
{
    
    private const int X = 20;
    private const int Y = 20;
    private Vector3 _vector3 = new(-15,15,20);
    private GameObject prifab;
    
    
    [SerializeField] private GameObject _prifab; 
    [SerializeField] private float _timeTheCubeAppeared = 0.04f;
    [SerializeField] private float _colorChangeTimeColors = 0.4f; 
    [SerializeField] private float _colorTransfusionTime = 1f;
    private float _;
    private List<Renderer> _renderers;
    private Renderer _renderer;
    
    private void Start()
    {
        _renderers = new List<Renderer>();
        StartCoroutine(CreateACubeCoroutine());
    } 

    private IEnumerator CreateACubeCoroutine()
    {

        for (int i = 0; i < Y; i++)
        {
            for (int j = 0; j < X; j++)
            {
                prifab = Instantiate(_prifab);
                _renderer = prifab.GetComponent<Renderer>();
                _renderers.Add(_renderer);
                prifab.transform.position = _vector3;
                _vector3.x += 1.5f;
                yield return new WaitForSeconds(_timeTheCubeAppeared);
                
            }
            _vector3.y -= 1.5f;
            _vector3.x = -15;
        }

    }
    public void ChangeColor()
    {
        StartCoroutine(CoroutineColorChanges());
    }

    private IEnumerator CoroutineColorChanges()
    {
        
       var nextColor = Random.ColorHSV(0f,1f,0.8f,1f,1f,1f);
       
       
        for (int i = 0; i < _renderers.Count; i++)
        {
            var startColor = _renderers[i].material.color;
            StartCoroutine(CoroutineColorTransfusion(_renderers[i],startColor,nextColor));
            
            yield return new WaitForSeconds(_colorChangeTimeColors);
        }

        
    }

    private IEnumerator CoroutineColorTransfusion(Renderer renderer, Color startColor, Color nextColor)
    {
        var times = _colorTransfusionTime;
        var currentTime = 0f;
         
        while (currentTime<times)
        {
            renderer.material.color = Color.Lerp(startColor, nextColor, currentTime / times);
            currentTime += Time.deltaTime;
            yield return null;
        }
        
        
    }
   
    
    
}
