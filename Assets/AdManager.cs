using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour,
    IUnityAdsInitializationListener,
    IUnityAdsLoadListener,
    IUnityAdsShowListener
{
    [SerializeField] string _androidGameId = "6049757";
    [SerializeField] string _adUnitId = "Interstitial_Android";
    [SerializeField] bool _testMode = true;

    public static AdManager instance;

    bool _adLoaded = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Advertisement.Initialize(_androidGameId, _testMode, this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ─── IUnityAdsInitializationListener ─────────────────────────────────

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads 초기화 완료 → 광고 로드 시작");
        Advertisement.Load(_adUnitId, this);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogWarning("Unity Ads 초기화 실패: " + error + " - " + message);
    }

    // ─── IUnityAdsLoadListener ────────────────────────────────────────────

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Unity Ads 로드 완료: " + adUnitId);
        _adLoaded = true;
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.LogWarning("Unity Ads 로드 실패: " + error + " - " + message + " → 3초 후 재시도");
        _adLoaded = false;
        Invoke("RetryLoad", 3f);
    }

    void RetryLoad()
    {
        Debug.Log("Unity Ads 재시도 로드");
        Advertisement.Load(_adUnitId, this);
    }

    // ─── 광고 표시 ────────────────────────────────────────────────────────

    public void ShowInterstitialAd()
    {
        if (_adLoaded)
        {
            Debug.Log("광고 표시");
            _adLoaded = false;
            Advertisement.Show(_adUnitId, this);
        }
        else
        {
            Debug.LogWarning("광고 미로드 상태 → TryAgain 바로 표시");
            if (GameManager.instance != null)
                GameManager.instance.ShowTryAgain();
        }
    }

    // ─── IUnityAdsShowListener ────────────────────────────────────────────

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState completionState)
    {
        if (GameManager.instance != null)
            GameManager.instance.ShowTryAgain();
        Advertisement.Load(_adUnitId, this);
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.LogWarning("Unity Ads 표시 실패: " + error + " - " + message);
        if (GameManager.instance != null)
            GameManager.instance.ShowTryAgain();
        Advertisement.Load(_adUnitId, this);
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
}
