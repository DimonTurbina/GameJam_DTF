using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : Menu
{
    bool _active;
    public Canvas canvas;
    public ZombieManager manager;
    private void Start()
    {
      
        _active = false;
        manager.isPause = _active;
        canvas.enabled = _active;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _active = !_active;
            manager.isPause = _active;
            canvas.enabled = _active;
        }
    }
}
