＜32bit版ExBSecurity.dllのレジストリ登録方法＞
あらかじめ、dllファイルを下記の方法で、追加処理を行ってください。
なお、OSによって処理内容が異なりますので、ご注意下さい。
------------------------------------------------------
「OS：32bit版」
１．DLLを以下にコピーする。
C:\Windows\System32\ExBSecurity.dll

２．DLLをレジストリに登録する。
C:\Windows\System32\cmd.exeを立ち上げる。
※以下のコマンドを入力し実行。
regsvr32 ExBSecurity.dll
------------------------------------------------------
「OS：64bit版」
１．DLLを以下にコピーする。
C:\Windows\SysWOW64\ExBSecurity.dll

２．DLLをレジストリに登録する。
C:\Windows\SysWOW64\cmd.exeを立ち上げる。
※以下のコマンドを入力し実行。
regsvr32 ExBSecurity.dll

*******************************************************************************
レジストリ登録でエラーが出た時は？？
------------------------------------------------------
登録エラーが発生するのは32bitのDLLを64bitのregsvr32で登録したためです。
64bitWindowsの場合、System32フォルダは、名前がSystem32のくせに
中に入っているのは64bitのモジュールです。
32bitのモジュールはSysWOW64フォルダにあります。
(逆だろ！と思いますよね？私もそう思います。
頭悪いです。)
 
さて、スタートメニューからコマンドプロンプトを開くと、
64bit版のコマンドプロンプトが開きます。
ここでregsvr32と打つと、名前が32のくせに
64bit版のregsvr32が実行されます。

このregsvr32で登録できるのは64bitコンパイルされたdllだけで、
32bitのdllは登録できないのです。

32bitのdllを登録するためには32bitのコマンドプロンプトから
32bitのregsvr32を実行する必要があります。

これらはSysWOW64の中にありますので、
SysWOW64の中のcmd.exeで32bitのコマンドプロンプトを開き、
SysWOW64の中の32bit版のregsvr32でdllを登録すればいいのです。
*******************************************************************************
