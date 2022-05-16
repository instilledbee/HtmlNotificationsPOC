# HTML Notifications Blazor Server POC

This is a proof-of-concept implementing HTML5 push notifications in a Blazor Server application. The application uses [MajorSoft Blazor Notification Components](https://github.com/majorimi/blazor-components/blob/master/.github/docs/Notifications.md) to facitiliate displaying notifications, and uses **SignalR** to listen for push notifications.

## Files of interest
* `Notification.razor` - where the user can grant consent to receive notifications
* `NotificationsHost.razor` - an invisible, long-running component that is initialized in `Index.razor`. This houses a Signalr `HubConnection` that listens when new push messages are received from the server.
* `SendNotification.razor` - UI for testing push notifications. When a notification is sent, push notifications should display to all clients connected to the Blazor app that have granted consent.
* `NotificationsHub.cs` - the SignalR Hub for sending out push notification payloads. The endpoint is then mapped in `Program.cs` so the app recognizes it as a valid SignalR hub for connections.

## TODO:
* Get the SignalR connections to work in a Docker container. Currently there is an issue with the SignalR connections not working inside Docker due to the app unable to resolve its "localhost" equivalent.
* Implement off-site push notifications. The component library's functionality is limited to the actual displaying of notifications, and does not expose the necessary browser APIs for off-site push notifications. Integrating [Firebase Cloud Messaging](https://firebase.google.com/docs/cloud-messaging/) along with the components should suffice.