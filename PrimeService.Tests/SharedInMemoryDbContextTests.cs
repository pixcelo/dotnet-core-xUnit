using Xunit;

namespace PrimeService.Tests
{
    /// <summary>
    /// Fixtureクラス
    /// </summary>
    /// <remarks>
    /// メリット
    /// 1. リソースの効率的な使用：データベースコンテキストや他の重いリソースをテストごとに再生成する必要がなくなり、リソースの使用効率が向上する
    /// 2. 実行時間を短縮：データベースの初期化や接続の確立など、コストのかかる操作を一度だけ行うことで、テストの実行時間を短縮できる
    /// 3. セットアップとクリーンアップの簡素化：セットアップとクリーンアップのコードを一箇所にまとめることができ、コードの重複を避けることができる
    /// </remarks>
    public class InMemoryDbContextFixture : IDisposable
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public InMemoryDbContextFixture()
        {
            this.Context = new InMemoryDbContext();

            // ... initialize data in the test database ...
        }

        public void Dispose()
        {
            // ... clean up test data from the database ...
        }

        public InMemoryDbContext Context { get; private set; }
    }

    public class SharedInMemoryDbContextTests : IClassFixture<InMemoryDbContextFixture>, IDisposable
    {
        InMemoryDbContextFixture fixture;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SharedInMemoryDbContextTests(InMemoryDbContextFixture fixture)
        {
            // 共有したFixtureを受け取る
            this.fixture = fixture;
        }

        public void Dispose()
        {
            // ...
        }

        [Fact]
        public void WithNoItems_CountShouldReturnZero()
        {
            var count = this.fixture.Context.Users.Count();
            this.fixture.Context.Users.Add(new User());

            Assert.True(0 <= count);
        }

        [Fact]
        public void AfterAddingItem_CountShouldReturnOne()
        {
            this.fixture.Context.Users.Add(new User());

            var count = this.fixture.Context.Users.Count;

            Assert.True(0 <= count);
        }
    }

    /// <summary>
    /// インメモリデータベースコンテキスト
    /// </summary>
    public sealed class InMemoryDbContext
    {
        public List<User> Users { get; set; } = new List<User>();
    }

    /// <summary>
    /// ユーザークラス
    /// </summary>
    public sealed class User
    {
        public string? Name { get; set; }
    }
 }