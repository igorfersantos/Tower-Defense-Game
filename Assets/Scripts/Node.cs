using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color occupiedNodeColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [Header("Optional")]
    public GameObject turret;

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
        buildManager = BuildManager.Instance;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (turret != null)
        {
            Debug.Log("Can't build it there! - TODO: Display on Screen");
            return;
        }

        buildManager.BuildTurretOn(this);
        rend.material.color = occupiedNodeColor;
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        rend.material.DisableKeyword("_EMISSION");
        rend.material.EnableKeyword("_SPECULARHIGHLIGHTS_OFF");
        rend.material.SetFloat("_SpecularHighlights",0f);

        if (turret != null)
        {
            rend.material.color = occupiedNodeColor;
        }
        else if (!buildManager.HasEnoughMoneyToBuild)
        {
            rend.material.color = notEnoughMoneyColor;
        }
        else
        {
            rend.material.color = hoverColor;
        }
    }
    void OnMouseExit()
    {
        rend.material.color = startColor;
        rend.material.EnableKeyword("_EMISSION");
        rend.material.DisableKeyword("_SPECULARHIGHLIGHTS_OFF");
        rend.material.SetFloat("_SpecularHighlights",1f);
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
}
