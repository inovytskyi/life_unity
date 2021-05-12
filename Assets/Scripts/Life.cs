using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class Life : MonoBehaviour
{
    [SerializeField] private Tile livetile;

    [SerializeField] private Tilemap _tilemap;

    private bool _pause = true;

    public UnityEvent<bool> pauseEvent = new UnityEvent<bool>();

    public bool Pause { get => _pause;}

    // Start is called before the first frame update
    void Start()
    {
        //_tilemap = GetComponent("Tilemap") as Tilemap;
        //_tilemap.SetTile(new Vector3Int(-1, 0, 0), livetile);
        //_tilemap.SetTile(new Vector3Int(0, 0, 0), livetile);
        //_tilemap.SetTile(new Vector3Int(1, 0, 0), livetile);        

    }

    public void TogglePause()
    {
        _pause = !_pause;
        pauseEvent.Invoke(_pause);
    }

    public Vector3 getCenter()
    {
        return _tilemap.cellBounds.center;
    }

    public int getHeight()
    {
        return _tilemap.cellBounds.yMax - _tilemap.cellBounds.yMin;
    }
    
    public int getWidth()
    {
        return _tilemap.cellBounds.xMax - _tilemap.cellBounds.xMin;
    }

    // Update is called once per frame
    void Update()   
    {
        if (_pause)
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {

                Vector3Int pos = _tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                TileBase clickedTile = _tilemap.GetTile(pos);
                if (clickedTile == null)
                {
                    _tilemap.SetTile(pos, livetile);
                }
                else
                {
                    _tilemap.SetTile(pos, null);
                }



            }
        } 
    }

    public void Tick()
    {
        List<Vector3Int> toCreate = new List<Vector3Int>();
        List<Vector3Int> toDestroy = new List<Vector3Int>();
        int minX = _tilemap.cellBounds.xMin - 1;
        int maxX = _tilemap.cellBounds.xMax + 1;
        int minY = _tilemap.cellBounds.yMin - 1;
        int maxY = _tilemap.cellBounds.yMax + 1;
        for(int x=minX; x<=maxX; x++)
        {
            for(int y=minY; y<=maxY; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                int neighbours = getNeighbours(pos);

                if (_tilemap.GetTile(pos))
                {
                    if (!(neighbours == 2 || neighbours == 3))
                        toDestroy.Add(pos);
            
                }
                else
                {
                    if (neighbours == 3)
                    {
                        toCreate.Add(pos);
                    }
                }
            }
        }
        foreach(Vector3Int pos in toDestroy)
        {
            _tilemap.SetTile(pos, null);
        }
        foreach(Vector3Int pos in toCreate)
        {
            _tilemap.SetTile(pos, livetile);
        }


    }

    private int getNeighbours(Vector3Int pos)
    {
        int count = 0;
        if (_tilemap.GetTile(pos - new Vector3Int(-1, +1, 0)))
            count++;
        if (_tilemap.GetTile(pos - new Vector3Int(-1, 0, 0)))
            count++;
        if (_tilemap.GetTile(pos - new Vector3Int(-1, -1, 0)))
            count++;
        if (_tilemap.GetTile(pos - new Vector3Int(+1, +1, 0)))
            count++;
        if (_tilemap.GetTile(pos - new Vector3Int(+1, 0, 0)))
            count++;
        if (_tilemap.GetTile(pos - new Vector3Int(+1, -1, 0)))
            count++;
        if (_tilemap.GetTile(pos - new Vector3Int(0, 1, 0)))
            count++;
        if (_tilemap.GetTile(pos - new Vector3Int(0, -1, 0)))
            count++;
        
        return count;
    }
    
    
}
