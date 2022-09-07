using System;
using UnityEngine;

public class SnowBrush : MonoBehaviour
{
    [SerializeField] private CustomRenderTexture snowHeightMapTexture;
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

        Material snowHeightMapMat;
        
        if (Input.GetMouseButton(0))
        {
            Ray checkRay = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(checkRay, out RaycastHit hit))
            {
                drawPosition = hit.textureCoord;

                snowHeightMapMat = hit.collider.gameObject.GetComponent<SnowPlaneData>().GetSnowHeightMaterial();
            }
            else return;
        }
        else return;
        
        snowHeightMapMat.SetVector(DrawPosition,drawPosition);

        const float brushDivideCoof = 100;
        snowHeightMapMat.SetFloat(BrushSize,brushSize / brushDivideCoof);
    }
    
    
}
