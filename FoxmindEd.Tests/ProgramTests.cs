using NUnit.Framework;
using System.Linq;

namespace FoxmindEd.Tests
{
    public class Tests
    {
        [Test]
        public void Should_Throw_File_Dont_Contain_Data()
        {
            Assert.That(() => Program.ProcessRows(null, out double maxSumma), Throws.Exception);
        }

        [Test]
        public void Should_Throw_File_Dont_Contain_Any_Data()
        {
            Assert.That(() => Program.ProcessRows(Enumerable.Empty<string>().ToArray(), out double maxSumma), Throws.Exception);
        }

        [Test]
        public void Should_Return_Max_Zero_Summa()
        {
            var strArray = new[] { "0,0", "0,0, 000"};

            var res = Program.ProcessRows(strArray, out double maxSumma);

            var records = res.Where(v => v.Value == maxSumma).Select(k => k.Key).ToArray();

            Assert.AreEqual(maxSumma, 0);
            Assert.AreEqual(records.Length, 2);
            Assert.IsTrue(records.Contains(1) || records.Contains(2));
        }

        [Test]
        public void Should_Return_Max_Zero_Summa_With_Negative_Num()
        {
            var strArray = new[] { "0,0", "-1, 0, 1" };

            var res = Program.ProcessRows(strArray, out double maxSumma);

            var records = res.Where(v => v.Value == maxSumma).Select(k => k.Key).ToArray();

            Assert.AreEqual(maxSumma, 0);
            Assert.AreEqual(records.Length, 2);
            Assert.IsTrue(records.Contains(1) || records.Contains(2));
        }

        [Test]
        public void Should_Return_Max_Summa()
        {
            var strArray = new[] { "1, 3, 4", "f, 7.8, 0", "2.3, 6, 0, 5.4"};

            var res = Program.ProcessRows(strArray, out double maxSumma);

            var maxRecord = res.Where(v => v.Value == maxSumma).Select(k => k.Key).ToArray();

            Assert.AreEqual(maxRecord.Length, 1);
            Assert.AreEqual(3, maxRecord.First());
        }

        [Test]
        public void Should_Return_Record_With_Character()
        {
            var strArray = new[] { "1, 3, 4", "f, 7.8, 0", "2.3, 6, 0, 5.4" };

            var res = Program.ProcessRows(strArray, out double maxSumma);

            var record = res.Where(v => v.Value == Program.BreackedSumma).Select(k => k.Key).ToArray();

            Assert.AreEqual(record.Length, 1);
            Assert.AreEqual(2, record.First());
        }

        [Test]
        public void Should_Return_Records_With_Character()
        {
            var strArray = new[] { "1, 3, 4", "f, 7.8, 0", "2.3, 6, 0, 5.4h" };

            var res = Program.ProcessRows(strArray, out double maxSumma);

            var records = res.Where(v => v.Value == Program.BreackedSumma).Select(k => k.Key).ToArray();

            Assert.AreEqual(records.Length, 2);
            Assert.IsTrue(records.Contains(2) || records.Contains(3));
        }

        [Test]
        public void Should_Return_Records_MaxSumma()
        {
            var strArray = new[] { "1, 3, 4", "f, 7.8, 0", "2.3, 6, 0, 5.4", "2.3, 6, 0, 5.4" };

            var res = Program.ProcessRows(strArray, out double maxSumma);

            var records = res.Where(v => v.Value == maxSumma).Select(k => k.Key).ToArray();

            Assert.AreEqual(records.Length, 2);
            Assert.IsTrue(records.Contains(3) || records.Contains(4));
        }
    }
}