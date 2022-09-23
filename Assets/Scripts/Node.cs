using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color occupiedNodeColor;
    public Vector3 positionOffset;

    private GameObject turret;

    private Color startColor;
    private Renderer rend;

    private BuildManager buildManager;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    void OnMouseDown()
    {
        if(EventSystem.current.IsPointerOverGameObject())
            return;
            
        if (buildManager.GetTurretToBuild() == null)
            return;

        if (turret != null)
        {
            Debug.Log("Can't build it there! - TODO: Display on Screen");
            return;
        }

        // Build a turret
        GameObject turrentToBuild = buildManager.GetTurretToBuild();
        turret = Instantiate(turrentToBuild, transform.position + positionOffset, transform.rotation);
    }

    void OnMouseEnter()
    {
        if(EventSystem.current.IsPointerOverGameObject())
            return;

        if (buildManager.GetTurretToBuild() == null)
            return;

        rend.material.color = hoverColor;
        rend.material.DisableKeyword("_EMISSION");
    }
    void OnMouseExit()
    {
        rend.material.color = startColor;
        rend.material.EnableKeyword("_EMISSION");
    }

    /// <summary>
    /// Called every frame while the mouse is over the GUIElement or Collider.
    /// </summary>
    private void OnMouseOver()
    {
        if (rend.material.color == occupiedNodeColor) return;

        if (turret != null)
            rend.material.color = occupiedNodeColor;
    }
}
