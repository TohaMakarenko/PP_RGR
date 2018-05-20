# cmd.exe /c start "dotnet" /affinity 7 .\PerfomanceTest.dll 1000
dotnet.exe .\PerfomanceTest.dll | Out-File -FilePath .\result.txt
$lastExitCode