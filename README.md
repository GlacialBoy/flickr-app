# Flickr App Project
## Purpose
Create a web page application that let the user to show and search flickr images.
The application is composed as:
- Create a proxy API in netcore (Flickr public API https://www.flickr.com/services/feeds/)
- Create a page that displays the public feed images in either a list or grid view.
- Optional: The user should be able to enter a keyword in a search box and click on a search button and the app should return images with the relevant tags.

## Features
- Load set of public images from Flickr
- Clicking the images you'll open the specific Flickr page
- Search for Tags (also multiple tags, separated by spaces)

## Run
Using a bash or command type, build the project with `dotnet build` and `dotnet run`.
As a IDE I've used Visual Studio Code, so there is no solution file.

## Structure
ASP.NET Core and WebApi in the backend, React with Bootstrap and Material UI component as a front-end layer

## Data Source
Flickr public apis https://www.flickr.com/services/feeds/docs/photos_public/ 

## Testing
There is another project "flickr-app" that is testing the app. 
It contains test on the parsing component of the apis.
To run the test execute `dotnet test`
