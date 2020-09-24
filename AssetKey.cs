using System;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;

/*===============================================================
Project:	Peasyread
Developer:	Marci San Diego
Company:	David Morgan Education - marcianosd@dm-ed.com
Date:		14/02/2019 13:55
===============================================================*/

namespace DMED.AssetCatalog
{
	[Serializable]
	public struct AssetKey : IEquatable<AssetKey>
	{
		[FormerlySerializedAs("_keySet")]
		[Tooltip("The name of the set which this asset belongs in.")]
		public string keySet;

		[FormerlySerializedAs("_keyEvent")]
		[Tooltip("The name of the \"event\" for this asset.")]
		public string keyEvent;

		[FormerlySerializedAs("_version")]
		[Tooltip("Version number of the asset used for indexing.")]
		public int version;

		public bool isEmpty => string.IsNullOrWhiteSpace(keySet) || string.IsNullOrWhiteSpace(keyEvent);

		private int test = -1;

		public AssetKey(AssetKey assetKey)
		{
			keySet = assetKey.keySet;
			keyEvent = assetKey.keyEvent;
			version = assetKey.version;
		}

		public AssetKey(string keySet, string keyEvent, int version = 1)
		{
			this.keySet = keySet;
			this.keyEvent = keyEvent;
			this.version = version;
		}

		public void Copy(AssetKey assetKey)
		{
			keySet = assetKey.keySet;
			keyEvent = assetKey.keyEvent;
			version = assetKey.version;
		}

		public void Set(string keySet, string keyEvent, int version)
		{
			this.keySet = keySet;
			this.keyEvent = keyEvent;
			this.version = version;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(keySet);
			sb.Append('.');
			sb.Append(keyEvent);
			sb.Append('.');
			sb.Append(version);
			return sb.ToString();
		}

		public override int GetHashCode()
		{
			return keySet.GetHashCode() ^ (keyEvent.GetHashCode() << 2) ^ (version.GetHashCode() >> 2);
		}

		public override bool Equals(object other)
		{
			if (!(other is AssetKey)) return false;

			return Equals((AssetKey)other);
		}

		public bool Equals(AssetKey other)
		{
			return keySet == other.keySet && keyEvent == other.keyEvent && version == other.version;
		}

		public static bool operator ==(AssetKey lhs, AssetKey rhs)
		{
			return lhs.Equals(rhs);
		}

		public static bool operator !=(AssetKey lhs, AssetKey rhs)
		{
			return !(lhs == rhs);
		}

		public static implicit operator string(AssetKey assetKey) => assetKey.ToString();

		public static AssetKey Parse(string audioKeyString)
		{
			var split = audioKeyString.Split('.');

			try {
				var set = split[0];
				var evt = split[1];
				var ver = int.Parse(split[2]);
				return new AssetKey(set, evt, ver);
			} catch (Exception) {
				throw new Exception(string.Format("Incorrect Audio Key string format!"));
			}
		}

		public static bool TryParse(string audioKeyString, out AssetKey audioKey)
		{
			var split = audioKeyString.Split('.');

			if (split.Length == 3) {
				var set = split[0];
				var evt = split[1];
				if (int.TryParse(split[2], out int ver)) {
					audioKey = new AssetKey(set, evt, ver);
					return true;
				}
			}
			audioKey = new AssetKey();
			return false;
		}

	}
}
