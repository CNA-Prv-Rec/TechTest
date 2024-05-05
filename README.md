Notes:

(1) Everything is harder for me because I've recently ditched windows and moved to linux. So instead of using visual studio 2022 on windows I'm using vscode on linux. I don't have my normal ways of working to hand.

(2) You would really like me to have a sql database. I'm on linux so right now I don't have sql server. I used an in memory database instead, though I could probably have spun up a cloud db, not sure that would have helped since I'm not likely to be doing hard coded connection details or checking them into github. And then I wrote lots of linq to sql code, so not sure if this fully works on an in memory EF database. Maybe? It's not fully supported, so there's a todo there if anybody wanted it to put a proper database behind it.

(3) Project currently doesn't compile due to some syntax issues which I can't see and resolve easily in vscode. If I had my normal dev environment I'd fix it in a flash. It can't find my User class and I have a semi colon in the wrong place, nothing major but due to my dev environment I can't see why the compiler doesn't like what I wrote, sorry.

(4) If you can fix these two niggles, then it *ought* to run on http://localhost:7019/ (might be another port for you) and the swagger file will be at https://localhost:7019/swagger/index.html

(5) Since my new funky dev environment doesn't give me the option to right click a class and say create unit tests, creating a unit test project has been a bit of a challenge. I've created a test project and done a basic selenium get but this will not count as a unit test, sorry. I got to the point where the urls were working at one point but didn't get as far as retieving them through selenium. 

(6) Stopped at this point because I need to take my kid birthday-clothes-shopping. 

So generally, fix 2 issues, work out how to create a unit test project in vscode, and get some tests still to do. But I think you can get the general gist of things anyway.

