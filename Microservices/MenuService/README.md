# REST API Example - Jinx Coffeehouse App

### This Project

This project is a .NET 7 REST API utilizing a MongoDB non-relational database. The purpose of the API is to access and manage information for both the mobile app and the website for Jinx Coffeehouse.

### Basic Functionality

This app allows a user to do the following:

1. **GET Request** route **'/'**, which responds a confirmation that the Web API is live and identifies itself along with it's version number.
2. **GET Request** route **'/MenuItems'**, which sends back a full list of menu items.
3. **POST Request** route **'/MenuItems'**, which allows for a JSON body to specify an item type and sends back a list of all menu items tagged with that item type.
4. **PUT Request** route **'/MenuItems'**, which allows for a JSON body to specify an full item and have it inserted into the database
5. **DELETE Request** route **'/MenuItems'**, which allows for a specified item ID to be looked up and removed from the database

#
#
### Notes:

#### Setting up the database

When installing MongoDB, make sure you install MongoDB Compass as well. Once you have the Compass app installed, you can enter it and create a new Database called JinxCoffeehouse with a collection named MenuItems. From there, import the items in the database from JinxCoffeehouse.MenuItems.json, located in the "Database" folder.

Once you have a database, grab the database connection string from the Compass app and put it into appsettings.json in the "Database" > "ConnectionString" value.

#

#### Running the project

If you would like to build and run the Web API, open the terminal and type the following:

```bash
dotnet run
```

If you would like to build and run the Web API with Swagger auto-running in a browser at launch, type the following:

```bash
dotnet watch run
```

#

#### Filtering the Menu:

If you would like to send a POST request to the **'/MenuItems'** route and would like to specify the MenuItemType, you do so by having a JSON body such as this:

```json
{
    "MenuItemType": "DRINK"
}
```

This will return the menu list filtered down to only DRINK items. You can replace DRINK with FOOD or OTHER in the body string to get the other item lists.