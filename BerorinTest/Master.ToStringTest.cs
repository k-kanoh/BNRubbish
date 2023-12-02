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
    public class ToStringTest(string _text, string _hex, string _name, int? _order, int? _group)
    {
        [Test]
        public void Output()
        {
            var master = new Master() { Hex = _hex, Name = _name, Order = _order, Group = _group };
            Assert.That(master.ToString(), Is.EqualTo(_text));
        }
    }
}
