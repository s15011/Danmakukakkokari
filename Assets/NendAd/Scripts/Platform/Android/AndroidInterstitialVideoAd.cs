﻿#if UNITY_ANDROID
namespace NendUnityPlugin.Platform.Android
{
	using System;
	using UnityEngine;
	using NendUnityPlugin.AD.Video;

	internal class AndroidInterstitialVideoAd : NendAdInterstitialVideo
	{
		private const string NendAdVideoClassName = "net.nend.unity.plugin.NendUnityInterstitialVideoAd";
		private AndroidJavaObject m_JavaObject;
		private AndroidVideoAdListener m_Listener;

		internal AndroidInterstitialVideoAd (string spotId, string apiKey) : base ()
		{
			using (var activity = CommonUtils.GetCurrentActivity()) {
				m_JavaObject = new AndroidJavaObject (NendAdVideoClassName, activity, int.Parse (spotId), apiKey);
			}
			m_Listener = AndroidVideoAdListener.NewListener (onResponse);
		}

		internal override void LoadInternal ()
		{
			VideoMethodUtils.LoadAd(m_JavaObject, m_Listener);
		}

		internal override bool IsLoadedInternal ()
		{
			return VideoMethodUtils.IsLoaded(m_JavaObject);
		}

		internal override void ShowInternal ()
		{
			VideoMethodUtils.ShowAd(m_JavaObject);
		}

		internal override void ReleaseInternal ()
		{
			if (null != m_JavaObject) {
				if (null != m_Listener) {
					m_Listener.ReleaseCallback ();
					m_Listener = null;
				}
				VideoMethodUtils.ReleaseAd(m_JavaObject);
				m_JavaObject.Dispose ();
				m_JavaObject = null;
			}
		}

		internal override void SetMediationNameInternal (string mediationName) {
			VideoMethodUtils.SetMediationName(m_JavaObject, mediationName);
		}

		internal override void SetUserIdInternal (string userId) {
			VideoMethodUtils.SetUserId(m_JavaObject, userId);
		}

		internal override void AddFallbackFullboardInternal (string spotId, string apiKey) {
			VideoMethodUtils.AddFallbackFullboard(m_JavaObject, spotId, apiKey);
		}

		private void onResponse (VideoAdCallbackType type, object args)
		{
			switch (type) {
			case VideoAdCallbackType.FailedToLoad:
				CallBack (new ErrorVideoAdCallbackArgments(type, (int)args));
				break;
			default:
				CallBack (new VideoAdCallbackArgments(type));
				break;
			}
		}

		~AndroidInterstitialVideoAd ()
		{
			Dispose ();
		}

		public override void Dispose ()
		{
			NendUnityPlugin.Common.NendAdLogger.D ("Dispose AndroidVideoAd.");
			ReleaseInternal ();
		}
	}
}
#endif

