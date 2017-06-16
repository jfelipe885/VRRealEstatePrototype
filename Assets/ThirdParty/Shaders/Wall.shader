
//https://forum.unity3d.com/threads/resolved-trying-to-make-holes-in-walls-with-stencil-buffer-shaders.429746/

Shader "ThirdParty/Wall" {
  Properties{
    _Color("Main Color", Color) = (1,1,1,1)
    _MainTex("Base (RGB)", 2D) = "white" {}
  }
    Category{
    Lighting Off
    ZWrite on
    Cull Back
    SubShader{

    Stencil{
    Ref 1
    Comp notequal
    Pass Zero
  }

    Tags{
    "Queue" = "Geometry"
    "LightMode" = "Always"
    "RenderType" = "Opaque"
    "IgnoreProjector" = "True"
  }
    Pass{
    SetTexture[_MainTex]{
    constantColor[_Color]
    Combine texture * constant, texture * constant
  }
  }
  }
  }
}