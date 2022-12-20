window.app = {

    log: function (message) {
        console.log(message);
    },
    showModal: function (id) {
        $('#' + id).modal('show');
        
    },
    hideModal: function (id) {
        $('#' + id).modal('hide');
    },
    toastSuccess: function (message) {
        
        toastr.success(message);
    },
    toastFailure: function (message) {
        toastr.warning(message);
    },
}