Run the app
===========
Please install .net 10 from the interwebs before running these commands :)

Open PowerShell at the solution root and run the following commands:

`PS \USDS Take Home Test> cd EcfrAnalyzer`
`PS \USDS Take Home Test\EcfrAnalyzer> dotnet clean`
`PS \USDS Take Home Test\EcfrAnalyzer> dotnet build`
`PS \USDS Take Home Test\EcfrAnalyzer> dotnet run`

The `dotnet run` command will build and start the application.

App Summary 
===========

So I chose Blazor for this application because it is what I know best and there wan't a ton of time to complete this assignment. I spent roughly 12 hours on it. I know the document suggested 4 hours but there were a ton of API endpoints to consume. I only consumed the first 3 endpoints. I was already at 1200 lines and I was only a third of the way through the API end points. I was under the impression that I had to consume them all but given the constraints there was no way to do that. I added unit testing for the first two services and API endpoints but did not for the third.

There is a ton more code I could write against those endpoints but it wasn't super clear on what the users use case would be? In order to write more code I would need to know how the users were going to use the page. Truthfully, in order to manipulate data like the document suggested we would be using a desktop application and not a web application. Or we would at least use a SQL server and database back end. Based on the instructions it didn't seem that was wanted. Again I would go way past 1200 lines if I added Entity Framework and all the SQL DB classes. 

I also didn't spend a ton of time worrying about code coverage because the instructions didn't clearly state what type of code coverage you were looking for. Code coverage is really important but the quality of the test and what your tests are covering is more important the the raw code coverage number itself. 

If this isn't just a test and you really do need to comb through lots of data like that. I would highly recommend an Open AI chat model connection and an MCP server or something similar. It can make reasoning over large data sets like this much more powerful for the users.