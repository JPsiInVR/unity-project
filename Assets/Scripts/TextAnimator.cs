using UnityEngine;
using TMPro;

public class TextAnimator : MonoBehaviour
{
    [SerializeField]
    private Material material;

    [SerializeField]
    private Vector4 MaxOffset;
    [SerializeField]
    private float FrameTime;
    [SerializeField]
    private int FrameCount;
    [SerializeField]
    private Vector4 NoiseScale;
    [SerializeField]
    private bool doodleEnabled;

    private void Update()
    {
        material.SetVector("_DoodleMaxOffset", MaxOffset);
        material.SetFloat("_DoodleFrameTime", FrameTime);
        material.SetInt("_DoodleFrameCount", FrameCount);
        material.SetVector("_DoodleNoiseScale", NoiseScale);

        if(doodleEnabled)
        {
            material.EnableKeyword("DOODLE_ON");
        }
        else
        {
            material.DisableKeyword("DOODLE_ON");
        }
    }
}
