
<#
Connect-AzureRmAccount -Tenant "33479bdb-c908-45a7-95ab-ecb0bdb71593" -SubscriptionId "a4f8dcd6-87d0-4b57-a907-1b21b0278071"
$MapContent = "https://raw.githubusercontent.com/Weloom/WMMP/develop/Infrastructure/IntegrationAccount/Maps/convertDiigoResultToEntriesList/convertDiigoResultToEntriesList.liquid"
New-AzureRmIntegrationAccountMap -ResourceGroupName "WMMP" -Name "wmmp-ia" -MapName "IntegrationAccountMap47" -MapDefinition $MapContent
#>



 Connect-AzureRmAccount -Tenant "33479bdb-c908-45a7-95ab-ecb0bdb71593" -SubscriptionId "a4f8dcd6-87d0-4b57-a907-1b21b0278071"

$IntegrationAccountName = "wmmp-ia"
$ResouceGroupname = "WMMP" 
$ResourceLocation = "North Europe"
$ResourceName = "xxxx" 

$Content = Invoke-WebRequest -Uri "https://raw.githubusercontent.com/Weloom/WMMP/develop/Infrastructure/IntegrationAccount/Maps/convertDiigoResultToEntriesList/convertDiigoResultToEntriesList.liquid"
Write-Host $Content

$PropertiesObject = @{
    mapType = "liquid"
    content = "$Content"
    contentType = "text/plain"
}
New-AzureRmResource -Location $ResourceLocation -PropertyObject $PropertiesObject -ResourceGroupName $ResouceGroupname -ResourceType Microsoft.Logic/integrationAccounts/maps -ResourceName "$IntegrationAccountName/$ResourceName" -ApiVersion 2016-06-01 -Force
New-AzureRmResourceGroupDeployment 


