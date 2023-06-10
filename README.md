The solution in consists of three main parts or foldes:

1-Client Side Folder Project with dotnet7 RazorPage

2-APi Folder : Onion architecture projects

3-Shared Models: The ViewModels common on Client Side and Api ,it is taken as a reference on portal and appliction

General info: Iam using Mediator ,AutoMapper,Entity FrameWork Packages Packages on API Project Iam using session on Client Side and Tokens with Roles Claims on API Project To achieve security

you can Login via : UserName: admin Password: admin //admin can add products and see them UserName: testUser Password:testUser //testUser can only see products
