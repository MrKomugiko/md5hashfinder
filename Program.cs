using System.Security.Cryptography;
using System.Text;

namespace md5hashfinder 
{
    internal class Program
    {
        public const string allValidChars = " !\"#$%&\'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
        public const string hash = "2530fcdf7c92e1a4fec0542f8d17b72457ddd554d12b9800492f19960568fd6fae95fa5e7e9d77c08a7ca2ffbd23e38af352dbc8ff604e282574f44a7ebf7a176d87dc935f28fbf7657d4080375611b943d7273b7d8c0b9252c2ad1f99380772719c8eb2b6328435528a932190ae437e53b08811659ca41ad72576b15e9f060407a1c3c80db4a95eddaea3aef86e9cae03a01042656c639820dd2052227ad08fff72708b7b5ce1181ff0bb669da7493fcddd25cfb17d6bbad2a6da7dc316d2174e8006988778a71db78c96813e94a622dcf9c4c79401a30db2ae7bdff36a1bf9e24369f16e9c4a7595cb59519b825b3bc8611072effc47b68d987faf9c9156c13c96a99088566cde77530f79ec7ccd1b3411eb2bef4e72fa065cdbad6da0bf80bfcf008dd07f59aa5f546d3dc57e8ec7";
        public const string email = "alperenkuru83@gmail.com";

        static void Main(string[] args)
        {
            Console.ReadKey();

            Console.WriteLine(Decoder(hash));
            Console.ReadKey();
        }

        public static char[] allowedchars = new char[96];
        public static string Decoder(string hash)
        {
            allowedchars = allValidChars.ToCharArray();
            int length = hash.Length / 32;
            string hashpart = "",fulldecodedhash = "";

            for(int iteration = 0; iteration < length; iteration++)
            {
                hashpart = hash.Substring(0+iteration*32,32);
                fulldecodedhash+=ResolveHashPart(fulldecodedhash,hashpart);
            }
            return fulldecodedhash;
        }
        private static string? ResolveHashPart(string decryptedpart, string hashpart)
        {
            string generatedhash = "";
            for (int i = 0; i < allValidChars.Length; i++)
            {
                for (int k = 0; k < allValidChars.Length; k++)
                {
                    generatedhash = HashingFormula(String.Concat(decryptedpart+ allValidChars[i].ToString() + allValidChars[k].ToString()));
                    
                    if (generatedhash == hashpart)
                    {
                        string result = String.Concat(allValidChars[i].ToString() + allValidChars[k].ToString());
                        Console.WriteLine($"decoded: {hashpart} = {result}");
                        return result;
                    }
                }
            }
            return null;
        }
        private static string HashingFormula(string input) => CreateMD5(CreateMD5(email) + input + CreateMD5(input));
        private static string CreateMD5(string input)
        {
            // Step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString().ToLower();
        }
    
    }
}