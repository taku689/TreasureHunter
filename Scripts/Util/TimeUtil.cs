using System;

namespace TreasureHunter.Util
{
    public static class TimeUtil
    {

        private static DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        public static int CurrentUnixTime()
        {
            return (int)(DateTime.Now - UnixEpoch).TotalSeconds;
        }

        // UnixTimeからDateTimeへ変換
        public static DateTime GetDateTime(long unixTime)
        {
            return UnixEpoch.AddSeconds(unixTime);
        }
    }
}