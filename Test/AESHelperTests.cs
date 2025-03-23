using Common.Encryptions;

namespace Test
{
    public class AESHelperTests
    {
        private const string key = "a7f3c2e1d4b8g9h1";
        private const string iv = "z6y5x4w3v2u1t0s9";

        [Theory]
        [InlineData("CathayOnlinePractice")]
        [InlineData("Ryan")]
        [InlineData("Hello 世界")]
        public void EncryptDecrypt_ReturnsOriginalText(string originalText)
        {
            string encrypted = AESHelper.Encrypt(originalText, key, iv);
            string decrypted = AESHelper.Decrypt(encrypted, key, iv);

            Assert.Equal(originalText, decrypted);
        }
        [Fact]
        public void Encrypt_GeneratesDifferentOutputForDifferentInput()
        {
            string text1 = "TextOne";
            string text2 = "TextTwo";

            string encrypted1 = AESHelper.Encrypt(text1, key, iv);
            string encrypted2 = AESHelper.Encrypt(text2, key, iv);

            Assert.NotEqual(encrypted1, encrypted2);
        }
    }
}
