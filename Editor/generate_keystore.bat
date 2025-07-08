@echo off
setlocal

:: === 自定义参数 ===
set KEYSTORE_NAME=ZKZP_Release.keystore
set ALIAS_NAME=zkzp_alias
set STOREPASS=DP9925
set KEYPASS=DP9925
set VALIDITY=18250
set DNAME="CN=FuYu,O=ZKZP,OU=G2,L=Tianjin,ST=Tianjin,C=CN"

:: === JDK 路径（Unity 自带） ===
set JDK_PATH="D:\Unity3d\2021.3.39f1c1\Editor\Data\PlaybackEngines\AndroidPlayer\OpenJDK\bin\keytool.exe"

:: === 生成 keystore ===
%JDK_PATH% -genkey -v -keystore %KEYSTORE_NAME% -alias %ALIAS_NAME% -keyalg RSA -keysize 2048 -validity %VALIDITY% -storepass %STOREPASS% -keypass %KEYPASS% -dname %DNAME%

echo.
echo  Keystore Generator Complete：%KEYSTORE_NAME%
pause
