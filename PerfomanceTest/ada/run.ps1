$app = Start-Process cmd -Args "/c 2400.exe > result.txt"
$app.ProcessorAffinity = 1