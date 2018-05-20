ForEach($PROCESS in GET-PROCESS dotnet) { 
    $PROCESS.ProcessorAffinity=7
}
dotnet.exe .\PerfomanceTest.dll 1000 | Out-File -FilePath .\result.txt

$lastExitCode