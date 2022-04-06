# powershell.exe -ExecutionPolicy Bypass -File .\Service.ps1

$action = $args[0].ToString().ToLower()
$serviceName = "SapConnectionTest"
$serviceDesc = "Test connection to SAP each 5 minutes and triggers an alarm if connection fail."
$executable = "SapConnectionTest.exe"
$path = "C:\Services\SapConnectionTest\"

$service = Get-Service -Name $serviceName -ErrorAction SilentlyContinue

switch ($action) {
    "stop" {
          if ($null -ne $service) {
            Stop-Service -Name $serviceName
        }
    }
    "start" {
        if ($null -eq $service) {
            New-Service -Name $serviceName -StartupType Automatic -Description $serviceDesc -BinaryPathName $path$executable
            Start-Service -Name $serviceName
        }
        else {
            Start-Service -Name $serviceName
        }
    }
}
