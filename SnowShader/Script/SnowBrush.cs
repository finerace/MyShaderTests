using System;
using UnityEngine;

public class SnowBrush : MonoBehaviour
{
    [SerializeField] private CustomRenderTexture snowHeightMapTexture;
    [SerializeField] private Material snowHeightMapMat;
    private Camera mainCamera;
    [SerializeField] private float brushSize = 1;

    private static readonly int DrawPosition = Shader.PropertyToID("_DrawPosition");
    private static readonly int BrushSize = Shader.PropertyToID("_BrushSize");

    private void Awake()
    {
        mainCamera = Camera.main;
        
        snowHeightMapTexture.Initialize();
    }

    private void Update()
    {
        BrushManage();
    }

    private void BrushManage()
    {
        var drawPosition = -Vector4.one;

        if (Input.GetMouseButton(0))
        {
            Ray checkRay = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(checkRay, out RaycastHit hit))
            {
                drawPosition = hit.textureCoord;
            }
        }
        
        snowHeightMapMat.SetVector(DrawPosition,drawPosition);

        const float brushDivideCoof = 100;
        snowHeightMapMat.SetFloat(BrushSize,brushSize / brushDivideCoof);
    }
    
    
}
