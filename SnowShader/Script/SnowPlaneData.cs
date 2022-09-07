using System;
using UnityEngine;

public class SnowPlaneData : MonoBehaviour
{
    [SerializeField] private CustomRenderTexture renderTexture;
    [SerializeField] private Material heightMat;
    private MeshRenderer meshRenderer;
    private static readonly int HeightMap = Shader.PropertyToID("_HeightMap");

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        
        CopyHeightMat();
        CopyRenderTexture();

        renderTexture.material = heightMat;
        heightMat.SetTexture(HeightMap,renderTexture);
        
        var newRenderMat = new Material(meshRenderer.material);
        newRenderMat.SetTexture(HeightMap,renderTexture);

        meshRenderer.material = newRenderMat;

        void CopyRenderTexture()
        {
            var renderTextureCopy = new CustomRenderTexture
                (renderTexture.width,renderTexture.height,renderTexture.graphicsFormat);
            
            renderTexture = renderTextureCopy;

            renderTexture.updateMode = CustomRenderTextureUpdateMode.Realtime;
            renderTexture.doubleBuffered = true;
            renderTexture.filterMode = FilterMode.Point;
        }

        void CopyHeightMat()
        {
            var newHeightMat = new Material(heightMat);
            heightMat = newHeightMat;
        }
        
        renderTexture.Initialize();
        
    }

    public Material GetSnowHeightMaterial()
    {
        return heightMat;
    }
}
