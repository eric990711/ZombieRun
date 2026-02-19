using UnityEngine;
using UnityEngine.Advertisements;

/// <summary>
/// Unity Ads 광고 매니저 - 사망 후 전면 광고(Interstitial) 표시
///
/// [설정 방법]
/// 1. Unity Dashboard (https://dashboard.unity3d.com) 에서 프로젝트 생성
/// 2. Monetization > Unity Ads 활성화
/// 3. Android Game ID를 아래 _androidGameId에 입력
/// 4. 출시 전에 _testMode = false 로 변경
/// </summary>
public class AdManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] string _androidGameId = "6049757";

    // 전면 광고 placement ID (Unity Ads 기본값)
    [SerializeField] string _placementId = "video";

    // 테스트 모드: 개발 중에는 true, 출시 전에 false로 변경
    [SerializeField] bool _testMode = true;

    public static AdManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAds();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializeAds()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(_androidGameId, _testMode);
    }

    /// <summary>
    /// 광고 표시. 광고가 준비되지 않으면 바로 게임오버 패널 표시.
    /// </summary>
    public void ShowInterstitialAd()
    {
        if (Advertisement.IsReady(_placementId))
        {
            Advertisement.Show(_placementId);
        }
        else
        {
            // 광고가 준비되지 않은 경우 바로 게임오버 패널 표시
            GameManager.instance.ShowTryAgain();
        }
    }

    // ─── IUnityAdsListener 콜백 ───────────────────────────────────────────

    public void OnUnityAdsReady(string placementId)
    {
        // 광고 로드 완료 (별도 처리 불필요)
    }

    public void OnUnityAdsDidError(string message)
    {
        // 광고 오류 → 게임오버 패널 표시
        Debug.LogWarning("Unity Ads error: " + message);
        if (GameManager.instance != null)
            GameManager.instance.ShowTryAgain();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // 광고 시작 (별도 처리 불필요)
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // 광고 종료 후 게임오버 패널 표시
        if (GameManager.instance != null)
            GameManager.instance.ShowTryAgain();
    }
}
