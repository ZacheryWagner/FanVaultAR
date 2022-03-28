Shader "Portal/DepthMask" {

    /// See: https://docs.unity3d.com/2019.3/Documentation/Manual/SL-SubShaderTags.html
  
    SubShader {
        // Render after normal geometry but before masked geometry and transparent objs

        Tags {"Queue" = "Geometry-10"} // Ten is a relatively arbitrary number here
  
        ColorMask 0 // Don't render color
        ZWrite On   // Write to the Z Buffer
  
        Pass {}
    }
}