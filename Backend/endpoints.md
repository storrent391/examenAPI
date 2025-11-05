Documentation
Spotify
﻿

POST
http://localhost:5000/user
http://localhost:5000/user
﻿

Body
raw (json)
json
{
  "name": "TestUser",
  "password": "123456",
  "salt": "1"
}
PUT
http://localhost:5000/user/{7ACA187F-C19A-4454-8E2B-304E2B9503A6}
http://localhost:5000/user/{7ACA187F-C19A-4454-8E2B-304E2B9503A6}
﻿

Body
raw (json)
json
{
  "Name": "UpdatedUser",
  "Password": "newpass123",
  "Salt": "u"
}
POST
http://localhost:5000/profile
http://localhost:5000/profile
﻿

Body
raw (json)
json
{
    "Name":"PerfilUsuari",
    "Description": "Aquesta és una descripció",
    "Status": "1",
    "User_Id": "1f4cfddc-0658-48da-b0d7-34776a5d8b30"
}
PUT
http://localhost:5000/profile
http://localhost:5000/profile/af2686de-4ccf-44d6-b8f4-17afe0a49fff
﻿

Body
raw (json)
json
{
    "Name":"PerfilUsuari",
    "Description": "Aquesta és una descripció",
    "Status": "1"
}
DELETE
http://localhost:5000/profile Copy
http://localhost:5000/profile/af2686de-4ccf-44d6-b8f4-17afe0a49fff
﻿

Body
raw (json)
json
{
    "Name":"PerfilUsuari",
    "Description": "Aquesta és una descripció",
    "Status": "1",
    "User_Id": "1f4cfddc-0658-48da-b0d7-34776a5d8b30"
}
