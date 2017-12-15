$n = 1
function WriteLog([string]$message)
{
	Write-EventLog -LogName Application -Source Triage -EventId $n -Message $message
    $n = $n + 1
}

try
{
	New-EventLog -LogName application -Source Triage 2> $null
}
catch
{
	# don't care if it already exists
}

WriteLog("Beginning startup script")
$key = "HelloWorld.exe_170805_215723.dmp"
Import-Module AWSPowershell
cd C:\users\Administrator\Documents
WriteLog("Cloning triage")
git clone https://github.com/tysmithnet/triage triage 2>&1 | out-null # hack because git clone reports success to stderror
cd triage\Triage
WriteLog("Running nuget restore")
nuget restore
WriteLog("Building solution")
devenv /build Debug Triage.sln
cd .\Triage.Mortician\bin\Debug
WriteLog("Getting dump from S3 bucket", 5)
Read-S3Object -BucketName artifacts.triage -Key $key -File C:\Temp\$key
WriteLog("Running mortician on dump")
./Triage.Mortician.exe -d "C:\Temp\$key"