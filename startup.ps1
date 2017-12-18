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
# todo: need to check for errors
WriteLog("Beginning startup script")
$key = "HelloWorld.exe_170805_215723.dmp"
$bucketname = "artifacts.triage";
Import-Module AWSPowershell
cd C:\users\Administrator\Documents
WriteLog("Cloning triage")
git clone -b feature/end_to_end --recursive -j8 https://github.com/tysmithnet/triage.git triage 2>&1 | out-null # hack because git clone reports success to stderror
cd triage\Triage
WriteLog("Running nuget restore")
nuget restore
WriteLog("Building solution")
devenv /build Debug Triage.sln
cd .\Triage.Mortician\bin\Debug
WriteLog("Getting dump from S3 bucket")
Read-S3Object -BucketName $bucketname -Key $key -File C:\Temp\$key
WriteLog("Running mortician on dump")
./Triage.Mortician.exe config `
	-k `
		"upload-excel-to-s3" `
		"excel-bucket-id" `
	-v `
		"true" `
		"reports.triage"

./Triage.Mortician.exe run -d "C:\Temp\$key"