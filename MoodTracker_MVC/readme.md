# This WEB API project is deployed to Azure as a service, and Swagger is accessible via the link below.
	# https://moodtracker-webapi-cgfcbxg8hjbcebgg.australiaeast-01.azurewebsites.net/Swagger/index.html

# Technical Details
	# Database
 
 		# Hosted in Azure database
		# Connection details below
			#'Server=tcp:william.database.windows.net,1433;Initial Catalog=MoodTracker;Persist Security Info=False;User 				ID=williamTest;Password=\"NewPassword123\";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;'
   
  		# Database First approach then generate EF Model with Scaffolding using Package manager Console			
			# dotnet ef dbcontext scaffold 'Server=tcp:william.database.windows.net,1433;Initial Catalog=MoodTracker;Persist Security Info=False;User ID=williamTest;Password=\"NewPassword123\";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;' Microsoft.EntityFrameworkCore.SqlServer -o Models --context-dir Data  --context MoodTrackerContext --force

	# Framework Used
		# AutoMapper - I use AutoMapper to map between DTOs (Data Transfer Objects) and entities object.
		# DependencyInjection - i used DI to inject dependencies (such as services, controller, dbContext, AutoMapper and configurations) into classes like controllers or services rather than manually creating instances of them.
		# Swagger - Configures Swagger for API documentation, providing an interactive UI for API testing and exploration.
  
	# Security 
 		# No authentication method 
		# Jwt Bearer Token shall be implemented.
	
	
