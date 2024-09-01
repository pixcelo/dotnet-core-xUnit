using Xunit;

namespace PrimeService.Tests
{
    /// <summary>
    /// サンプルテストクラス
    /// </summary>
    /// <remarks>
    /// 1. テストクラスのインスタンスは、テストメソッドごとに生成される
    /// 2. テストメソッドの実行順は、ランダムとなる
    /// </remarks>
    /// <see href="https://xunit.net/docs/shared-context"/>
    public class StackTests : IDisposable
    {
        Stack<int> stack;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks>
        /// オブジェクト インスタンスを共有せずに、セットアップ/クリーンアップ コードを共有する
        /// 実行されるごとに常にクリーンなコピーが取得される
        /// </remarks>
        public StackTests()
        {
            // ここにセットアップコードを実装する
            stack = new Stack<int>();
        }

        public void Dispose()
        {
            // ここにクリーンアップコードを実装する
            stack.GetEnumerator().Dispose();            
        }

        [Fact]
        public void WithNoItems_CountShouldReturnZero()
        {
            var count = stack.Count;

            Assert.Equal(0, count);
        }

        [Fact]
        public void AfterPushingItem_CountShouldReturnThree()
        {
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            var enumerator = stack.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var item = enumerator.Current;
                Console.WriteLine(item);
            }

            Assert.Equal(3, stack.Count);
        }
    }
}
