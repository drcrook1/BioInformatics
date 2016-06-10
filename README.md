# BioInformatics

<a href="https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fdrcrook1%2FBioInformatics%2Fmaster%2FAutomation%2FAzureResourceGroup%2FTemplates%2Fazuredeploy.json" target="_blank">
    <img src="http://azuredeploy.net/deploybutton.png"/>
</a>

<a href="http://armviz.io/#/?load=https%3A%2F%2Fraw.githubusercontent.com%2Fdrcrook1%2FBioInformatics%2Fmaster%2FAutomation%2FAzureResourceGroup%2FTemplates%2Fazuredeploy.json" target="_blank">
    <img src="http://armviz.io/visualizebutton.png"/>
</a>

Solution template for deploying your own bio-informatics solutions with real time data streams, data visualizations
as well as machine learning.  This solution focuses on the Microsoft Band v2, however any device that produces
Biological information is applicable.

Before contributing to this project, please watch the following video to understand the very high 
level generic form of the architecture: https://aka.ms/iotanalytics 

Each folder is a compartmentalized flavor of the project.  The project delivers to
1. Web
2. iOS, Android, UWP
3. Power BI

The entire project runs in Azure and is tested there.

#IMPORTANT
High level naming conventions:

Project Prefix . Primary Section . Specific High . Specific Low

BioInfo.App.Library.Analytics  would be a good example of this naming convention.

BioInfo.Web.Api.Services is another good example of this naming convention. 
