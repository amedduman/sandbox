using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEffectController : MonoBehaviour
{
    // [SerializeField] Shader _shader;
    [SerializeField] Material _mat;
    RenderTexture rndTex;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if(rndTex == null)
        {
            rndTex = new RenderTexture(src.width, src.height, 0, src.format);
        }

        // RenderTexture rndTexture = RenderTexture.GetTemporary(
        //     src.width, src.height, 0, src.format
        // );

        Graphics.Blit(src, rndTex, _mat, 0);
        Graphics.Blit(rndTex, dest);

        // RenderTexture.ReleaseTemporary(rndTexture);
    }
}
