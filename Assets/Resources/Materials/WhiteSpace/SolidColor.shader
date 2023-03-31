Shader "Solid Color"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1)
    }

    SubShader
    {
        Color [_Color]
        Pass
        {
            Cull Off
            //ZTest [Always]
           // ZWrite Off

            
        }
    }
}