# Books Library
## by Majkel-code

Library is a simple C# application to improve my programming skills.
- run App
- register users
- login
- borrow/deposit books

as admin:
- add new books

## Tech
- DOTNET == 7.0
- Newtonsoft.json == 13.0.3

## Instalation
## CLONE
First of all clone this repositories:
```sh
cd <to directory>
git clone <repo link>
```
to find repository link - click "CODE" button on project site:


![Screenshot 2024-01-27 at 13 52 48](https://github.com/Majkel-code/BooksLibrary_Csharp/assets/13604347/a9eb1bb7-5042-4d14-8488-9f84deca8dcb)
## DOWNLOAD ZIP
Or download as zip pack


![Screenshot 2024-01-27 at 14 10 41](https://github.com/Majkel-code/BooksLibrary_Csharp/assets/13604347/c1995577-2eb5-4cb5-b3b6-8ae6e932c24f)

then unpack .zip file in your destination localization.


Then when you successfuly cloned or unpacked repo:
- start the App
you can simply do it by pressing 'run' button when in Program.cs file (Visual Studio Code)

<img width="239" alt="Screenshot 2024-01-27 at 13 56 10" src="https://github.com/Majkel-code/BooksLibrary_Csharp/assets/13604347/3e1270ae-2ffa-432d-a314-2213ced46761">



- When application starts you will saw screen like this:

<img width="645" alt="Screenshot 2024-01-27 at 13 57 30" src="https://github.com/Majkel-code/BooksLibrary_Csharp/assets/13604347/d2d2718b-e7f4-445b-8dd5-84c07ca6f509">



Now everything is ready to use. 
Application should create database.json file where are staored every information like:
- books - title, authors, number of pages
- users - login, password
- registry - user : book

Now you can register your first user by input 'r' in console and press 'enter'
then input login and password:

<img width="536" alt="Screenshot 2024-01-27 at 14 05 05" src="https://github.com/Majkel-code/BooksLibrary_Csharp/assets/13604347/c9a6d0d1-0549-4e26-ae20-026dbc1130dd">


Like you see, password must be >= 8 char
Then if password is correct and data are no used by other user you will see screen like this:

<img width="509" alt="Screenshot 2024-01-27 at 14 06 07" src="https://github.com/Majkel-code/BooksLibrary_Csharp/assets/13604347/98c70e7e-6ee2-4b8c-abc7-11c391eddecb">


Now you can login to this user or create another one.

Every time on start, application shows hint and display actual stored logins and passwords
just for simplify uses:
<img width="626" alt="Screenshot 2024-01-27 at 14 01 12" src="https://github.com/Majkel-code/BooksLibrary_Csharp/assets/13604347/164e1f5c-5b52-4a98-b2ef-c98c45f75dce">

