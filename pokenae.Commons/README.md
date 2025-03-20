# pokenae.Commons

## 概要

`pokenae.Commons`プロジェクトは、共通のサービス、リポジトリ、エンティティ、DTO、マッピングプロファイル、データベースコンテキスト、フィルタなどを提供します。このプロジェクトは、他のプロジェクトで再利用可能な汎用的な機能を提供することを目的としています。

本プロジェクトはプレゼンテーション層、アプリケーション層、ドメイン層、インフラストラクチャ層の4層で構成されるポケなえのプロジェクトを支援するものです。

## 各層の責務

### プレゼンテーション層

- ユーザーインターフェースを提供し、ユーザーからの入力を受け付けます。
- アプリケーション層のDTOを使用してデータを表示します。

### アプリケーション層

- ビジネスロジックを実行し、プレゼンテーション層とドメイン層の間のデータの移動を管理します。
- アプリケーション層のDTOは、アプリケーション層からプレゼンテーション層へのデータの移動を行い、`ApplicationDto`を継承します。

### ドメイン層

- ビジネスルールとビジネスロジックを実現します。
- ドメイン層のエンティティは`BaseEntity`を継承します。
- ドメイン層のValueObjectは`BaseValueObject`を継承します。
- ドメイン層のサービスはビジネスルール、ビジネスロジックを実現するためのメインのサービスとリポジトリと1:1で存在するinternalなサブサービスで構成され、サブサービスのインターフェースは`IEntityService`を実装し、その実装は`EntityService`を継承します。
- ドメイン層のDTOはインフラストラクチャ層からドメイン層へのデータの移動を行い、`InfrastructureDto`を継承します。
- ドメイン層のリポジトリはDBテーブルなどと1:1で存在し、`IBaseRepository`を実装します。

### インフラストラクチャ層

- データベースや外部サービスとの通信を管理します。
- インフラストラクチャ層の`DbContext`は`ApplicationDbContext`を継承します。
- インフラストラクチャ層のリポジトリの実装は`BaseRepository`を継承します。

## マッピング

- 各DTOとエンティティのマッピングを行うmapperは`BaseMappingProfile`を継承します。

## フィルタ

- `pokenae-UserManager(pUM)`を用いた権限管理を行う場合は`ApiAccessFilter`を有効にします。
- `pUM`を用いてアクセスログ管理を行う場合は`LoggingActionFilter`を有効にします。

## 参照方法

以下の手順に従って、`pokenae.Commons`プロジェクトを他のプロジェクトで参照し、利用することができます。

1. **プロジェクト参照の追加**:
   - Visual Studio 2022で、`pokenae.Commons`プロジェクトを右クリックし、「プロジェクトの依存関係」を選択します。
   - 依存関係を追加したいプロジェクトを選択し、「OK」をクリックします。

2. **NuGetパッケージの復元**:
   - `pokenae.Commons`プロジェクトで使用されているNuGetパッケージを復元します。ソリューションエクスプローラーでプロジェクトを右クリックし、「NuGet パッケージの復元」を選択します。

3. **名前空間のインポート**:
   - `pokenae.Commons`プロジェクトのクラスやインターフェースを使用するために、必要な名前空間をインポートします。例えば、以下のようにインポートします。

	- using pokenae.Commons.Filters; using pokenae.Commons.Data; using pokenae.Commons.DTOs; using pokenae.Commons.Entities; using pokenae.Commons.Mappings; using pokenae.Commons.Repositories; using pokenae.Commons.Repositories.impl; using pokenae.Commons.Services.Application; using pokenae.Commons.Services.Application.impl;

	
## まとめ

`pokenae.Commons`プロジェクトは、共通の機能を提供することで、他のプロジェクトでのコードの再利用性を高め、開発効率を向上させることを目的としています。各クラスやインターフェースを適切に参照し、利用することで、共通のビジネスロジックやデータ操作を簡単に実装することができます。
