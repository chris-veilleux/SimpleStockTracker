# SimpleStockTracker

Simple Stock Tracker is a .NET Core MVC Web Application using C#,
which integrates with a SQL Server database hosted on Azure to perform CRUD operations.

This application is designed to be an easy way to keep track of your stock investment accounts and their holdings.

On the Accounts page, users are shown a list of accounts they have entered. From there, they can create new accounts, view details of existing accounts, edit existing accounts, or delete existing accounts.

![accounts-page](https://user-images.githubusercontent.com/82720132/212564700-e8c25be2-79e4-4353-86b6-b87f50248a76.jpg)

On the Holdings page, users are shown a list of holdings (displayed as Buys and Sells of stock shares) and what account they were purchased in. As with the accounts page, they can then create a new holding, view details of existing holdings, edit existing holdings, or delete existing holdings.

Methods in the Holdings controller are covered by unit tests to ensure proper functionality.

![holdings-page](https://user-images.githubusercontent.com/82720132/212564751-9a1ff04c-45dc-4df8-a7bc-11a8a0ee91a0.jpg)

Note: To create, edit, or delete accounts and holdings, users must be registered and logged in to their account. Accounts can be created either through local registration or by using Google authentication.

### [Click here](https://simplestocktracker.azurewebsites.net/) to view the live site hosted on Azure.

Site style was customized by using a different colour scheme, font, logo, and other minor styling changes. Logo created with https://logomakr.com/app/.
