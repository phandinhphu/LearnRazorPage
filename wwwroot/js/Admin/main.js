document.addEventListener('DOMContentLoaded', function () {
    const confirmDeleteModal = document.getElementById('confirmDeleteModal');
    const formGeneral = document.forms['form-general'];

    const checkboxAll = $('#chose-all');
    const checkboxProductsItems = $('input[name="productsId[]"]');
    const btnAction = $('#btn-action');
    const btnDelete = $('#btn-delete');
    const btnsOperation = $('.btn-operation');
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
        formGeneral.action = `/Admin/Products/Trash/?handler=Destroy&id=${id}`;
        formGeneral.submit();
    });

    // Handle button operation click event
    btnsOperation.click((event) => {
        let action = event.target.getAttribute('data-action');
        let id = event.target.getAttribute('data-id');

        formGeneral.action = `/Admin/Products/Trash/?handler=${action}&id=${id}`;
        formGeneral.submit();
    });

    // Handle checkbox click event
    const rerenderBtnAction = (name) => {
        const checkboxProductsItems = $(`input[name="${name}[]"]:checked`);
        if (checkboxProductsItems.length > 0) {
            btnAction.removeClass('disabled');
        } else {
            btnAction.addClass('disabled');
        }
    }

    checkboxAll.change(() => {
        checkboxProductsItems.prop('checked', checkboxAll.prop('checked'));
        rerenderBtnAction("productsId");
    })

    checkboxProductsItems.change(() => {
        var isCheckAll = checkboxProductsItems.length === checkboxProductsItems.filter(':checked').length;
        checkboxAll.prop('checked', isCheckAll);
        rerenderBtnAction("productsId");
    })

    // Handle sidebar item click event
    const selector = document.querySelector.bind(document);
    const selectorAll = document.querySelectorAll.bind(document);

    selectorAll(".sidebar__item").forEach((item) => {
        item.addEventListener("click", () => {
            selector(".sidebar__item.selected")?.classList.remove("selected");

            item.classList.add("selected");
        });
    });

    selectorAll(".sidebar__item-link").forEach((item) => {
        item.addEventListener("click", () => {
            let arrow = item.querySelector(".icon");
            let subitems = item.nextElementSibling;

            if (arrow.classList.contains("rotate")) {
                arrow.classList.remove("rotate");
                arrow.classList.add("no-rotate");
            } else {
                arrow.classList.add("rotate");
                arrow.classList.remove("no-rotate");
            }

            subitems.classList.toggle("sidebar__sub-items--active");
        });
    });
});
