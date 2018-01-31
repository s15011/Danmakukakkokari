using NendUnityPlugin.Platform;

namespace NendUnityPlugin.AD.Video
{
	/// <summary>
	/// Video ad.
	/// </summary>
	public abstract class NendAdInterstitialVideo : NendAdVideo
	{
		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <param name="spotId">Spot id.</param>
		/// <param name="apiKey">API key.</param>
		public static NendAdInterstitialVideo NewVideoAd (string spotId, string apiKey)
		{
			return NendAdNativeInterfaceFactory.CreateInterstitialVideoAd (spotId, apiKey);
		}

		/// <summary>
		/// Add fallback fullboard ad.
		/// </summary>
		public void AddFallbackFullboard (string spotId, string apiKey)
		{
			AddFallbackFullboardInternal (spotId, apiKey);
		}

		internal abstract void AddFallbackFullboardInternal (string spotId, string apiKey);

	}
}

