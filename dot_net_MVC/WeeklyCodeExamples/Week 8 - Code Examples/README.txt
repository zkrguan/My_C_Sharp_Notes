### Week 8 code examples  

Use the Visual Studio "Task List", and look for the "TODO" comment tokens.  


**CustomStoreForCarBusiness**

Based on the ideas in the "AssocAddEdit" code example from Week 5. The problem domain is the automotive industry.  

Features:  
- Custom-created data model  
- Security-aware app  
- Claims-based app, using role claims  
- Programmatically creates data objects  

Notes: this code example has no database created yet, so you need do followings before you run the project:
- configure migrations for the project by running "enable-migrations;", ..., in the Package Manager Console
- login the web app with admin@example.com
- load initial data (saving the created objects of Country, Manufactuer, and Vehicle to datastore) by calling 
  conresponding methods of the LoadDataController through url requests on browser: e.g. http://localhost:13155/LoadData/Country 
