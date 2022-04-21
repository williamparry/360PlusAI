using System.Security.Cryptography;
using System.Text;

namespace ThreeSixtyPlusAI.Data
{

    public static class Utils
    {

        internal static readonly char[] chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        internal static string GetUniqueKey(int size)
        {
            byte[] data = new byte[4 * size];
            using (var crypto = RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new(size);
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;

                result.Append(chars[idx]);
            }

            return result.ToString();
        }

        public static string GetAccessCode(HttpContext context, bool throwError = true)
        {
            var accessCodeId = context.Session.GetString("AccessCodeId");

            if(String.IsNullOrEmpty(accessCodeId) && throwError) {
                throw new Exception();
            } else {
                return accessCodeId ?? "";
            }
        }

        public static void SetAccessCode(HttpContext context, string accessCodeId) {
            context.Session.SetString("AccessCodeId", accessCodeId);
        }

        public static bool GetHasThreeSixtyReview(HttpContext context)
        {
            return bool.Parse(context.Session.GetString("HasThreeSixtyReview") ?? "false");
        }

        public static void SetHasThreeSixtyReview(HttpContext context, bool hasThreeSixtyReview) {
            context.Session.SetString("HasThreeSixtyReview", hasThreeSixtyReview.ToString());
        }

        public static string GenerateAccessCodeText()
        {
            return GetUniqueKey(200);
        }

    }

}