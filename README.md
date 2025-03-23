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
