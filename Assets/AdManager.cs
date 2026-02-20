using UnityEngine;
using UnityEngine.Advertisements;

/// <summary>
/// Unity Ads 4.x 광고 매니저 - 사망 후 전면 광고(Interstitial) 표시
///
/// [설정 방법]
/// 1. 씬에 빈 GameObject 생성 → 이름 "AdManager"
/// 2. 이 스크립트를 컴포넌트로 추가
/// 3. 출시 전에 Test Mode 체크 해제
/// </summary>
public class AdManager : MonoBehaviour,
    IUnityAdsInitializationListener,
    IUnityAdsLoadListener,
    IUnityAdsShowListener
{
    [SerializeField] string _androidGameId = "6049757";

    // Unity Ads 4.x 기본 전면광고 Ad Unit ID
    [SerializeField] string _adUnitId = "Interstitial_Android";

    // 테스트 모드: 개발 중에는 true, 출시 전에 false로 변경
    [SerializeField] bool _testMode = true;

    public static AdManager instance;

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
        // 초기화 완료 → 광고 미리 로드
        Advertisement.Load(_adUnitId, this);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogWarning("Unity Ads 초기화 실패: " + error + " - " + message);
    }

    // ─── IUnityAdsLoadListener ────────────────────────────────────────────

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // 광고 로드 완료 (별도 처리 불필요)
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.LogWarning("Unity Ads 로드 실패: " + error);
        // 광고 로드 실패 시 바로 게임오버 패널 표시
        if (GameManager.instance != null)
            GameManager.instance.ShowTryAgain();
    }

    // ─── 광고 표시 ────────────────────────────────────────────────────────

    /// <summary>
    /// 광고 표시. 게임오버 시 호출됨.
    /// </summary>
    public void ShowInterstitialAd()
    {
        Advertisement.Show(_adUnitId, this);
    }

    // ─── IUnityAdsShowListener ────────────────────────────────────────────

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState completionState)
    {
        // 광고 종료 → 게임오버 패널 표시 + 다음 광고 미리 로드
        if (GameManager.instance != null)
            GameManager.instance.ShowTryAgain();
        Advertisement.Load(_adUnitId, this);
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.LogWarning("Unity Ads 표시 실패: " + error);
        // 광고 표시 실패 시 바로 게임오버 패널 표시
        if (GameManager.instance != null)
            GameManager.instance.ShowTryAgain();
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
}
