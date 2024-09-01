using System.Collections;
using Xunit;

namespace PrimeService.Tests
{
    public class CalculatorTests
    {
        private readonly Calculator _calculator;

        /// <summary>
        /// <see href="https://andrewlock.net/creating-parameterised-tests-in-xunit-with-inlinedata-classdata-and-memberdata/"/>
        /// </summary>
        public CalculatorTests()
        {
            this._calculator = new Calculator();
        }

        [Fact]
        public void Add_ShouldReturnCorrectSum()
        {
            // Arrange
            int a = 5;
            int b = 3;

            // Act
            int result = this._calculator.Add(a, b);

            // Assert
            Assert.Equal(8, result);
        }

        /// <summary>
        /// InlineData
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        [Theory]
        [InlineData(5, 3, 2)]
        [InlineData(10, 5, 5)]
        [InlineData(0, 0, 0)]
        public void Ok_Add(int expected, int a, int b)
        {
            // Act
            int actual = _calculator.Add(a, b);

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// ClassDataでデータを提供する
        /// </summary>
        [Theory]
        [ClassData(typeof(CalculatorTestData))]
        public void CanAddTheoryClassData(int expected, int a, int b)
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var actual = calculator.Add(a, b);

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// データ提供クラス
        /// </summary>
        public class CalculatorTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 3, 1, 2 };
                yield return new object[] { -10,　-4, -6 };
                yield return new object[] { 0, -2, 2 };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        /// <summary>
        /// ジェネレータプロパティを使用したテスト
        /// </summary>
        [Theory]
        [MemberData(nameof(TestData))]
        public void CanAddTheoryMemberDataProperty(int expected, int a, int b)
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var actual = calculator.Add(a, b);

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// ジェネレータプロパティ
        /// </summary>
        public static IEnumerable<object[]> TestData =>
            new List<object[]>
            {
                new object[] { 3, 1, 2 },
                new object[] { -10, -4, -6 },
                new object[] { 0, -2, 2 }
            };

        //[Fact]
        //public void Divide_ByZero_ShouldThrowDivideByZeroException()
        //{
        //    // Arrange
        //    int a = 10;
        //    int b = 0;

        //    // Act & Assert
        //    Assert.Throws<System.DivideByZeroException>(() => _calculator.Divide(a, b));
        //}

        /// <summary>
        /// 別のクラスのプロパティまたはメソッドからデータを読み込む
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        [Theory]
        [MemberData(nameof(CalculatorData2.Data), MemberType = typeof(CalculatorData2))]
        public void CanAddTheoryMemberDataMethod(int expected, int a, int b)
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var actual = calculator.Add(a, b);

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// データ提供クラス
        /// </summary>
        public class CalculatorData2
        {
            public static IEnumerable<object[]> Data =>
                new List<object[]>
                {
                    new object[] { 3, 1, 2 },
                    new object[] { -10, -4, -6 },
                    new object[] { 0, -2, 2 }                    
                };
        }

    }
}
