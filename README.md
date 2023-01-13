# SyncHuduSyncro
A small tool to improve data consistency and accessibility between Syncro and Hudu. 


# Setup - Hudu
Set up the official Syncro integration. 
- Contacts inserted to People
- Devices inserted to Computer Assets

Customize asset layout for Computer Assets
- Add 'Asset Link' field named 'Contact' referencing 'People'

Generate an API key to be used by the tool

# Setup - Syncro
Add a 'Web Link' custom field for Syncro Devices named 'Hudu'

Generate an API key to be used by the tool

# Setup - Tool
Populate the host and API keys in web.config
Compile tool and run to test  
Create a scheduled task to run the tool daily or as frequently as required
