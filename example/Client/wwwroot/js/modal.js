var myModal;

export function toggle(element) {
    if (myModal == null) {
        myModal = new bootstrap.Modal(element, {
            keyboard: false
        });
    }

    myModal.toggle();
}