using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public partial class CursorManager : Singleton<CursorManager>
{
    // 凝视射线在全息对象上时显示的光标对象  
    [Tooltip("Drag the Cursor object to show when it hits a hologram.")]
    public GameObject CursorOnHolograms;
    // 凝视射线离开全息对象时显示的光标对象  
    [Tooltip("Drag the Cursor object to show when it does not hit a hologram.")]
    public GameObject CursorOffHolograms;

    [Tooltip("Distance, in meters, to offset the cursor from the collision point.")]
    public float DistanceFromCollision = 0.01f;

    protected override void Awake()
    {
        // 当未设定光标对象时直接返回  
        if (CursorOnHolograms == null || CursorOffHolograms == null)
        {
            return;
        }

        // Hide the Cursors to begin with.  
        CursorOnHolograms.SetActive(false);
        CursorOffHolograms.SetActive(false);
    }

    void LateUpdate()
    {
        if (GazeManager.Instance == null || CursorOnHolograms == null || CursorOffHolograms == null)
        {
            return;
        }
        // 当凝视射线在全息对象上及离开全息对象时，分别显示不同的光标对象，以此来进行区分  
        if (GazeManager.Instance.HitObject)
        {
            CursorOnHolograms.SetActive(true);
            CursorOffHolograms.SetActive(false);
        }
        else
        {
            CursorOffHolograms.SetActive(true);
            CursorOnHolograms.SetActive(false);
        }

        // 计算并安置光标  
        // Place the cursor at the calculated position.  
        this.gameObject.transform.position = GazeManager.Instance.HitPosition + GazeManager.Instance.GazeNormal * DistanceFromCollision;

        // Orient the cursor to match the surface being gazed at.  
        gameObject.transform.up = GazeManager.Instance.GazeNormal;
    }
}
