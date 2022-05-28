var subscriptionId, subscriptionEndpoint, isSubscribed;

export function RegisterServiceWorker() {
    if (!('PushManager' in window)) {
        console.log('Push notifications are not supported in this browser.');
        return;
    }

    Notification.requestPermission().then(function (status) {
        console.log('Status: ' + status);

        if (status === 'denied') {
            console.log('User denied notification permissions');
            return;
        }
        else if (status === 'granted') {
            if ('serviceWorker' in navigator) {
                navigator.serviceWorker.register('./notifications-sw.js').then(function (registration) {
                    if (registration.installing) {
                        console.log('Service worker installing');
                    } else if (registration.waiting) {
                        console.log('Service worker installed');
                    } else if (registration.active) {
                        console.log('Service worker active');
                    }

                    if (registration.active) {
                        if (!(registration.showNotification)) {
                            console.log('Browser does not support off-site push notifications.');
                        }
                    }
                });
            } else {
                console.log('Browser does not support service workers. Push notifications may not work.');
            }
        }
    });
}

export function Subscribe(applicationServerPublicKey) {
    navigator.serviceWorker.ready.then(function (reg) {
        console.log('Registration: ' + reg.active);
        const subscribeParams = { userVisibleOnly: true };

        //Setting the public key of our VAPID key pair.
        const applicationServerKey = urlB64ToUint8Array(applicationServerPublicKey);
        subscribeParams.applicationServerKey = applicationServerKey;

        reg.pushManager.subscribe(subscribeParams)
            .then(function (subscription) {
                console.log(subscription);

                isSubscribed = true;

                const p256dh = base64Encode(subscription.getKey('p256dh'));
                const auth = base64Encode(subscription.getKey('auth'));

                console.log(subscription);

                console.log(subscription.endpoint);
                console.log(p256dh);
                console.log(auth);
            })
            .catch(function (e) {
                console.log('[subscribe] Unable to subscribe to push', e);
            });
    });
}

function urlB64ToUint8Array(base64String) {
    const padding = '='.repeat((4 - base64String.length % 4) % 4);
    const base64 = (base64String + padding)
        .replace(/\-/g, '+')
        .replace(/_/g, '/');

    const rawData = window.atob(base64);
    const outputArray = new Uint8Array(rawData.length);

    for (let i = 0; i < rawData.length; ++i) {
        outputArray[i] = rawData.charCodeAt(i);
    }
    return outputArray;
}

function base64Encode(arrayBuffer) {
    return btoa(String.fromCharCode.apply(null, new Uint8Array(arrayBuffer)));
}