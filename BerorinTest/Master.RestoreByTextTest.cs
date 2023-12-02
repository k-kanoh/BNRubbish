using BerorinApp;
using NUnit.Framework;

namespace BerorinTest.MasterTest
{
    [TestFixture("BB あくまのためいき 66|11", "BB", "あくまのためいき", 66, 11)]
    [TestFixture("BB あくまのためいき 66|", "BB", "あくまのためいき", 66, null)]
    [TestFixture("BB あくまのためいき |11", "BB", "あくまのためいき", null, 11)]
    [TestFixture("BB あくまのためいき", "BB", "あくまのためいき", null, null)]
    [TestFixture("BB 66|11", "BB", "", 66, 11)]
    [TestFixture("BB", "BB", "", null, null)]
    [TestFixture("あくまのためいき 66|11", "", "あくまのためいき", 66, 11)]
    [TestFixture("あくまのためいき", "", "あくまのためいき", null, null)]
    [TestFixture("66|11", "", "", 66, 11)]
    [TestFixture("", "", "", null, null)]
    [TestFixture("084AB0", "084AB0", "", null, null)]
    [TestFixture("2ヘッドドラゴン", "", "2ヘッドドラゴン", null, null)]
    [TestFixture("FF3", "0FF3", "", null, null)]
    [TestFixture("FF3初期設定.idn", "", "FF3初期設定.idn", null, null)]
    public class RestoreByTextTest(string _text, string _hex, string _name, int? _order, int? _group)
    {
        private Master _master = Master.RestoreByText(_text);

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

        [Test]
        public void Order()
        {
            Assert.That(_master.Order, Is.EqualTo(_order));
        }

        [Test]
        public void Group()
        {
            Assert.That(_master.Group, Is.EqualTo(_group));
        }
    }
}
