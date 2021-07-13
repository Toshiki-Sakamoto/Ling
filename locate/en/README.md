![UnityTestRunner](https://github.com/SakaToshi/Ling/workflows/UnityTestRunner/badge.svg?branch=master)[](https://gitlocalize.com/repo/6223/en?utm_source=badge)![git localized](https://gitlocalize.com/repo/6223/en/badge.svg)

# Ling

Roguelike game development like Furai Shiren

The genre is old-fashioned roguelike games. Also, a project to develop experimental games using convenient libraries such as UniRx, UniTask, and Zenject that can be used for Unity development.

## Demo

https://user-images.githubusercontent.com/9328751/123117505-c28d3780-d47c-11eb-9170-17844b971533.gif

## Requirement

- Unity2020.3.12f1
- VisualStudio Code
- UniRx
- UniTask
- Zenject v9.2.0
- MessagePipe v1.6.1

### Manual

「調整中(今はキーボードのqあたりで移動する）」：移動<br> 「スペース」: 一マス前に攻撃

---

### Other

If you get a lot of null errors when **playing, reimporting Adv.DLL in Plugins / Adv will fix it (under investigation)**

We do not currently guarantee booting from the Boot scene, so open BattleScene directly and run it.

The scenes required for BattleScene are automatically loaded at startup, so there is no problem with execution.

- Automatic test

Currently it fails because the Unity version of Docker does not match
