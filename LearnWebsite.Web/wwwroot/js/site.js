// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
/**
 * Sets whether "Are you sure ... to leave that site" dialog will be shown to the user when trying to leave
 * @param {boolean} preventLeaving
 */
function setPreventLeavingState(preventLeaving) {
    window.onbeforeunload = preventLeaving ? function () { return ""; } : undefined;
}