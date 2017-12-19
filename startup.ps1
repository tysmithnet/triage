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
$key = "MEMORY_DUMP"
$dumpbucket = "DUMP_BUCKET"
$reportbucket = "REPORT_BUCKET"
$branch = 'GIT_BRANCH'
$debugRelease = 'DEBUG_RELEASE'
Import-Module AWSPowershell
cd C:\users\Administrator\Documents
WriteLog("Cloning triage")
git clone -b $branch --recursive https://github.com/tysmithnet/triage.git triage 2>&1 | out-null # hack because git clone reports success to stderror

if(-not (Test-Path C:\Temp\$key))
{
	WriteLog("Failed to clone git project")
}
cd triage\Triage
WriteLog("Running nuget restore")
nuget restore
WriteLog("Building solution")
devenv /build $debugRelease Triage.sln
cd ".\Triage.Mortician\bin\$debugRelease"
WriteLog("Getting dump from S3 bucket")
Read-S3Object -BucketName $dumpbucket -Key $key -File C:\Temp\$key

if(-not (Test-Path C:\Temp\$key))
{
	WriteLog("Failed to get memory dump.. retrying")
	for($i = 1; $i -le 5; $i++)
	{
		WriteLog("Retry Attempt $n")
		Read-S3Object -BucketName $dumpbucket -Key $key -File C:\Temp\$key
		if(-not (Test-Path C:\Temp\$key))
		{
			Start-Sleep -Seconds $i
			continue
		}
		else
		{
			break
		}
	}

	if(-not (Test-Path C:\Temp\$key))
	{
		WriteLog("Could not download file dumpfile from s3 bucket. Exiting.")
		exit
	}
}

WriteLog("Configuring mortician")
./Triage.Mortician.exe config `
	-k `
		"upload-excel-to-s3" `
		"excel-bucket-id" `
	-v `
		"true" `
		"$reportbucket"

WriteLog("Running mortician on dump")
./Triage.Mortician.exe run -d "C:\Temp\$key"