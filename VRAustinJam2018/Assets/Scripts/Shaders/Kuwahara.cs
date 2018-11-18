using UnityEngine;

public class Kuwahara : MonoBehaviour
{
    [SerializeField]
    private Material _material;

    // Will be called from camera after regular rendering is done.
    public void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //_material.SetFloat("_Radius", intensity);
        Graphics.Blit(source, destination, _material);
    }
}
