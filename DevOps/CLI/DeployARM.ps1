Connect-AzureRmAccount -Tenant "33479bdb-c908-45a7-95ab-ecb0bdb71593" -SubscriptionId "a4f8dcd6-87d0-4b57-a907-1b21b0278071"

$ResouceGroupName = "WMMP" 
$Content = "https://raw.githubusercontent.com/Weloom/WMMP/develop/Infrastructure/IntegrationAccount/integrationaccount.json"

New-AzureRmResourceGroupDeployment -ResourceGroupName $ResouceGroupName -TemplateUri $Content
