using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceBuilding : MonoBehaviour, IDragHandler, IDropHandler, IBeginDragHandler
{
    public GameObject Prefab;

    public PlaceBuildingData PlaceBuildingData;

    public TextMeshProUGUI Cost;

    public float MaxDistance = 8.0f;

    private GameObject _dragged;
    
    private GameObject _lineDragged;

    private int _cost;

    // Start is called before the first frame update
    void Start()
    {
        _cost = Int32.Parse(Cost.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public int GetBuildingLayer()
    {
        return LayerMask.GetMask(new string[] { "Buildings", "Resources" });
    }

    public void OnDrag(PointerEventData eventData)
    {
        var newPos = PlaceBuildingData.MainCamera.ScreenToWorldPoint(eventData.position);
        newPos.z = 0.0f;
        newPos = Simulator.Snap(newPos);

        var overlaps = Physics2D.OverlapBoxAll(new Vector2(newPos.x, newPos.y), Vector2.one * 0.5f,0.0f, GetBuildingLayer());
        if (overlaps.Length > 0)
        {
            return;
        }

        if (_dragged != null)
        {
            _dragged.transform.position = newPos;
        }

        if (_lineDragged != null)
        {
            var scale = _lineDragged.transform.localScale;

            var vx = PlaceBuildingData.MainBase.transform.position.y - _dragged.transform.position.y;
            var vy = PlaceBuildingData.MainBase.transform.position.x - _dragged.transform.position.x;
            var v = new Vector2(vx, vy);

            _lineDragged.SetActive(v.magnitude <= MaxDistance);

            var rotation = Mathf.Rad2Deg * Mathf.Atan2(-vy, vx);
            _lineDragged.transform.rotation = Quaternion.Euler(rotation * Vector3.forward);
            _lineDragged.transform.localScale = new Vector3(_lineDragged.transform.localScale.x, v.magnitude,
                _lineDragged.transform.localScale.z);
            _lineDragged.transform.position = new Vector3(PlaceBuildingData.MainBase.transform.position.x, PlaceBuildingData.MainBase.transform.position.y) - new Vector3(0.5f * v.y, 0.5f * v.x, 0.0f);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (_dragged == PlaceBuildingData.ErrorPrefab)
        {
            PlaceBuildingData.ErrorPrefab.SetActive(false);
        }
        _dragged = null;
        _lineDragged = null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (PlaceBuildingData.Simulator.TryToBuy(_cost))
        {
            _dragged = Instantiate(Prefab);
            _lineDragged = Instantiate(PlaceBuildingData.Line);
        }
        else
        {
            _dragged = PlaceBuildingData.ErrorPrefab;
            _dragged.SetActive(true);
        }
    }
}
