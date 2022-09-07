using System;
using UnityEngine;

public class PaintScript : MonoBehaviour
{
    [SerializeField] private CustomRenderTexture renderTexture;
    [SerializeField] private Material sandMaterial;
    [SerializeField] private Gradient paintGradient;
    
    private void Start()
    {
        renderTexture.Initialize();
    }

    private void Update()
    {
        SetPaintColor();
        // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
        DrawManageUpdate();
    }

    private void SetPaintColor()
    {
        var setColor = paintGradient.Evaluate(Mathf.PingPong(Time.time, 1));
        
        sandMaterial.SetVector("_DrawColor",setColor);
    }
    
    private void DrawManageUpdate()
    {
        Vector3 drawPos;
        
        if (Input.GetMouseButton(0))
        {
            drawPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
        else
        {
            drawPos = -Vector2.one * 10000;
        }
        
        sandMaterial.SetVector("_DrawPosition",drawPos);

        
    }
    
}
