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