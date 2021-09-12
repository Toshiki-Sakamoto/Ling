![UnityTestRunner](https://github.com/SakaToshi/Ling/workflows/UnityTestRunner/badge.svg?branch=master)
[![gitlocalized ](https://gitlocalize.com/repo/6223/en/badge.svg)](https://gitlocalize.com/repo/6223/en?utm_source=badge)

# Ling
風来のシレンのようなローグライクゲーム開発

ジャンルは昔ながらのローグライクゲーム
また、UniRx, UniTask, Zenject といったUnity開発に利用できる便利なライブラリを使用した実験的なゲーム開発を行うプロジェクト

## Demo
https://user-images.githubusercontent.com/9328751/123117505-c28d3780-d47c-11eb-9170-17844b971533.gif

## Requirement

* Unity2021.2.Xb
* VisualStudio Code
* UniRx
* UniTask
* Zenject v9.2.0
* MessagePipe v1.6.1

### Manual
「調整中(今はキーボードのqあたりで移動する）」：移動<br>
「スペース」: 一マス前に攻撃

-----
### その他
Play時Nullエラーが大量に出る場合は
**Plugins/Adv の中にある Adv.DLLを reimport すると治る (調査中**

Bootシーンから起動するのは現在保障していないので、直接BattleSceneを開いて実行して下さい

BattleSceneに必要なシーンは起動時に自動で読み込まれるので実行に問題ありません


- 自動テスト

DockerのUnityバージョンが合わないため現状失敗します
