### PROJECT AUTHORS - Completed on 04/18/2016
Lead Developer - Ryan Tunis
Developer - Michael MacDonald
Developer - Casey Timmers

Please note that every person in the above list contributed to every file in one way or another.

### DEPLOYMENT NOTES:
Before deploying, run update-database in the package manager console to create an instance of the database. Otherwise the identity system will not work properly.

### ADMINISTRATOR ACCOUNT:
Username: admin
Password: password

The admin should change their password in their User Settings located underneath their name in the menu immediately after deployment. More admin accounts can be created within the User Manager if need be.

### OTHER USER ACCOUNTS:
There are 3 other accounts that we used for testing the roles which are as follows:

Username: client
Password: password

Username: board
Password: password

Username: family
Password: password

These accounts should be deleted through the User Manager once the web site is deployed. Only the admin account or user created admin accounts have the ability to do this.

### NOTES ABOUT SLIDESHOWS:
Currently there are seed images that are loaded for the slideshows that exist on the Programs page. 
These slide shows can be edited through the Slideshow Manager and should be edited to suit the needs of HPS once this website is deployed.

### EMAILS USED

### Primary Account
Username: hpfs@outlook.com 
Password: c7h2%Kq[_0!>,sT

### Secondary Email for Two Step Verification
Username: hpfsOntario@gmail.com
Password: jU$4`mB\0aE6@fGw2^

### This email is temporary and can be replaced with one of your choosing
Username: hpfsOntario2@gmail.com
Password: jU$4`mB\0aE6@fGw2^

### TWILIO (For phone verification)
Username: hpfs@outlook.com 
Password: c7h2%Kq[_0!>,sT
Number: (289) 271-9932 (Twilio assigns you this number for their usage when sending password resets, this number must not change)

### FITBIT API
In order to correctly link FitBit with this website, an account on the Fitbit Developers website will need to be created. They will provide you with a FitbitConsumerKey and a FitbitConsumerSecret.
Simply replace those values in the Web.config file with the ones provided for you. 
Also, in their website you will need to point their Callback URL to http://www.hpfs.on.ca/Pages/FitbitManager.aspx in order to recieve the verification to access their database.
This only needs to be set up once. Each time a user accesses the Fitbit Manager of the site it will redirect them to the Fitbit login page from the Fitbit site which they will then enter their own user name and password in order to view their data and synchronize it with the HPS database.

### API KEYS - Located in Web.config
# Fitbit
<add key="FitbitConsumerKey" value="d8acdd55f094b791fabc655731711a90" />
<add key="FitbitConsumerSecret" value="5e75bd6b75a17b0b11eb1d132958c469" />
 
# Twilio
<add key="TwilioSID" value="ACacdccf06412fb593068cc8a79eb0a124" />
<add key="TwilioTOKEN" value="35381e03a6b6ded6db543cb3c2aa57b2" />
<add key="TwilioID" value="+12892719932" />

# Contact Form and Password Recovery
<add key="Email" value="hpfs@outlook.com" />
<add key="Password" value="gkriavbcfiaomdqm" />

Note: This is set up to use the Outlook SMTP Server. You may change the email and password to whichever you desire but it has to be from Outlook.com unless you configure your own SMTP Server.