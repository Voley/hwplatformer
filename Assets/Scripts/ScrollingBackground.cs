using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private Transform _background1;
    [SerializeField] private Transform _background2;
    [SerializeField] private float _scrollSpeed;
    [SerializeField] private float _itemVerticalSize;

    void Update()
    {
        float moveByY = _scrollSpeed * Time.deltaTime;

        _background1.Translate(new Vector3(0, moveByY, 0));
        _background2.Translate(new Vector3(0, moveByY, 0));

        if (_background2.position.y <= 0)
        {
            _background1.position = new Vector3(0, _itemVerticalSize + _background2.position.y, 0);
            (_background1, _background2) = (_background2,  _background1);
        }
    }
}
