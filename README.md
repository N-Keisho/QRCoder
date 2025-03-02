# QRCoder
QRコードを用いて初心者でも簡単にプログラミングを体験できるアプリケーションです。

[UnityRoom](https://unityroom.com/games/qrcoder)で実際に遊ぶことができます。

## 経緯
このゲームは工学院大学のプログラミングサークル「KogCoder」のメンバーが[学園祭](https://hachiojisaionline.wordpress.com/)に向けて開発したプログラミング体験アプリケーションです。
「プログラミングをやったこと・触れたことのない人でも楽しく学ぶことができる」というコンセプトをもとに、カードを並び替えるだけで簡単にコーディングができるようになっています。
初めてのチーム製作のため様々な苦労がありましたが、無事に完成し、当日は小学生から大人の方まで様々な方に楽しんでいただくことができました！ご来場ありがとうございました！


## 説明
カードを並べて、読み取り用サイトにその写真をアップロードしよう！
IDが発行されるので、ステージ画面右上のID欄に入れて実行'▷'だ！
あなたの作ったプログラムが実行されるよ！

## 関連サイト
### 読み取り用サイト
- URL : [https://8sai.kogcoder.com](https://8sai.kogcoder.com)
- userID : KogUser1
- pass : t3PS9mkg

### カード配布先
- URL : [https://drive.google.com/drive/folders/19hNjCJRkBRtTVV3RQvDUYK_HXTp6JdTn](https://drive.google.com/drive/folders/19hNjCJRkBRtTVV3RQvDUYK_HXTp6JdTn)

## ショートカット
### ステージセレクト画面
- T : タイトル画面
- ←→ : 選択ステージ変更
- Enter : ステージ決定

### ステージ画面
- T : タイトル画面
- S : ステージセレクト
- A : リトライ
- PassWord : (私たちのサークル名は...?)

## 仕組み
カードに印刷されたQRコードは数値を表しており、それが一つの命令に対応しています。
例えば「10101」は「一歩進む」に対応しています。サーバ側では写真に含まれたそれらのQRコードをPythonの画像処理ライブラリであるOpenCVを用いて読み取り、作成したソースコードをデータベースに保存します。
保存時に発行されるコードIDをUnityで作成されたアプリに入力すると、サーバにgetリクエストが送信され、サーバはjsonファイルとしてそのソースコードを返します。
Unity側ではjsonファイルのソースコードに従って命令を一つ一つ実行してきます。

## イメージ画像
### タイトル
![TitleImage](https://github.com/N-Keisho/QRCoder/assets/133760530/f74513c4-4266-4a5f-ad1a-ef98068a3a88)
### ステージ選択
![StageSelectImage](https://github.com/N-Keisho/QRCoder/assets/133760530/76ad5fba-2358-4b57-867a-a10ffe2f1d73)
### プレイ画面
![PlayImage](https://github.com/N-Keisho/QRCoder/assets/133760530/7c6d0190-8f8d-41b0-88f9-83661d5b8dcb)
### カード
![CardImage](https://github.com/N-Keisho/QRCoder/assets/133760530/28814fdc-6c8a-4b21-9190-bc1f8a28e67e)
### カードを並べた様子
![LineTheCardsUpImage](https://github.com/N-Keisho/QRCoder/assets/133760530/df1d1d1c-f043-4e85-af8c-678d3ccc0651)
