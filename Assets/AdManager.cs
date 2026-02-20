using UnityEngine;

/// <summary>
/// LevelPlay (Ads Mediation) 광고 매니저 - 사망 후 전면 광고(Interstitial) 표시
///
/// [설정 방법]
/// 1. 씬에 빈 GameObject 생성 → 이름 "AdManager"
/// 2. 이 스크립트를 컴포넌트로 추가
/// 3. Inspector에서 _appKey에 LevelPlay 대시보드의 App Key 입력
///    (앱 목록 → 앱 선택 → App Key 복사)
/// 4. 출시 전에 IronSource.Agent.setConsent(true) 등 개인정보 설정 확인
/// </summary>
public class AdManager : MonoBehaviour
{
    // LevelPlay 대시보드(platform.ironsrc.com) → Apps → 앱 선택 → App Key
    [SerializeField] string _appKey = "여기에_App_Key_입력";

    public static AdManager instance;

    // ─── 초기화 ──────────────────────────────────────────────────────────

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
        // 이벤트 먼저 등록 후 초기화
        IronSourceEvents.onSdkInitializationCompletedEvent += OnSdkInitialized;
        IronSource.Agent.init(_appKey, IronSourceAdUnits.INTERSTITIAL);
    }

    void OnSdkInitialized()
    {
        // 초기화 완료 → 광고 미리 로드
        IronSource.Agent.loadInterstitial();
    }

    // ─── 이벤트 등록 / 해제 ──────────────────────────────────────────────

    void OnEnable()
    {
        IronSourceInterstitialEvents.onAdReadyEvent        += OnInterstitialReady;
        IronSourceInterstitialEvents.onAdLoadFailedEvent   += OnInterstitialLoadFailed;
        IronSourceInterstitialEvents.onAdClosedEvent       += OnInterstitialClosed;
        IronSourceInterstitialEvents.onAdShowFailedEvent   += OnInterstitialShowFailed;
    }

    void OnDisable()
    {
        IronSourceInterstitialEvents.onAdReadyEvent        -= OnInterstitialReady;
        IronSourceInterstitialEvents.onAdLoadFailedEvent   -= OnInterstitialLoadFailed;
        IronSourceInterstitialEvents.onAdClosedEvent       -= OnInterstitialClosed;
        IronSourceInterstitialEvents.onAdShowFailedEvent   -= OnInterstitialShowFailed;
    }

    // ─── 이벤트 핸들러 ───────────────────────────────────────────────────

    void OnInterstitialReady(IronSourceAdInfo adInfo)
    {
        // 광고 로드 완료 (별도 처리 불필요)
    }

    void OnInterstitialLoadFailed(IronSourceError error)
    {
        Debug.LogWarning("전면광고 로드 실패: " + error);
        // 광고 로드 실패 시 바로 게임오버 패널 표시
        if (GameManager.instance != null)
            GameManager.instance.ShowTryAgain();
    }

    void OnInterstitialClosed(IronSourceAdInfo adInfo)
    {
        // 광고 종료 → 게임오버 패널 표시 + 다음 광고 미리 로드
        if (GameManager.instance != null)
            GameManager.instance.ShowTryAgain();
        IronSource.Agent.loadInterstitial();
    }

    void OnInterstitialShowFailed(IronSourceError error, IronSourceAdInfo adInfo)
    {
        Debug.LogWarning("전면광고 표시 실패: " + error);
        // 광고 표시 실패 시 바로 게임오버 패널 표시
        if (GameManager.instance != null)
            GameManager.instance.ShowTryAgain();
    }

    // ─── 광고 표시 ───────────────────────────────────────────────────────

    /// <summary>
    /// 광고 표시. 게임오버 시 호출됨.
    /// </summary>
    public void ShowInterstitialAd()
    {
        if (IronSource.Agent.isInterstitialReady())
        {
            IronSource.Agent.showInterstitial();
        }
        else
        {
            // 광고 준비 안 됐으면 바로 게임오버 패널 표시
            if (GameManager.instance != null)
                GameManager.instance.ShowTryAgain();
        }
    }

    // ─── 앱 일시정지 처리 (IronSource 필수) ─────────────────────────────

    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }
}
