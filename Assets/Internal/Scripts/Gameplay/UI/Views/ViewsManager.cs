using System.Collections.Generic;
using Internal.Scripts.Gameplay.UI.Views;
using Internal.Scripts.Installers;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

/// <summary>
/// Controls the active UI view by showing and hiding panels, and maintains a history
/// of opened views for navigation. Provides methods for opening, closing, and
/// navigating back through UI panels, as well as scene and application controls.
/// </summary>
public class ViewsManager
{
    private readonly Canvas _viewsCanvas;

    private BaseUIPanel _currentUIPanel;

    private readonly List<BaseUIPanel> _viewsQueue = new List<BaseUIPanel>();
    private readonly DiContainer _container;

    [Inject]
    public ViewsManager(DiContainer container, [Inject(Id = UIInstaller.VIEWS_CANVAS_ID)] Canvas canvas)
    {
        _container = container;
        _viewsCanvas = canvas;
    }

    /// <summary>
    /// Hides current view and unpause game.
    /// </summary>
    public void HideCurrentContainer()
    {
        Object.Destroy(_currentUIPanel.gameObject);
        _currentUIPanel = null;
        _viewsQueue.Clear();
    }

    /// <summary>
    /// Shows new view and hides old view.
    /// </summary>
    /// <param name="prefab">View to show</param>
    public void OpenContainer(BaseUIPanel prefab)
    {
        if (_currentUIPanel != null)
        {
            Object.Destroy(_currentUIPanel.gameObject);
        }

        _currentUIPanel = _container.InstantiatePrefabForComponent<BaseUIPanel>(prefab, _viewsCanvas.transform);

        _viewsQueue.Remove(prefab);
        _viewsQueue.Add(prefab);
    }

    /// <summary>
    /// Open previous view, can be used in 'Back' buttons.
    /// </summary>
    public void OpenPreviousContainer()
    {
        if (_viewsQueue.Count < 2)
        {
            HideCurrentContainer();
            return;
        }

        _viewsQueue.Remove(_viewsQueue[^1]);
        OpenContainer(_viewsQueue[^1]);
    }
}