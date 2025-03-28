﻿# pokenae.Commons

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
- ドメイン層のサービスはビジネスルール、ビジネスロジックを実現するためのメインのサービスとリポジトリと1:1で存在するサブサービスで構成され、サブサービスのインターフェースは`IRepositoryService`を実装し、その実装は`RepositoryService`を継承します。
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

## 命名規約

### ドメイン層のDTO、リポジトリ、リポジトリと1:1の関係にあるサブサービス

- **マスタテーブル**:
  - **接頭語**: `M1`
  - **例**: `M1CustomerRepository`, `M1ProductRepository`, `M1CustomerDto`, `M1CustomerService`

- **トランザクションテーブル**:
  - **接頭語**: `T1`
  - **例**: `T1OrderRepository`, `T1InvoiceRepository`, `T1OrderDto`, `T1OrderService`

- **ログテーブル**:
  - **接頭語**: `L1`
  - **例**: `L1ErrorLogRepository`, `L1AccessLogRepository`, `L1ErrorLogDto`, `L1ErrorLogService`

- **設定テーブル**:
  - **接頭語**: `C1`（Configurationの略）
  - **例**: `C1AppSettingsRepository`, `C1UserSettingsRepository`, `C1AppSettingsDto`, `C1AppSettingsService`

- **参照テーブル**:
  - **接頭語**: `R1`（Referenceの略）
  - **例**: `R1CountryRepository`, `R1CurrencyRepository`, `R1CountryDto`, `R1CountryService`

- **履歴テーブル**:
  - **接頭語**: `H1`（Historyの略）
  - **例**: `H1OrderHistoryRepository`, `H1LoginHistoryRepository`, `H1OrderHistoryDto`, `H1OrderHistoryService`

- **一時テーブル**:
  - **接頭語**: `T2`（Temporaryの略）
  - **例**: `T2TempDataRepository`, `T2SessionDataRepository`, `T2TempDataDto`, `T2TempDataService`

- **キュー**:
  - **接頭語**: `Q1`（Queueの略）
  - **例**: `Q1MessageQueueRepository`, `Q1TaskQueueRepository`, `Q1MessageQueueDto`, `Q1MessageQueueService`

- **外部API**:
  - **接頭語**: `E1`（Externalの略）
  - **例**: `E1GoogleApiRepository`, `E1TwitterApiRepository`, `E1GoogleApiDto`, `E1GoogleApiService`

### サービスの命名規約

- **ドメイン層のサービス**:
  - **接尾語**: `Service`
  - **例**: `CustomerService`, `OrderService`

- **アプリケーション層のサービス**:
  - **接尾語**: `UseCase`
  - **例**: `CreateOrderUseCase`, `GetCustomerDetailsUseCase`

## まとめ

`pokenae.Commons`プロジェクトは、共通の機能を提供することで、他のプロジェクトでのコードの再利用性を高め、開発効率を向上させることを目的としています。各クラスやインターフェースを適切に参照し、利用することで、共通のビジネスロジックやデータ操作を簡単に実装することができます。
