function scrollToBottom() {
    const block = document.querySelector(".message-block");
    block.scrollTop = block.scrollHeight;
}

function scrollToBottomPhone() {
    const block = document.querySelector(".message-block-phone");
    block.scrollTop = block.scrollHeight;
}

window.blazorModal = {
    show: function (id) {
        var modal = new bootstrap.Modal(document.getElementById(id));
        modal.show();
    }
};