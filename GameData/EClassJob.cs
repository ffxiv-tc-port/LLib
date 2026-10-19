using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace LLib.GameData;

[SuppressMessage("Design", "CA1028", Justification = "uint in Lumina")]
public enum EClassJob : uint
{
    Adventurer = 0,
    Gladiator = 1,
    Pugilist = 2,
    Marauder = 3,
    Lancer = 4,
    Archer = 5,
    Conjurer = 6,
    Thaumaturge = 7,
    Carpenter = 8,
    Blacksmith = 9,
    Armorer = 10,
    Goldsmith = 11,
    Leatherworker = 12,
    Weaver = 13,
    Alchemist = 14,
    Culinarian = 15,
    Miner = 16,
    Botanist = 17,
    Fisher = 18,
    Paladin = 19,
    Monk = 20,
    Warrior = 21,
    Dragoon = 22,
    Bard = 23,
    WhiteMage = 24,
    BlackMage = 25,
    Arcanist = 26,
    Summoner = 27,
    Scholar = 28,
    Rogue = 29,
    Ninja = 30,
    Machinist = 31,
    DarkKnight = 32,
    Astrologian = 33,
    Samurai = 34,
    RedMage = 35,
    BlueMage = 36,
    Gunbreaker = 37,
    Dancer = 38,
    Reaper = 39,
    Sage = 40,
    Viper = 41,
    Pictomancer = 42,
}

public static class EClassJobExtensions
{
    public static bool IsClass(this EClassJob classJob) =>
        classJob is >= EClassJob.Gladiator and <= EClassJob.Thaumaturge
            or EClassJob.Arcanist
            or EClassJob.Rogue
        || classJob.IsCrafter()
        || classJob.IsGatherer();

    public static bool HasBaseClass(this EClassJob classJob) =>
        Enum.GetValues<EClassJob>()
            .Where(x => x.IsClass())
            .Any(x => x.AsJob() == classJob);

    public static EClassJob AsJob(this EClassJob classJob) => classJob switch
    {
        EClassJob.Gladiator => EClassJob.Paladin,
        EClassJob.Marauder => EClassJob.Warrior,
        EClassJob.Pugilist => EClassJob.Monk,
        EClassJob.Lancer => EClassJob.Dragoon,
        EClassJob.Rogue => EClassJob.Ninja,
        EClassJob.Archer => EClassJob.Bard,
        EClassJob.Conjurer => EClassJob.WhiteMage,
        EClassJob.Thaumaturge => EClassJob.BlackMage,
        EClassJob.Arcanist => EClassJob.Summoner,
        _ => classJob,
    };

    public static bool IsTank(this EClassJob classJob) =>
        classJob is EClassJob.Gladiator
            or EClassJob.Paladin
            or EClassJob.Marauder
            or EClassJob.Warrior
            or EClassJob.DarkKnight
            or EClassJob.Gunbreaker;

    public static bool IsHealer(this EClassJob classJob) =>
        classJob is EClassJob.Conjurer
            or EClassJob.WhiteMage
            or EClassJob.Scholar
            or EClassJob.Astrologian
            or EClassJob.Sage;

    public static bool IsMelee(this EClassJob classJob) =>
        classJob is EClassJob.Pugilist
            or EClassJob.Monk
            or EClassJob.Lancer
            or EClassJob.Dragoon
            or EClassJob.Rogue
            or EClassJob.Ninja
            or EClassJob.Samurai
            or EClassJob.Reaper
            or EClassJob.Viper;

    public static bool IsPhysicalRanged(this EClassJob classJob) =>
        classJob is EClassJob.Archer
            or EClassJob.Bard
            or EClassJob.Machinist
            or EClassJob.Dancer;

    public static bool IsCaster(this EClassJob classJob) =>
        classJob is EClassJob.Thaumaturge
            or EClassJob.BlackMage
            or EClassJob.Arcanist
            or EClassJob.Summoner
            or EClassJob.RedMage
            or EClassJob.BlueMage
            or EClassJob.Pictomancer;

    public static bool DealsPhysicalDamage(this EClassJob classJob) =>
        classJob.IsTank() || classJob.IsMelee() || classJob.IsPhysicalRanged();

    public static bool DealsMagicDamage(this EClassJob classJob) =>
        classJob.IsHealer() || classJob.IsCaster();

    public static bool IsCrafter(this EClassJob classJob) =>
        classJob is >= EClassJob.Carpenter and <= EClassJob.Culinarian;

    public static bool IsGatherer(this EClassJob classJob) => classJob is >= EClassJob.Miner and <= EClassJob.Fisher;

    public static string ToFriendlyString(this EClassJob classJob)
    {
        return classJob switch
        {
            EClassJob.Gladiator => "劍術士",
            EClassJob.Pugilist => "格鬥士",
            EClassJob.Marauder => "斧術士",
            EClassJob.Lancer => "槍術士",
            EClassJob.Archer => "弓術士",
            EClassJob.Conjurer => "幻術士",
            EClassJob.Thaumaturge => "咒術士",
            EClassJob.Carpenter => "木工師",
            EClassJob.Blacksmith => "鍛造師",
            EClassJob.Armorer => "甲冑師",
            EClassJob.Goldsmith => "金工師",
            EClassJob.Leatherworker => "皮革師",
            EClassJob.Weaver => "裁縫師",
            EClassJob.Alchemist => "鍊金術師",
            EClassJob.Culinarian => "烹調師",
            EClassJob.Miner => "採掘師",
            EClassJob.Botanist => "園藝師",
            EClassJob.Fisher => "漁師",
            EClassJob.Paladin => "騎士",
            EClassJob.Monk => "武僧",
            EClassJob.Warrior => "戰士",
            EClassJob.Dragoon => "龍騎士",
            EClassJob.Bard => "吟遊詩人",
            EClassJob.WhiteMage => "白魔道士",
            EClassJob.BlackMage => "黑魔道士",
            EClassJob.Arcanist => "巴術士",
            EClassJob.Summoner => "召喚士",
            EClassJob.Scholar => "學者",
            EClassJob.Rogue => "雙劍士",
            EClassJob.Ninja => "忍者",
            EClassJob.Machinist => "機工士",
            EClassJob.DarkKnight => "暗黑騎士",
            EClassJob.Astrologian => "占星術師",
            EClassJob.Samurai => "武士",
            EClassJob.RedMage => "赤魔道士",
            EClassJob.BlueMage => "青魔道士",
            EClassJob.Gunbreaker => "絕槍戰士",
            EClassJob.Dancer => "舞者",
            EClassJob.Reaper => "奪魂者",
            EClassJob.Sage => "賢者",
            EClassJob.Viper => "毒蛇劍士",
            EClassJob.Pictomancer => "繪靈法師",
            _ => classJob.ToString(),
        };
    }
}
