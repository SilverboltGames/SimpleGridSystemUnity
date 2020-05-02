using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class SilverGrid
{
    public int _height { get; private set; }
    public int _width { get; private set; }
    public float _cellSize { get; private set; }
    public Vector3 _origin { get; private set; } = Vector3.zero;
    private int[,] _cellValue;
    public SilverGrid(int width, int height, float cellSize = 1, Vector3 origin = default)
    {
        _height = height;
        _width = width;
        _origin = origin;
        _cellSize = cellSize;
        _cellValue = new int[width, height];
    }

    public void DrawGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Debug.DrawLine(_origin + new Vector3(x, y) * _cellSize, _origin + new Vector3(x, y) * _cellSize + new Vector3(_cellSize, 0));
                Debug.DrawLine(_origin + new Vector3(x, y) * _cellSize, _origin + new Vector3(x, y) * _cellSize + new Vector3(0, _cellSize));
            }
        }
        Debug.DrawLine(_origin + new Vector3(_width, 0) * _cellSize, (_origin + new Vector3(_width, _height) * _cellSize));
        Debug.DrawLine(_origin + new Vector3(0, _height) * _cellSize, (_origin + new Vector3(_width, _height) * _cellSize));
    }

    public Vector3 CelltoWorld(int x, int y)
    {
        float worldX = _origin.x + (x * _cellSize) + _cellSize/2;
        float worldY = _origin.y + (y * _cellSize) + _cellSize/2;
        return new Vector3(worldX, worldY, _origin.z);
    }

    /// <summary>
    /// Make sure to use Vector2Int and not Vector2
    /// </summary>
    public Vector2Int WorldtoCell(Vector3 position)
    {
        int x = Mathf.FloorToInt((position.x - _origin.x)/_cellSize);
        int y = Mathf.FloorToInt((position.y - _origin.y) / _cellSize);
        return new Vector2Int(x, y);
    }

    
    public int GetCellValue(int x, int y)
    {
        return _cellValue[x, y];
    }
    public int GetCellValue(Vector3 position)
    {
        Vector2Int cell = WorldtoCell(position);
        return GetCellValue(cell.x, cell.y);
    }


    public void SetCellValue(int x, int y, int value)
    {
        _cellValue[x, y] = value;
    }
    public void SetCellValue(Vector3 position, int value)
    {
        Vector2Int cell = WorldtoCell(position);
        SetCellValue(cell.x, cell.y, value);
    }


    public Rect GetCellRectWorld(int x, int y)
    {
        float minX = _origin.x + (x * _cellSize);
        float minY = _origin.y + (y * _cellSize);
        return new Rect(minX, minY, _cellSize, _cellSize);
    }
    public Rect GetCellRectWorld(Vector3 position)
    {
        Vector2Int cell = WorldtoCell(position);
        return GetCellRectWorld(cell.x, cell.y);
    }
    public Rect GetCellRectGUI(int x, int y)
    {
        float minX = _origin.x + (x * _cellSize);
        float minY = _origin.y + ((y + 1) * _cellSize);
        Vector2 GUICoordinates = HandleUtility.WorldToGUIPoint(new Vector2(minX, minY));
        float ppu = (Screen.height / 2) / Camera.main.orthographicSize;
        float boxSize = (_cellSize * ppu) * .99f;
        return new Rect(GUICoordinates.x, GUICoordinates.y, boxSize, boxSize);
    }
    public Rect GetCellRectGUI(Vector3 position)
    {
        Vector2Int cell = WorldtoCell(position);
        return GetCellRectGUI(cell.x, cell.y);
    }

    public bool CheckCellExistance(int x, int y)
    {
        bool xWithinBounds = (x >= 0 && x <= _width) ? true : false;
        bool yWithinBounds = (y >= 0 && y <= _height) ? true : false;
        return xWithinBounds && yWithinBounds; //return true if both are true
    }
    public bool CheckCellExistance(Vector3 position)
    {
        Vector2Int cell = WorldtoCell(position);
        return CheckCellExistance(cell.x, cell.y);
    }
}