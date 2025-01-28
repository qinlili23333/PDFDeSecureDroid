# PDFDeSecureDroid
 
由Windows版的PDFDeSecure启发，使用相同的实现方式，但基于MAUI开发，支持安卓  
一上午搓基础功能，一下午解决AOT问题  

理论可以移植到iOS，但我没有mACos设备

## 使用方式
ARM64设备可以直接下载Release内安装包
Release内的安装包不保证签名一致性，如果无法覆盖安装请卸载旧版本（反正也没有需要储存的数据不是吗）  
其他架构请参与Google Play内部测试获取  
先点击此链接加入测试群组：https://groups.google.com/g/qinlili_beta  
然后点击此链接获取测试版本：https://play.google.com/apps/testing/moe.qinlili.pdfdesecure  


## 目标设备
Android 10以上的设备  
理论上可以降低到最低Android 5以上，但我没有测试，先一刀切拉个最低10再说我最低的设备都11了  

## 构建环境
- Visual Studio 2022 Preview
- .NET SDK 9.0

## 致谢
- [PDFDeSecure](https://github.com/abatsakidis/PDFDeSecure)  
- [PDFSharp](https://docs.pdfsharp.net/)

## PS
.NET的NativeAOT还有很长的路要走，光是Debug不开AOT能跑Release开AOT就崩就解决了我几个小时捏麻麻滴  