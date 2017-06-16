
//https://forum.unity3d.com/threads/resolved-trying-to-make-holes-in-walls-with-stencil-buffer-shaders.429746/

Shader "ThirdParty/WallOccluder" 
{
  Category
  {
    Lighting Off
    Cull Back
    zwrite off

    SubShader
    {
      Colormask 0
      Stencil
      {
        Ref 1
        Comp always
        Pass replace
       }

      Tags
      {
        "Queue" = "Geometry"
        "LightMode" = "Always"
        "RenderType" = "Opaque"
        "IgnoreProjector" = "True"
      }

      Pass
      {
      }
    }
  }
}