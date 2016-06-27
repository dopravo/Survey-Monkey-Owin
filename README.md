##Welcome to SurveyMonkey Owin
####Getting started
1. Create a new Asp.net Application.
2. In the Startup.Auth, in the App_Start Folder, add the following Code to Authenticate your Web Application Users with their SurveyMonkey Accounts.
3. Add Your Client Id, Client Secret, Callback Path, Api Key in the appsettings of your Web.config file.
4. Use Configration Manger to get the values of those settings.

```xml
  <appSettings>
    <add key="clientId" value="Your Survey Monkey Client Id" />
    <add key="apikey" value="Your Survey Monkey API Key" />
    <add key="clientSecret" value="Your Survey Monkey Client Secret" />
    <add key="callbackPath" value="Your Survey Monkey CallbackPath set in your Survey Monkey app settings" />
  </appSettings>
```

```c#
    app.UseSurveyMonkeyAuthentication(
        clientId: ConfigurationManager.AppSettings["clientId"],
        apiKey: ConfigurationManager.AppSettings["apikey"],
        clientSecret: ConfigurationManager.AppSettings["clientSecret"],
        callbackPath: ConfigurationManager.AppSettings["callbackPath"],
        displayName: "SurveyMonkey",
        domain: "api.SurveyMonkey.net",
        userInfoPath: "/v{0}/users/me",
        apiVersion: 3,
        authorizationPath: "oauth/authorize",
        tokenPath: "oauth/token"
    );
```
in ApplictionUser class add the following properties 
```c#
        public string AccountType { get; set; }
        public string Language { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string AccessToken { get; set; }
        public string Department { get; set; }
        public string DateCreated { get; set; }
```
in the AccountController change the ExternalLoginConfirmation method by changing the user object adding the new attribues to it.
```c#
    ApplicationUser user = new ApplicationUser
    {
        Email = model.Email,
        AccountType = info.ExternalIdentity.Claims.PropertyValueIfExists("account_type"),
        Language = info.ExternalIdentity.Claims.PropertyValueIfExists("language"),
        UserName = info.ExternalIdentity.Claims.PropertyValueIfExists("username"),
        GivenName = info.ExternalIdentity.Claims.PropertyValueIfExists("givenname"),
        Surname = info.ExternalIdentity.Claims.PropertyValueIfExists("surname"),
        AccessToken = info.ExternalIdentity.Claims.PropertyValueIfExists("access_token"),
        DateCreated = info.ExternalIdentity.Claims.PropertyValueIfExists("date_created"),
        Department = info.ExternalIdentity.Claims.PropertyValueIfExists("group")
    };
```        
