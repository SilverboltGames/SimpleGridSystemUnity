using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    SilverGrid _grid;
	void Start()
	{
        _grid = new SilverGrid(10, 10, 1, new Vector3(-5, -5));
    }
    private void Update()
    {
        //_grid.DrawGrid();
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (_grid.CheckCellExistance(mouseWorldPos))
            {
                int value = (_grid.GetCellValue(mouseWorldPos) == 1) ? 0 : 1;
                _grid.SetCellValue(mouseWorldPos, value);
            }
        }
    }
    private void OnGUI()
    {
        for (int x = 0; x < _grid._width; x++)
        {
            for (int y = 0; y < _grid._height; y++)
            {
                GUI.Box(_grid.GetCellRectGUI(x, y), _grid.GetCellValue(x, y).ToString());
            }
        }
    }
}