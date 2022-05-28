# HTML Notifications Blazor Server POC

This is a proof-of-concept implementing HTML5 push notifications in a Blazor Server application. The application uses [`web-push-csharp`](https://github.com/coryjthompson/WebPushDemo) to facitiliate displaying and pushing notifications to registered clients.

## Getting started
1. Before the app can send push notifications, a public/private key pair must be supplied in `appsettings.json`
2. To generate keys, start the application and navigate to the **Get VAPID keys** page from the menu
3. Copy the generated config section and append to the project's `appsettings.json`
4. Once saved, restart the application and begin subscribing devices in the **Notifications** page
5. Send push notifications in the **Devices List** in the home page.

## Files of interest
* `Notifications.razor` - where the user can grant consent to receive notifications. Behind the scenes, this page is also responsible for registering the service worker (see below)
* `Notifications.razor.js` - a JS file loaded by `Notifications.razor`. Registers the service worker located at the root of the website (`notifications-sw.js`), and communicates registration state to the page through JS interop
* `notifications-sw.js` - placed in the root of the website (`wwwroot`) so that the scope covers the entire app. Listens for push events and displays them accordingly.
* `SubscriptionsController.cs` - the "backend" API that wraps the `web-push-csharp` library, and serves as persistence to track subscribed clients

## TODO:
* Get the SignalR connections to work in a Docker container. Currently there is an issue with the API connections not working inside Docker due to the app unable to resolve its "localhost" equivalent.