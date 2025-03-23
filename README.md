# 資料庫建置說明

本專案使用 Entity Framework Core 進行資料庫版本管理，透過純語法即可在本機進行資料庫建置與遷移操作。

---

## 🔧 Local 資料庫建置步驟

### ✅ 方法一：使用 Visual Studio 套件管理器主控台

1. 開啟 **套件管理器主控台 (Package Manager Console)**
2. 將 **預設專案設為 `DAL`**
3. 執行以下指令：

```powershell
Add-Migration InitialCreate
Update-Database
```

---

### ✅ 方法二：使用命令列 (CMD / Terminal)

1. 進入 `CathayOnlinePractice` 資料夾根目錄
2. 執行以下指令：

```bash
dotnet ef migrations add InitialCre --project DAL --startup-project WebApi
dotnet ef database update --project DAL --startup-project WebApi
```

---

## 📄 其他說明

### 1️⃣ 產出從首次到目前所有 Migration 的 SQL 檔案：

```powershell
Script-Migration -Output .\DAL\SqlScripts\FullSchema.sql
```

---

### 2️⃣ 產出特定版本之間的 SQL 檔案：

```powershell
Script-Migration -From {前一版本} -To {目標版本} -Output .\DAL\SqlScripts\{檔名}.sql
```

📌 範例：

```powershell
Script-Migration -From InitialCreate -To AddOrderTable -Output .\DAL\SqlScripts\OrderTableOnly.sql
```

---

如需更多 EF Core CLI 語法說明，可參考官方文件：[https://docs.microsoft.com/ef/core/cli/](https://docs.microsoft.com/ef/core/cli/)

---

## 🛠️ 額外實作功能說明

### 📘 1. API 呼叫與外部 API 的 Request/Response Log

- 所有進入 API 的 Request 及 Response，以及對外部 API 的請求與回應內容，皆會被記錄。
- 使用 **NLog** 作為日誌系統，將記錄輸出至指定日誌檔案。
- 可擴充記錄內容（如 Headers、StatusCode、ElapsedTime 等）。

---

### ❗ 2. Error Handling 統一處理 API Response

- 使用自訂的 `ExceptionFilterAttribute`。
- 處理所有未捕捉的例外，回傳統一格式的 API 回應（包含錯誤代碼、訊息等）。
- 確保前端或呼叫端收到一致的錯誤資訊。

---

### 🔐 3. 加解密技術應用（AES / RSA）

- 實作 AES 加解密工具類別，提供簡單介面呼叫。
- 加解密所需的 `Key` 與 `IV` 可透過環境變數進行設定。
- 保持加解密邏輯安全且可配置，便於依環境切換。
