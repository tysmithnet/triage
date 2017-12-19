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
$awsAccessKeyId = 'AWS_ACCESS_KEY_ID'
$awsSecretKey = 'AWS_SECRET_KEY'

Set-AWSCredential -AccessKey $awsAccessKeyId -SecretKey $awsSecretKey

cd C:\users\Administrator\Documents
WriteLog("Cloning triage")
git clone -b $branch --recursive https://github.com/tysmithnet/triage.git triage 2>&1 | out-null # hack because git clone reports success to stderror

if(-not (Test-Path .\triage\Triage))
{
	WriteLog("Failed to clone git project")
	exit
}
cd triage\Triage

WriteLog("Running nuget restore")
nuget restore

WriteLog("Building solution")
devenv /build $debugRelease Triage.sln
cd ".\Triage.Mortician\bin\$debugRelease"

WriteLog("Configuring mortician")
./Triage.Mortician.exe config `
	-k `
		"upload-excel-to-s3" `
		"excel-bucket-id" `
		"aws-access-key-id"`
		"aws-secret-key"`
	-v `
		"true" `
		"$reportbucket"`
		"$awsAccessKeyId"`
		"$awsSecretKey"

WriteLog("Downloading file")
Read-S3Object -BucketName $dumpbucket -Key $key -File C:\Temp\$key

WriteLog("Running mortician on dump")
./Triage.Mortician.exe run -d "C:\Temp\$key"
WriteLog("Complete")