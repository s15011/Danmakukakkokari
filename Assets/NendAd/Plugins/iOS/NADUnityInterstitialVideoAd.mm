//
//  NADUnityInterstitialVideoAd.m
//  Unity-iPhone
//

#import "NADUnityVideoAd.h"
#import "NADUnityInterstitialVideoAd.h"
#import "NADUnityGlobal.h"

extern UIViewController* UnityGetGLViewController();

//==============================================================================

@interface NendIOSInterstitialVideoAd : NendIOSVideoAd <NADInterstitialVideoDelegate>

@property (nonatomic) NADInterstitialVideo *interstitialVideo;

@end

//==============================================================================

@implementation NendIOSInterstitialVideoAd

#pragma mark - NADInterstitialVideoDelegate

- (void)nadInterstitialVideoAdDidReceiveAd:(NADInterstitialVideo *)nadInterstitialVideoAd
{
    NSLog(@"%s", __FUNCTION__);
    if (self.callback) {
        self.callback(self.unityPtr, VideoAdCallbackLoadSuccess);
    }
}

- (void)nadInterstitialVideoAd:(NADInterstitialVideo *)nadInterstitialVideoAd didFailToLoadWithError:(NSError *)error
{
    NSLog(@"%s : error = %@", __FUNCTION__, error);
    if (self.errorCallback) {
        self.errorCallback(self.unityPtr, VideoAdCallbackFailedToLoad, error.code);
    }
}

- (void)nadInterstitialVideoAdDidFailedToPlay:(NADInterstitialVideo *)nadInterstitialVideoAd
{
    NSLog(@"%s", __FUNCTION__);
    if (self.callback) {
        self.callback(self.unityPtr, VideoAdCallbackFailedToPlay);
    }
}

- (void)nadInterstitialVideoAdDidOpen:(NADInterstitialVideo *)nadInterstitialVideoAd
{
    NSLog(@"%s", __FUNCTION__);
    if (self.callback) {
        self.callback(self.unityPtr, VideoAdCallbackAdShown);
    }
}

- (void)nadInterstitialVideoAdDidClose:(NADInterstitialVideo *)nadInterstitialVideoAd
{
    NSLog(@"%s", __FUNCTION__);
    if (self.callback) {
        self.callback(self.unityPtr, VideoAdCallbackAdClosed);
    }
}

- (void)nadInterstitialVideoAdDidStartPlaying:(NADInterstitialVideo *)nadInterstitialVideoAd
{
    NSLog(@"%s", __FUNCTION__);
    if (self.callback) {
        self.callback(self.unityPtr, VideoAdCallbackAdStarted);
    }
}

- (void)nadInterstitialVideoAdDidStopPlaying:(NADInterstitialVideo *)nadInterstitialVideoAd
{
    NSLog(@"%s", __FUNCTION__);
    if (self.callback) {
        self.callback(self.unityPtr, VideoAdCallbackAdStopped);
    }
}

- (void)nadInterstitialVideoAdDidCompletePlaying:(NADInterstitialVideo *)nadInterstitialVideoAd
{
    NSLog(@"%s", __FUNCTION__);
    if (self.callback) {
        self.callback(self.unityPtr, VideoAdCallbackAdCompleted);
    }
}

- (void)nadInterstitialVideoAdDidClickAd:(NADInterstitialVideo *)nadInterstitialVideoAd
{
    NSLog(@"%s", __FUNCTION__);
    if (self.callback) {
        self.callback(self.unityPtr, VideoAdCallbackAdClicked);
    }
}

- (void)nadInterstitialVideoAdDidClickInformation:(NADInterstitialVideo *)nadInterstitialVideoAd
{
    NSLog(@"%s", __FUNCTION__);
    if (self.callback) {
        self.callback(self.unityPtr, VideoAdCallbackInformationClicked);
    }
}


@end

///-----------------------------------------------
/// @name C Interfaces
///-----------------------------------------------

NendIOSVideoAdPtr _CreateInterstitialVideoAd(const char* spotId, const char* apiKey)
{
    NendIOSInterstitialVideoAd *IOSVideoAd = [[NendIOSInterstitialVideoAd alloc] init];
    IOSVideoAd.interstitialVideo = [[NADInterstitialVideo alloc] initWithSpotId:NADUnityCreateNSString(spotId) apiKey:NADUnityCreateNSString(apiKey)];
    IOSVideoAd.interstitialVideo.delegate = IOSVideoAd;
    NADUnityCacheObject(IOSVideoAd);
    return (__bridge NendIOSVideoAdPtr)IOSVideoAd;
}

void _LoadInterstitialVideoAd(NendUnityVideoAdPtr unityPtr, NendIOSVideoAdPtr iOSPtr, NendUnityVideoAdNormalCallback callback, NendUnityVideoAdErrorCallback errorCallback)
{
    NendIOSInterstitialVideoAd *IOSVideoAd = (__bridge NendIOSInterstitialVideoAd *)iOSPtr;
    IOSVideoAd.callback = callback;
    IOSVideoAd.errorCallback = errorCallback;
    IOSVideoAd.unityPtr = unityPtr;
    [IOSVideoAd.interstitialVideo loadAd];
}

void _ShowInterstitialVideoAd(NendIOSVideoAdPtr iOSPtr)
{
    NendIOSInterstitialVideoAd *IOSVideoAd = (__bridge NendIOSInterstitialVideoAd *)iOSPtr;
    if (IOSVideoAd.interstitialVideo.isReady) {
        [IOSVideoAd.interstitialVideo showAdFromViewController:UnityGetGLViewController()];
    }
}

void _ReleaseInterstitialVideoAd(NendIOSVideoAdPtr iOSPtr)
{
    NendIOSInterstitialVideoAd *IOSVideoAd = (__bridge NendIOSInterstitialVideoAd *)iOSPtr;
    [IOSVideoAd.interstitialVideo releaseVideoAd];
}

bool _IsLoadedInterstitial(NendIOSVideoAdPtr iOSPtr)
{
    NendIOSInterstitialVideoAd *IOSVideoAd = (__bridge NendIOSInterstitialVideoAd *)iOSPtr;
    return [IOSVideoAd.interstitialVideo isReady];
}

void _SetInterstitialMediationName (NendIOSVideoAdPtr iOSPtr, const char* mediationName)
{
    NendIOSInterstitialVideoAd *IOSVideoAd = (__bridge NendIOSInterstitialVideoAd *)iOSPtr;
    [IOSVideoAd.interstitialVideo setMediationName:NADUnityCreateNSString(mediationName)];
}

void _SetInterstitialUserId (NendIOSVideoAdPtr iOSPtr, const char* userId)
{
    NendIOSInterstitialVideoAd *IOSVideoAd = (__bridge NendIOSInterstitialVideoAd *)iOSPtr;
    [IOSVideoAd.interstitialVideo setUserId:NADUnityCreateNSString(userId)];
}

void _SetInterstitialOutputLog (NendIOSVideoAdPtr iOSPtr, BOOL isOutputLog)
{
    NendIOSInterstitialVideoAd *IOSVideoAd = (__bridge NendIOSInterstitialVideoAd *)iOSPtr;
    IOSVideoAd.interstitialVideo.isOutputLog = isOutputLog;
}

void _AddFallbackFullboard (NendIOSVideoAdPtr iOSPtr, const char* spotId, const char* apiKey)
{
    NendIOSInterstitialVideoAd *IOSVideoAd = (__bridge NendIOSInterstitialVideoAd *)iOSPtr;
    [IOSVideoAd.interstitialVideo addFallbackFullboardWithSpotId:NADUnityCreateNSString(spotId) apiKey:NADUnityCreateNSString(apiKey)];
}

void _DisposeInterstitialVideoAd(NendIOSVideoAdPtr iOSPtr)
{
    NendIOSInterstitialVideoAd *IOSVideoAd = (__bridge NendIOSInterstitialVideoAd *)iOSPtr;
    NADUnityRemoveObject(IOSVideoAd);
}


