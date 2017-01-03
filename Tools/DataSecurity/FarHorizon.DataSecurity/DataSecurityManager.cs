using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FarHorizon.DataSecurity
{
    public class DataSecurityManager
    {
        const string secretString = "fAr7fba3382b65f42f89H0rI";
        const string SALT = "T23h45is124I54s55M65il3q43F5Ti9w";
        const string IV = "h0wgdfg433C34l45StH544iSh4s9Sf0m";

        #region Variable(s)
        //byte[] iv = new byte[16];

        ////byte[] _secretArray;
        //byte[] SecretArray
        //{
        //    get { return _secretArray == null ? _secretArray = Encoding.UTF8.GetBytes(secretString) : _secretArray; }
        //    set { _secretArray = value; }
        //}        

        ////AesEngine _engine;
        //AesEngine Engine
        //{
        //    get { return _engine == null ? new AesEngine() : _engine; }
        //    set { _engine = value; }
        //}

        ////CbcBlockCipher _blockCipher; //CBC
        //CbcBlockCipher BlockCipher
        //{
        //    get { return _blockCipher == null ? new CbcBlockCipher(Engine) : _blockCipher; }
        //    set { _blockCipher = value; }
        //}

        ////PaddedBufferedBlockCipher _cipher; //Default scheme is PKCS5/PKCS7
        //PaddedBufferedBlockCipher Cipher
        //{
        //    get { return _cipher == null ? new PaddedBufferedBlockCipher(BlockCipher) : Cipher; }
        //    set { Cipher = value; }
        //}

        ////KeyParameter _keyParam;
        //KeyParameter KeyParam
        //{
        //    get { return _keyParam == null ? new KeyParameter(Convert.FromBase64String(secretString)) : _keyParam; }
        //    set { _keyParam = value; }
        //}

        ////ParametersWithIV _keyParamWithIV;
        //ParametersWithIV KeyParamWithIV
        //{
        //    get { return _keyParamWithIV == null ? new ParametersWithIV(KeyParam, iv, 0 , 16) : _keyParamWithIV; }
        //    set { _keyParamWithIV = value; }
        //}

        //public DataSecurityManager()
        //{
        //    //Set up
        //    _engine = new AesEngine();
        //    _blockCipher = new CbcBlockCipher(_engine); //CBC
        //    _cipher = new PaddedBufferedBlockCipher(_blockCipher); //Default scheme is PKCS5/PKCS7
        //    _keyParam = new KeyParameter(Convert.FromBase64String(secretString));
        //    _keyParamWithIV = new ParametersWithIV(_keyParam, iv, 0, 16);

        //    secretArray = Encoding.UTF8.GetBytes(secretString);
        //}

        // This constant is used to determine the keysize of the encryption algorithm in bits.
        // We divide this by 8 within the code below to get the equivalent number of bytes.
        #endregion

        private const int Keysize = 256;

        // This constant determines the number of iterations for the password bytes generation function.
        private const int DerivationIterations = 1000;

        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                return plainText;
            }

            if (ConfigurationManager.AppSettings["EncryptionAllowed"] != null)
            {
                var encryptionAllowed = Convert.ToBoolean(ConfigurationManager.AppSettings["EncryptionAllowed"]);
                if (!encryptionAllowed)
                    return plainText;
            }

            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            //var saltStringBytes = Generate256BitsOfRandomEntropy();
            //var ivStringBytes1 = Generate256BitsOfRandomEntropy();

            var saltStringBytes = Encoding.UTF8.GetBytes(SALT);
            var ivStringBytes = Encoding.UTF8.GetBytes(IV);

            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(secretString, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return cipherText;

            if (cipherText.Length < 64 || cipherText.Length % 4 != 0)
            {
                //This is not encrypted text or some part of the encrypted text is missing.
                //Text length has to be multiple of 4.
                return cipherText;
            }

            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();

            var saltString = Encoding.UTF8.GetString(saltStringBytes, 0, saltStringBytes.Length);
            if (string.Compare(saltString, SALT, false) != 0)
            {
                return cipherText;
            }

            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(secretString, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }
    }
}
