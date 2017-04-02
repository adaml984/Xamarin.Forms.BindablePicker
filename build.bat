@echo off
MsBuild.exe .\Xamarin.Forms.BindablePicker.sln /t:Build /p:Configuration=Release
.\nuget\nuget.exe pack Xamarin.Forms.BindablePicker.nuspec -ForceEnglishOutput