$(document).ready(function () {

    //#region Show TempData Notification
    var tempDataMessage = $('#tempDataMessage').val();
    if (tempDataMessage) {
        notify(tempDataMessage, 'info');
        //Remove the value. This seems to help prevent showing the message twice if the user hits the back button
        $('#tempDataMessage').val('');
    }
    //#endregion

});


//#region Notification Helper
/** @description a notification with basic options
 * @param {any} text The text of the notification.
 * @param {any} type The type of the notification. Options are default, success, info, warning, error.
 * @param {any} delay Duration in milliseconds that the notifciation will stay on the screen. Default = 4000
 */
function notify(text, type, delay) {
    if (delay === undefined) {
        delay = 4000;
    }
    Lobibox.notify(type, {
        title: false,
        msg: text,
        size: 'mini',
        sound: false,
        delay: delay,
        icon: false
    });
}

/** @description Generates a notification with advanced options
 * @param {string} text The text of the notification.
 * @param {string} type The type of the notification. Options are default, success, info, warning, error. Default = default
 * @param {bool} showIcon Sets if the icon show be shown for the notification. Default = false
 * @param {string} icon Class of the icon. Defaults to the default icon for the type if blank or null.
 * @param {string} size sets the size of the notification. Options are normal, mini, large. Default = normal
 * @param {number} duration Duration in milliseconds that the notifciation will stay on the screen. Default = 4000
 */
function notifyAdvanced(text, type, showIcon, icon, size, duration) {
    if (duration === undefined) {
        duration = 4000;
    }
    if (type === undefined || type === null || type.length < 1) {
        type = 'default';
    }
    if (size === undefined || size === null || size.length < 1) {
        size = 'normal';
    }

    if (showIcon) {
        if (icon === undefined || icon === null || icon.length < 1) {
            if (type === 'success') {
                icon = 'mdi mdi-check';
            } else if (type === 'error') {
                icon = 'mdi mdi-alert-octagon-outline';
            } else if (type === 'warning') {
                icon = 'mdi mdi-alert-outline';
            } else if (type === 'info') {
                icon = 'mdi mdi-exclamation';
            }
        }
    } else {
        icon = false;
    }

    Lobibox.notify(type, {
        title: false,
        msg: text,
        size: size,
        sound: false,
        delay: duration,
        icon: icon
    });
}