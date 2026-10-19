using System;
using System.Linq;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace LLib.GameUI;

public static class LAddon
{
    private const int UnitListCount = 18;

    public static unsafe AtkUnitBase* GetAddonById(uint id)
    {
        // AtkStage.Instance() 是 [StaticAddress(..., isPointer: true)]：只有「特徵碼沒解析到」才會
        // 擲例外，解析到但遊戲尚未建好 AtkStage 時回傳的是存在該位址的 null 指標；
        // RaptureAtkUnitManager 又是 AtkStage 上的純指標欄位。兩層都沒判空的話，
        // 下面的 unitManager->Count 與 Entries 就是對垃圾位址解參考。
        var stage = AtkStage.Instance();
        if (stage == null)
        {
            return null;
        }

        var raptureAtkUnitManager = stage->RaptureAtkUnitManager;
        if (raptureAtkUnitManager == null)
        {
            return null;
        }

        var unitManagers = &raptureAtkUnitManager->AtkUnitManager.DepthLayerOneList;
        for (var i = 0; i < UnitListCount; i++)
        {
            var unitManager = &unitManagers[i];
            foreach (var j in Enumerable.Range(0, Math.Min(unitManager->Count, unitManager->Entries.Length)))
            {
                var unitBase = unitManager->Entries[j].Value;
                if (unitBase != null && unitBase->Id == id)
                {
                    return unitBase;
                }
            }
        }

        return null;
    }

    public static unsafe bool TryGetAddonByName<T>(this IGameGui gameGui, string addonName, out T* addonPtr)
        where T : unmanaged
    {
        ArgumentNullException.ThrowIfNull(gameGui);
        ArgumentException.ThrowIfNullOrEmpty(addonName);

        var a = gameGui.GetAddonByName(addonName);
        if (!a.IsNull)
        {
            addonPtr = (T*)a.Address;
            return true;
        }
        else
        {
            addonPtr = null;
            return false;
        }
    }

    public static unsafe bool IsAddonReady(AtkUnitBase* addon)
    {
        // GetAddonById 與 TryGetAddonByName 兩個入口都會給出 null，這裡是它們最常見的下游；
        // 判空後 fail-closed 回 false，語意就是「還沒就緒」。
        return addon != null && addon->IsVisible && addon->UldManager.LoadedState == AtkLoadState.Loaded;
    }
}
