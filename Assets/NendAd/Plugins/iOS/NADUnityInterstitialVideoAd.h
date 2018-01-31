//
//  NADUnityInterstitialVideoAd.h
//  Unity-iPhone
//

#import <NendAd/NADInterstitialVideo.h>
#import <NendAd/NADVideo.h>

#import "NADUnityVideoAd.h"

extern "C" {
    NendIOSVideoAdPtr _CreateInterstitialVideoAd(const char* spotId, const char* apiKey);
    void _LoadInterstitialVideoAd(NendUnityVideoAdPtr unityPtr, NendIOSVideoAdPtr iOSPtr, NendUnityVideoAdNormalCallback callback, NendUnityVideoAdErrorCallback errorCallback);
    bool _IsLoadedInterstitial(NendIOSVideoAdPtr iOSPtr);
    void _ShowInterstitialVideoAd(NendIOSVideoAdPtr iOSPtr);
    void _ReleaseInterstitialVideoAd(NendIOSVideoAdPtr iOSPtr);
    void _SetInterstitialMediationName (NendIOSVideoAdPtr iOSPtr, const char* mediationName);
    void _SetInterstitialUserId (NendIOSVideoAdPtr iOSPtr, const char* userId);
    void _SetInterstitialOutputLog (NendIOSVideoAdPtr iOSPtr, BOOL isOutputLog);
    void _AddFallbackFullboard (NendIOSVideoAdPtr iOSPtr, const char* spotId, const char* apiKey);
    void _DisposeInterstitialVideoAd(NendIOSVideoAdPtr iOSPtr);
}
