using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RotatingCircle : MonoBehaviour
{

    private int levelIndex;
    private Transform _transform;
    [SerializeField] private TextMeshProUGUI levelIndexText;
    private float speed = 0;

    public void Initialize()
    {
        _transform = transform;
    }

    public void SetLevel(int _levelIndex, float _speed)
    {
        levelIndex = _levelIndex;
        UpdateText();
        speed = _speed;
    }

    private void UpdateText()
    {
        levelIndexText.text = levelIndex.ToString();    
    }

    public void CallUpdate()
    {
        _transform.Rotate(new Vector3(0,0, speed * Time.deltaTime));
    }


}
