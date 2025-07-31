using UnityEngine;

public class SyncLoader : MonoBehaviour
{
    [Header("Texture Application")]
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private string texturePath = "Textures/Ground";

    [Header("Audio Playback")]
    [SerializeField] private string audioPath = "Audio/Lock";

    void Start()
    {
        ApplyTexture();
        PlayAudio();
    }

    private void ApplyTexture()
    {
        Texture2D texture = Resources.Load<Texture2D>(texturePath);
        if (texture != null && targetRenderer != null)
        {
            Material mat = new Material(Shader.Find("Unlit/Texture"));
            mat.mainTexture = texture;
            targetRenderer.material = mat;
        }
        else
        {
            Debug.LogWarning("Texture or targetRenderer not found.");
        }
    }

    private void PlayAudio()
    {
        AudioClip clip = Resources.Load<AudioClip>(audioPath);
        if (clip != null)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Audio clip not found.");
        }
    }
}
