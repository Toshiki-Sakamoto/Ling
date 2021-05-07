![UnityTestRunner](https://github.com/SakaToshi/Ling/workflows/UnityTestRunner/badge.svg?branch=master)

# Ling
風来のシレンのようなローグライクゲーム開発

ジャンルは昔ながらのローグライクゲーム
また、UniRx, UniTask, Zenject といったUnity開発に利用できる便利なライブラリを使用した実験的なゲーム開発を行うプロジェクト

## Demo
https://user-images.githubusercontent.com/9328751/97722836-73ba1880-1b0e-11eb-91d6-b9ae988fddf9.gif

## Requirement

* Unity2020.3.3f1
* VisualStudio Code
* UniRx
* UniTask
* Zenject

### Manual
「調整中(今はキーボードのqあたりで移動する）」：移動<br>
「スペース」: 一マス前に攻撃

-----
### その他
Play時Nullエラーが大量に出る場合は
Plugins/Adv の中にある Adv.DLLを reimport すると治る (調査中

Bootシーンから起動するのは現在保障していないので、直接BattleSceneを開いて実行して下さい
BattleSceneに必要なシーンは起動時に自動で読み込まれるので実行に問題ありません
