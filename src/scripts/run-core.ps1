Write-Host "🔥🔥🔥 Magic start here: e-University 🔥🔥🔥"

$portPattern = ":3000\s.+LISTENING\s+\d+$"

function Get-PidNumber() {
	$pidNumberPattern = "\d+$"

	$foundProcesses = netstat -ano | findstr :"3000"
	$processes = $foundProcesses | Select-String -Pattern $portPattern
	$firstMatch = $processes.Matches.Get(0).Value

	return [regex]::match($firstMatch, $pidNumberPattern).Value
}

function Get-ServiceApiPath($serviceName) {
	return ".\src\services\EUniversity.$serviceName.Api\EUniversity.$serviceName.Api.csproj"
}

$codePath = (Get-Location).ToString();

$dotnetSolutionPath = $codePath + '\src\services'
$clientAppPath = $codePath + "\src\web"

# Build dotnet app
dotnet build $dotnetSolutionPath

if ($LASTEXITCODE -ne 0) {
	Write-Host "💀 Build .NET APP Finished with error. 💀"
	exit 1
}

$authPath = Get-ServiceApiPath "Authorization"
$gatewayPath = Get-ServiceApiPath "Gateway"
$managerPath = Get-ServiceApiPath "Manager"


Write-Host "*************************🚀 Starting Authorization.Api 🚀*************************"

$authServiceProcess = Start-Process -FilePath dotnet -ArgumentList "watch run --verbosity m --project $authPath" -PassThru
Start-Sleep -Seconds 2

Write-Host "*************************🚀 Starting Manager.Api 🚀*************************"

$managerServiceProcess = Start-Process -FilePath dotnet -ArgumentList "watch run --verbosity m --project $managerPath" -PassThru
Start-Sleep -Seconds 2

Write-Host "*************************🚀 Starting Gateway.Api 🚀*************************"

$gatewayServiceProcess = Start-Process -FilePath dotnet -ArgumentList "watch run --verbosity m --project $gatewayPath" -PassThru
Start-Sleep -Seconds 2

# check to see if the UI is still running, and if so, don't launch another one.
$foundUI = netstat -ano | findstr :"3000"
Write-Host "*************************🔥 Starting College UI 🔥*************************"

If ($foundUI | Select-String -Pattern $portPattern -Quiet) {
	Write-Host "UI is already running on port 3000..."
	$pidNumber = Get-PidNumber
	$webClientProcess = Get-Process -Id $pidNumber
}
Else {
	Write-Host "************************* Starting Next UI *************************"
	Set-Location $clientAppPath
	Start-Process -FilePath npm -ArgumentList "run dev" -PassThru

	Start-Sleep -Seconds 25
	Start-Process "http://localhost:3000" -PassThru
	Set-Location $codePath

	$pidNumber = Get-PidNumber

	$webClientProcess = Get-Process -Id $pidNumber
}

# Stop all process
Read-Host -Prompt '🔥🛑 Press the <ANY> key to quit and kill backend services'

$authServiceProcess | Stop-Process -Force -ErrorAction SilentlyContinue
$managerServiceProcess | Stop-Process -Force -ErrorAction SilentlyContinue
$gatewayServiceProcess | Stop-Process -Force -ErrorAction SilentlyContinue

$webClientProcess | Stop-Process -Force