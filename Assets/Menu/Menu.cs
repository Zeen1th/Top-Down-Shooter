using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class Menu : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject loadingPanel;

    [Header("UI Elements")]
    [SerializeField] private Button startButton;
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private TextMeshProUGUI loadingText; // in percentage

    [Header("Scene Reference")]
    [SerializeField] private AssetReference sceneReference;

    private void Start()
    {
        menuPanel.SetActive(true);
        loadingPanel.SetActive(false);
    }

    public void OnStartButtonClicked()
    {
        menuPanel.SetActive(false);
        loadingPanel.SetActive(true);

        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperationHandle<SceneInstance> handle = sceneReference.LoadSceneAsync();

        while (!handle.IsDone)
        {
            float progress = handle.PercentComplete;
            loadingSlider.value = progress;
            loadingText.text = Mathf.RoundToInt(progress * 100f) + "%";
            yield return null;
        }
    }
}
