# PDFDeSecureDroid
 
由Windows版的PDFDeSecure启发，使用相同的实现方式，但基于MAUI开发，支持安卓  
一上午搓基础功能，一下午解决AOT问题  

理论可以移植到iOS，但我没有mACos设备

## 使用方式
下载Release内安装包或自行构建

## 目标设备
Android 10以上的ARM64设备  
自行构建可支持其他架构，理论全架构支持因为没有原生库  
理论上可以降低到最低Android 5以上，但我没有测试，先一刀切拉个最低10再说我最低的设备都11了  

## 构建环境
- Visual Studio 2022 Preview
- .NET SDK 8.0

## 致谢
- [PDFDeSecure](https://github.com/abatsakidis/PDFDeSecure)  
- [PDFSharp](https://docs.pdfsharp.net/)

## PS
.NET的NativeAOT还有很长的路要走，光是Debug不开AOT能跑Release开AOT就崩就解决了我几个小时捏麻麻滴  