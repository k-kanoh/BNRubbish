using BerorinApp;
using NUnit.Framework;
using System.Reflection;

namespace BerorinTest.BNE2ConverterTest
{
    [TestFixture("BB あくまのためいき", "BB", "あくまのためいき")]
    [TestFixture("あくまのためいき", "", "あくまのためいき")]
    [TestFixture("BB", "BB", "")]
    [TestFixture("BB ", "BB", "")]
    [TestFixture("8A40", "8A40", "")]
    [TestFixture("8A430", "", "8A430")]
    [TestFixture("BBあくまのためいき", "", "BBあくまのためいき")]
    [TestFixture("B	アルミラージ", "0B", "アルミラージ")]
    [TestFixture("", "", "")]
    public class IdnConvertMasterTest
    {
        private Master _master;
        private string _hex;
        private string _name;

        public IdnConvertMasterTest(string text, string hex, string name)
        {
            _hex = hex;
            _name = name;

            var method = typeof(BNE2Converter).GetMethod("IdnConvertMaster", BindingFlags.NonPublic | BindingFlags.Instance);
            _master = (Master)method.Invoke(new BNE2Converter(), new object[] { text });
        }

        [Test]
        public void Hex()
        {
            Assert.That(_master.Hex, Is.EqualTo(_hex));
        }

        [Test]
        public void Name()
        {
            Assert.That(_master.Name, Is.EqualTo(_name));
        }
    }
}
