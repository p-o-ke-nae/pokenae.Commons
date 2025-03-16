# pokenae.Commons

## 概要

`pokenae.Commons`プロジェクトは、共通のサービス、リポジトリ、エンティティ、DTO、マッピングプロファイル、データベースコンテキスト、フィルタなどを提供します。このプロジェクトは、他のプロジェクトで再利用可能な汎用的な機能を提供することを目的としています。

## 参照方法

以下の手順に従って、`pokenae.Commons`プロジェクトを他のプロジェクトで参照し、利用することができます。

1. **プロジェクト参照の追加**:
   - Visual Studio 2022で、`pokenae.Commons`プロジェクトを右クリックし、「プロジェクトの依存関係」を選択します。
   - 依存関係を追加したいプロジェクトを選択し、「OK」をクリックします。

2. **NuGetパッケージの復元**:
   - `pokenae.Commons`プロジェクトで使用されているNuGetパッケージを復元します。ソリューションエクスプローラーでプロジェクトを右クリックし、「NuGet パッケージの復元」を選択します。

3. **名前空間のインポート**:
   - `pokenae.Commons`プロジェクトのクラスやインターフェースを使用するために、必要な名前空間をインポートします。例えば、以下のようにインポートします。

   using pokenae.Commons.Filters;
   using pokenae.Commons.Data;
   using pokenae.Commons.DTOs;
   using pokenae.Commons.Entities;
   using pokenae.Commons.Mappings;
   using pokenae.Commons.Repositories;
   using pokenae.Commons.Repositories.impl;
   using pokenae.Commons.Services.Application;
   using pokenae.Commons.Services.Application.impl;


## 使用目的と意図

### `pokenae.Commons.Controllers.BaseController`

`BaseController`は、共通のコントローラ機能を提供する抽象クラスです。APIアクセスのログ記録やエラーハンドリングなどの共通機能を実装しています。

### `pokenae.Commons.Filters.ApiAccessFilter`

`ApiAccessFilter`は、ポケなえが管理するAPIに対してアクセス権限をチェックするフィルタです。特定のAPIエンドポイントにアクセスする前に、アクセス権限を確認します。

### `pokenae.Commons.Data.ApplicationDbContext`

`ApplicationDbContext`は、データベースコンテキストクラスです。エンティティの保存、更新、削除などのデータベース操作を管理します。

### `pokenae.Commons.DTOs.BaseDto`

`BaseDto`は、データ転送オブジェクトの基底クラスです。エンティティのデータを転送するための共通プロパティを提供します。

### `pokenae.Commons.Entities.BaseEntity`

`BaseEntity`は、エンティティの基底クラスです。作成者、作成日時、更新者、更新日時、削除者、削除日時などの共通プロパティを提供します。

### `pokenae.Commons.Mappings.BaseMappingProfile`

`BaseMappingProfile`は、AutoMapperのマッピングプロファイルの基底クラスです。エンティティとDTOのマッピング設定を行います。

### `pokenae.Commons.Repositories.IEntityRepository`

`IEntityRepository`は、エンティティのリポジトリインターフェースです。エンティティのCRUD操作を定義します。

### `pokenae.Commons.Repositories.impl.EntityRepository`

`EntityRepository`は、`IEntityRepository`の実装クラスです。エンティティのCRUD操作を実装します。

### `pokenae.Commons.Services.Application.IApplicationService`

`IApplicationService`は、アプリケーションサービスのインターフェースです。DTOを使用したビジネスロジックを定義します。

### `pokenae.Commons.Services.Application.impl.ApplicationService`

`ApplicationService`は、`IApplicationService`の実装クラスです。DTOを使用したビジネスロジックを実装します。

## 実際の使用例

### `ApiAccessFilter`の使用例

`ApiAccessFilter`をASP.NET Coreのフィルタとして使用する例を示します。

public void ConfigureServices(IServiceCollection services) { services.AddHttpClient(); services.AddScoped<ApiAccessFilter>();
services.AddControllers(options =>
{
    options.Filters.Add<ApiAccessFilter>();
});
}

### `ApplicationDbContext`の使用例

`ApplicationDbContext`を設定し、依存性注入する例を示します。

public void ConfigureServices(IServiceCollection services) { services.AddDbContext<ApplicationDbContext<YourDbContext>>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
services.AddHttpContextAccessor();
}

### `EntityRepository`の使用例

`EntityRepository`を使用してエンティティのCRUD操作を行う例を示します。

public class YourService { private readonly IEntityRepository<YourEntity> _repository;
public YourService(IEntityRepository<YourEntity> repository)
{
    _repository = repository;
}

public void AddEntity(YourDto dto)
{
    var entity = new YourEntity
    {
        // DTOからエンティティへのマッピング
    };
    _repository.Add(entity);
}

public YourDto GetEntity(Guid id)
{
    var entity = _repository.Find(e => e.Id == id);
    return new YourDto
    {
        // エンティティからDTOへのマッピング
    };
}
}


## 拡張方法

### `IEntityRepository`の拡張

`IEntityRepository`を拡張するには、新しいメソッドをインターフェースに追加します。


public interface IEntityRepository<T> where T : BaseEntity 
{ 
    // 既存のメソッド
    // 新しいメソッド
    Task<T> FindAsync(Func<T, bool> predicate);
}


### `EntityRepository`の拡張

`EntityRepository`を拡張するには、`IEntityRepository`で定義した新しいメソッドを実装します。


public class EntityRepository<T> : IEntityRepository<T> where T : BaseEntity 
{ 
    // 既存のメソッド
    public async Task<T> FindAsync(Func<T, bool> predicate)
    {
        return await Task.Run(() => _context.Set<T>().FirstOrDefault(predicate));
    }
}


### `IEntityService`の拡張

`IEntityService`を拡張するには、新しいメソッドをインターフェースに追加します。


public interface IEntityService<T> where T : BaseEntity 
{ 
    // 既存のメソッド
    // 新しいメソッド
    Task<T> FindAsync(Func<T, bool> predicate);
}


### `EntityService`の拡張

`EntityService`を拡張するには、`IEntityService`で定義した新しいメソッドを実装します。


public class EntityService<T> : IEntityService<T> where T : BaseEntity 
{ 
    // 既存のメソッド
    public async Task<T> FindAsync(Func<T, bool> predicate)
    {
        return await repository.FindAsync(predicate);
    }
}


### `BaseMappingProfile`の拡張

`BaseMappingProfile`を拡張するには、新しいマッピング設定を追加します。


public class CustomMappingProfile : BaseMappingProfile 
{ 
    public CustomMappingProfile() 
    { 
        CreateMap<YourEntity, YourDto>(); CreateMap<YourDto, YourEntity>(); 
    } 
}


## まとめ

`pokenae.Commons`プロジェクトは、共通の機能を提供することで、他のプロジェクトでのコードの再利用性を高め、開発効率を向上させることを目的としています。各クラスやインターフェースを適切に参照し、利用することで、共通のビジネスロジックやデータ操作を簡単に実装することができます。
