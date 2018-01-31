#if UNITY_ANDROID
namespace NendUnityPlugin.Platform.Android
{
	using System;
	using UnityEngine;

	internal class VideoMethodUtils
	{
		internal static void LoadAd (AndroidJavaObject m_JavaObject, AndroidVideoAdListener listener)
		{
			m_JavaObject.Call ("loadAd", listener);
		}

		internal static bool IsLoaded (AndroidJavaObject m_JavaObject)
		{
			return m_JavaObject.Call <bool>("isLoaded");
		}

		internal static void ShowAd (AndroidJavaObject m_JavaObject)
		{
			using (var activity = CommonUtils.GetCurrentActivity()) {
				m_JavaObject.Call ("showAd", activity);
			}
		}

		internal static void ReleaseAd (AndroidJavaObject m_JavaObject)
		{
			m_JavaObject.Call ("releaseAd");
		}

		internal static void SetMediationName (AndroidJavaObject m_JavaObject, string mediationName) {
			m_JavaObject.Call ("setMediationName", mediationName);
		}

		internal static void SetUserId (AndroidJavaObject m_JavaObject, string userId) {
			m_JavaObject.Call ("setUserId", userId);
		}

		internal static void AddFallbackFullboard (AndroidJavaObject m_JavaObject, string spotId, string apiKey) {
			m_JavaObject.Call ("addFallbackFullboard", int.Parse (spotId), apiKey);
		}

	}
}
#endif


