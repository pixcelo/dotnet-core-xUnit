using PrimeService.Models;
using Xunit;

namespace PrimeService.Tests
{
    /// <summary>
    /// アサーションマッチャー調査用クラス
    /// <see href="https://iiweis.net/blog/csharp-xunit-assert-cheat-sheet/"/>
    /// </summary>
    public class AssertionExamples
    {
        /// <summary>
        /// AssertEqual
        /// </summary>
        /// <see cref="AssertEqualOverloads"/>
        [Fact]
        public void AssertEqual()
        {
            // Assert.Equal(期待値, 実行値)
            Assert.Equal("1", 1.ToString());

            // シーケンスも比較可。
            Assert.Equal(new[] { "1", "2", }, "1,2".Split(',')); // T[]
            Assert.Equal(new List<string>() { "1", "2", }, "1,2".Split(',')); // IEnumerable<string>
        }

        /// <summary>
        /// AssertStrictEqual
        /// </summary>
        [Fact]
        public void AssertStrictEqual()
        {
            // Assert.StrictEqual(期待値, 実行値)
            Assert.StrictEqual("1", 1.ToString());

            // EqualityComparer<T>.Defaultが比較に使用されるため、
            // Assert.Equalで比較可能だったシーケンスの要素比較は不可。
            // 配列の場合は以下のように同一インスタンスであればパスする。
            var texts = new string[] { "1", "2", };
            Assert.StrictEqual(texts, texts);
        }

        [Fact]
        public void AssertNotEqual()
        {
            // Assert.NotEqual(期待値, 実行値);
            //Assert.NotEqual("1", 1.ToString());

            // シーケンスも比較可。
            Assert.NotEqual(new[] { "1", "2", }, "1,2,3".Split(',')); // T[]
            Assert.NotEqual(new List<string>() { "1", "2", }, "1,2,3".Split(',')); // IEnumerable<string>
        }

        [Fact]
        public async void AssertThrowsAny()
        {
            var zero = 0;

            // ジェネリック版
            // Assert.ThrowsAny<型>(テストコード)
            Assert.ThrowsAny<Exception>(new Action(() => throw new ArgumentException()));
            Assert.ThrowsAny<Exception>(() => 1 / zero);

            // 非ジェネリック版
            // 無し。


            // 非同期ジェネリック版
            // Assert.ThrowsAnyAsync<型>(テストコード)
            await Assert.ThrowsAnyAsync<Exception>(() => Task.Run(() => 1 / zero));

            // 非同期非ジェネリック版
            // 無し。
        }

        [Fact]
        public void DemonstrateAssertions()
        {
            // 1. Assert.Equal - 値が等しいことを検証
            Assert.Equal(4, 2 + 2);
            Assert.Equal("Hello", "Hello");

            // 2. Assert.NotEqual - 値が等しくないことを検証
            Assert.NotEqual(5, 2 + 2);

            // 3. Assert.True / Assert.False - 条件が真または偽であることを検証
            Assert.True(1 < 2);
            Assert.False(1 > 2);

            // 4. Assert.Null / Assert.NotNull - オブジェクトがnullまたはnullでないことを検証
            object obj = null;
            Assert.Null(obj);
            obj = new object();
            Assert.NotNull(obj);

            // 5. Assert.Same / Assert.NotSame - 同じインスタンスであるかを検証
            var list1 = new List<int>();
            var list2 = list1;
            var list3 = new List<int>();
            Assert.Same(list1, list2);
            Assert.NotSame(list1, list3);

            // 6. Assert.Contains / Assert.DoesNotContain - コレクションに要素が含まれているかを検証
            var numbers = new List<int> { 1, 2, 3, 4, 5 };
            Assert.Contains(3, numbers);
            Assert.DoesNotContain(6, numbers);

            // 7. Assert.Throws<T> - 例外がスローされることを検証
            Assert.Throws<DivideByZeroException>(() => { var result = 1 / Convert.ToInt32("0"); });

            // 8. Assert.InRange / Assert.NotInRange - 値が指定範囲内にあるかを検証
            Assert.InRange(5, 1, 10);
            Assert.NotInRange(11, 1, 10);

            // 9. Assert.Matches / Assert.DoesNotMatch - 文字列が正規表現にマッチするかを検証
            Assert.Matches("^Hello", "Hello, World!");
            Assert.DoesNotMatch("^Goodbye", "Hello, World!");

            // 10. Assert.IsType<T> / Assert.IsNotType<T> - オブジェクトの型を検証
            Assert.IsType<string>("Hello");
            Assert.IsNotType<int>("Hello");

            // 11. Assert.Empty / Assert.NotEmpty - コレクションが空であるかを検証
            Assert.Empty(new List<int>());
            Assert.NotEmpty(new[] { 1, 2, 3 });

            // 12. Assert.All - コレクションの全要素が条件を満たすことを検証
            var evenNumbers = new[] { 2, 4, 6, 8 };
            Assert.All(evenNumbers, num => Assert.Equal(0, num % 2));
        }

        /// <summary>
        /// AssertEqualの単体テスト
        /// </summary>
        [Fact]
        public void AssertEqualOverloads()
        {
            // 1. Assert.Equal(object expected, object actual)
            object obj1 = "test";
            object obj2 = "test";
            Assert.Equal(obj1, obj2);

            // 2. Assert.Equal<T>(T expected, T actual)
            int num1 = 5;
            int num2 = 5;
            Assert.Equal(num1, num2);

            // 3. Assert.Equal(double expected, double actual, int precision)
            double d1 = 0.33333;
            double d2 = 1.0 / 3.0;
            int precision = 5; // precisionは「的確、正確、精密さ」を意味
            Assert.Equal(d1, d2, precision); // 小数点以下5桁まで等しいかどうかを検証

            // 4. Assert.Equal(decimal expected, decimal actual, int precision)
            decimal dec1 = 0.1m;
            decimal dec2 = 0.1m;
            precision = 10;
            Assert.Equal(dec1, dec2, precision);

            // 5. Assert.Equal<T>(IEnumerable<T> expected, IEnumerable<T> actual)
            var list1 = new List<int> { 1, 2, 3 };
            var list2 = new List<int> { 1, 2, 3 };
            Assert.Equal(list1, list2);　// 各要素が等しいかどうかを検証

            // 6. Assert.Equal(DateTime expected, DateTime actual, TimeSpan precision)
            var date1 = DateTime.Now;
            var date2 = date1.AddMilliseconds(10);
            Assert.Equal(date1, date2, TimeSpan.FromSeconds(1));

            // 7. Assert.Equal(string expected, string actual, bool ignoreCase = false, bool ignoreLineEndingDifferences = false, bool ignoreWhiteSpaceDifferences = false)
            string str1 = "Hello\rWorld ";
            string str2 = "hello\nworld  ";
            Assert.Equal(
                str1,
                str2,
                ignoreCase: true, // 大文字と小文字の違いを無視
                ignoreLineEndingDifferences: true,　// 改行コードの違いを無視
                ignoreWhiteSpaceDifferences: true); // 空白の違いを無視

            // 8. Assert.Equal<T>(T expected, T actual, Func<T, T, bool> comparer)
            var person1 = new Person { Name = "John", Age = 30 };
            var person2 = new Person { Name = "John", Age = 30 };
            var person3 = person1;
            Assert.NotEqual(person1, person2); // 異なるインスタンスなので等しくない
            Assert.Equal(person1, person3); // 同じインスタンスなので等しい
            Assert.Equal(person1, person2, new GenericComparer<Person>(x => x.Name));
            
            Assert.Same(person1, person3); // 同じインスタンスかどうかを比較
            Assert.NotSame(person1, person2);

            var person4 = new PersonRecord { Name = "John", Age = 30 };
            var person5 = new PersonRecord { Name = "John", Age = 30 };
            Assert.Equal(person4, person5); // レコード型（C#9.0～）はプロパティの値が等しい場合、等しいと判定
        }

        /// <summary>
        /// IEqualityComparerを実装した比較用クラス
        /// </summary>
        /// <see href="https://stackoverflow.com/questions/6694508/how-to-use-the-iequalitycomparer"/>
        /// <typeparam name="T"></typeparam>
        public class GenericComparer<T> : IEqualityComparer<T> where T : class
        {
            private Func<T, object> _expr { get; set; }

            public GenericComparer(Func<T, object> expr)
            {
                this._expr = expr;
            }

            public bool Equals(T x, T y)
            {
                var first = _expr.Invoke(x);
                var sec = _expr.Invoke(y);
                return first != null && first.Equals(sec);
            }

            public int GetHashCode(T obj)
            {
                return obj.GetHashCode();
            }
        }

        [Fact]
        public void AssertTrueFalseOverloads()
        {
            Assert.True(true);
            //Assert.True(false, "custom message");

            Assert.False(false);
            //Assert.False(true, "custom message");
        }

        [Fact]
        public void ok()
        {
            //float intValue = 1.0;
            //double doubleValue = 1.0;

            //Assert.Equal(intValue, doubleValue);
            //Assert.StrictEqual(intValue, doubleValue);
            Assert.StrictEqual("1", 1.ToString());
        }

    }
}
