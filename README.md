# LLib

Dalamud 插件共用函式庫，由 Liza Carvelli 開發（原用於 `Gearsetter` 插件），提供職業/裝備欄位列舉
（`EClassJob`、`ETerritoryIntendedUse`）、原生 UI addon 存取（`LAddon`、`LAtkValue`）、裝備數值計算
（`Gear/GearStatsCalculator`）、ImGui 視窗基底（`LWindow`）與商店介面（`Shop`）等共用元件。

## 台服 fork 的目的

跟隨艦隊釘 API13 / net9；並針對台服環境做了兩項實質修改（非僅版本追隨）：

- **`EClassJob.ToFriendlyString()` 改回傳台服官方繁體中文職業名**（原版回傳英文），
  42 個職業名稱以台服 EXD dump 的 `ClassJob.csv` 為準逐字校對（例如 row 26 巴術士，
  不是陸服慣用的「秘術士」）。
- **`LAddon.GetAddonById` / `IsAddonReady` 補上判空防護**：`AtkStage.Instance()` 與
  `RaptureAtkUnitManager` 都可能在遊戲尚未建好對應 UI 時回傳存在的 null 指標，原版沒有判空，
  會對垃圾位址解參考；改為兩層判空、fail-closed 回 `null` / `false`。

## 與上游的差異

上述兩項改動，加上方案檔的 Release 組態原本對到 Debug 已改正；其餘結構與上游一致。

## 誰在用它

目前只有 **`Gearsetter`** 一個插件 repo 使用（子模組 + `ProjectReference`）。

---

上游原始碼：<https://github.com/alydevs/LLib>
