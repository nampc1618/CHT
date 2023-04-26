$path = (Get-Process -Name 'CHT').Path
Stop-Process -Name CHT
Start-Sleep -Milliseconds 1000
Start-Process -FilePath  $path
