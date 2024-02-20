using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class VisibilityFeature : ScriptableRendererFeature
{
    public VisibilityPassSettings visibilityPassSettings;

    private VisibilityPass visibilityPass;

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if(!renderingData.cameraData.isSceneViewCamera) {
            renderer.EnqueuePass(visibilityPass);
        }
    }

    public override void Create()
    {
        visibilityPass = new VisibilityPass(visibilityPassSettings);
    }
}
