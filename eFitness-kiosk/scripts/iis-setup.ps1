# PowerShell script to automate IIS configuration for eFitness-Kiosk application
# This script assumes IIS and .NET Hosting Bundle are already installed.
# It also assumes the React.js app is built and .NET Core BFF is published.

# --- Configuration Variables ---
$BFF_APP_NAME = "eFitnessKioskApi"
$BFF_PHYSICAL_PATH = "C:\inetpub\wwwroot\eFitnessKioskApi" # Path where BFF is published
$BFF_PORT = 80
$BFF_HOSTNAME = "api.efitness-kiosk.local" # Optional, set to $null if not used

$FRONTEND_APP_NAME = "eFitnessKioskFrontend"
$FRONTEND_PHYSICAL_PATH = "C:\inetpub\wwwroot\eFitnessKioskFrontend" # Path where React build is copied
$FRONTEND_PORT = 81
$FRONTEND_HOSTNAME = "app.efitness-kiosk.local" # Optional, set to $null if not used

# --- Functions ---
function Create-AppPool {
    param (
        [string]$Name
    )
    Write-Host "Creating Application Pool: $Name"
    try {
        Add-AppPool -Name $Name -PassThru | Set-ItemProperty -Name managedRuntimeVersion -Value "No Managed Code"
        Set-ItemProperty -Path IIS:\AppPools\$Name -Name enable32BitAppOnWin64 -Value $false
        Write-Host "Application Pool '$Name' created successfully."
    } catch {
        Write-Warning "Failed to create Application Pool '$Name'. It might already exist. Error: $($_.Exception.Message)"
    }
}

function Create-Website {
    param (
        [string]$Name,
        [string]$PhysicalPath,
        [int]$Port,
        [string]$AppPoolName,
        [string]$Hostname = $null
    )
    Write-Host "Creating Website/Application: $Name"
    try {
        $binding = "http":"$Port":$Hostname
        if ($Hostname) {
            New-Website -Name $Name -PhysicalPath $PhysicalPath -Port $Port -HostHeader $Hostname -ApplicationPool $AppPoolName
        } else {
            New-Website -Name $Name -PhysicalPath $PhysicalPath -Port $Port -ApplicationPool $AppPoolName
        }
        Write-Host "Website/Application '$Name' created successfully."
    } catch {
        Write-Warning "Failed to create Website/Application '$Name'. It might already exist or port is in use. Error: $($_.Exception.Message)"
    }
}

function Configure-SpaRewrite {
    param (
        [string]$SiteName,
        [string]$PhysicalPath
    )
    Write-Host "Configuring URL Rewrite for SPA in site: $SiteName"
    $webConfigContent = @"
<?xml version="1.0" encoding="UTF-8"?>
<configuration>
  <system.webServer>
    <rewrite>
      <rules>
        <rule name="React Routes" stopProcessing="true">
          <match url=".*" />
          <conditions logicalGrouping="MatchAll">
            <add input="{REQUEST_FILENAME}" matchType="IsFile" negate="true" />
            <add input="{REQUEST_FILENAME}" matchType="IsDirectory" negate="true" />
            <add input="{REQUEST_URI}" pattern="^/(api)" negate="true" />
          </conditions>
          <action type="Rewrite" url="/index.html" />
        </rule>
      </rules>
    </rewrite>
  </system.webServer>
</configuration>
"@

    $webConfigPath = Join-Path $PhysicalPath "web.config"
    try {
        $webConfigContent | Out-File -FilePath $webConfigPath -Encoding UTF8
        Write-Host "web.config created/updated for SPA rewrite in $SiteName."
    } catch {
        Write-Error "Failed to create/update web.config for SPA rewrite. Error: $($_.Exception.Message)"
    }
}

# --- Execution ---

# Ensure IIS Administration module is available
Import-Module WebAdministration -ErrorAction SilentlyContinue
if (-not (Get-Module -ListAvailable -Name WebAdministration)) {
    Write-Error "IIS WebAdministration module not found. Please ensure IIS Management Tools are installed."
    exit 1
}

# 1. Configure Backend (BFF)
Write-Host "\n--- Configuring Backend (BFF) ---"
Create-AppPool -Name "$($BFF_APP_NAME)Pool"
Create-Website -Name $BFF_APP_NAME -PhysicalPath $BFF_PHYSICAL_PATH -Port $BFF_PORT -AppPoolName "$($BFF_APP_NAME)Pool" -Hostname $BFF_HOSTNAME

# 2. Configure Frontend (React.js SPA)
Write-Host "\n--- Configuring Frontend (React.js SPA) ---"
# For frontend, we can reuse DefaultAppPool or create a new one if needed
# Create-AppPool -Name "$($FRONTEND_APP_NAME)Pool"
Create-Website -Name $FRONTEND_APP_NAME -PhysicalPath $FRONTEND_PHYSICAL_PATH -Port $FRONTEND_PORT -AppPoolName "DefaultAppPool" -Hostname $FRONTEND_HOSTNAME
Configure-SpaRewrite -SiteName $FRONTEND_APP_NAME -PhysicalPath $FRONTEND_PHYSICAL_PATH

Write-Host "\n--- IIS Configuration Complete ---"
Write-Host "Please ensure your BFF is published to '$BFF_PHYSICAL_PATH' and React build to '$FRONTEND_PHYSICAL_PATH'."
Write-Host "Remember to configure CORS in your BFF's Program.cs to allow requests from your frontend URL."
Write-Host "Update your React app's .env with REACT_APP_API_URL pointing to your BFF (e.g., http://$($BFF_HOSTNAME)/api or http://localhost:$($BFF_PORT)/api)."
