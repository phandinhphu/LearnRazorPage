// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener('DOMContentLoaded', function () {
    const confirmDeleteModal = document.getElementById('confirmDeleteModal');
    const formDelete = document.forms['form-delete'];

    const checkboxAll = $('#chose-all');
    const selectTag = $('#select-action');
    const checkboxItems = $('input[name="productsId[]"]');
    const btnAction = $('#btn-action');
    const btnDelete = $('#btn-delete');
    var id;

    // Handle modal event
    if (confirmDeleteModal) {
        confirmDeleteModal.addEventListener('show.bs.modal', event => {
            // Button that triggered the modal
            const button = event.relatedTarget
            // Extract info from data-bs-* attributes
            id = button.getAttribute('data-bs-id')
        })
    }

    // Handle form delete submit event
    btnDelete.click(() => {
        formDelete.action = `/products/?handler=delete&id=${id}`;
        formDelete.submit();
    });

    // Handle select tag change event
    selectTag.change(() => {
        let action = selectTag.val();

        if (action == 'restore') {
            btnAction.removeClass('disabled');
        } else {
            rerenderBtnAction();
        }
    })

    // Handle checkbox click event
    const rerenderBtnAction = () => {
        const checkedItems = $('input[name="productsId[]"]:checked');
        if (checkedItems.length > 0) {
            btnAction.removeClass('disabled');
        } else {
            btnAction.addClass('disabled');
        }
    }

    checkboxAll.change(() => {
        checkboxItems.prop('checked', checkboxAll.prop('checked'));
        rerenderBtnAction();
    })

    checkboxItems.change(() => {
        var isCheckAll = checkboxItems.length === checkboxItems.filter(':checked').length;
        checkboxAll.prop('checked', isCheckAll);
        rerenderBtnAction();
    })
});