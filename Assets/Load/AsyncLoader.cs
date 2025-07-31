using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AsyncLoader : MonoBehaviour
{
    void Start()
    {
        // Load texture
        Addressables.LoadAssetAsync<Texture2D>("Textures/Rock Wall").Completed += OnTextureLoaded;

        // Load and play audio
        Addressables.LoadAssetAsync<AudioClip>("Audio/Open").Completed += handle =>
        {
            AudioSource audio = new GameObject("AsyncAudio").AddComponent<AudioSource>();
            audio.clip = handle.Result;
            audio.Play();
        };
    }

    void OnTextureLoaded(AsyncOperationHandle<Texture2D> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            quad.name = "AsyncTextureQuad";
            quad.transform.position = new Vector3(0, 1, 3);

            Material mat = new Material(Shader.Find("Unlit/Texture"));
            mat.mainTexture = handle.Result;

            quad.GetComponent<Renderer>().material = mat;
        }
        else
        {
            Debug.LogError("Failed to load texture from Addressables.");
        }
    }
}
