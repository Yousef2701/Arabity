let scroller = document.querySelector(".scroller");
let height = document.documentElement.scrollHeight - document.documentElement.clientHeight;
window.addEventListener("scroll", () => {
    let scrollTop = document.documentElement.scrollTop;
    scroller.style.width = (scrollTop / height) * 100 + "%";
})
document.getElementById("deleteAccount").onclick = () => {
    document.getElementById("delete").click();
}
function CenterCardClick() {
    document.querySelectorAll("#center").forEach(element => {
        element.onclick = () => {
            sessionStorage.setItem("clicked", element.children[1].textContent);
            console.log(element.children[2].children[0]);
            element.children[2].children[0].click;
        }
    });
}
function ProductCardClick() {
    document.querySelectorAll("#product").forEach(element => {
        element.onclick = () => {
            sessionStorage.setItem("clicked-product", element.children[0].textContent);
        }
    });
}
function rate() {
    let arr = document.querySelectorAll("#stars svg");
    document.querySelectorAll("#stars svg").forEach((element,i) => {
        element.onclick = () => {
            for (x = 4; x >= i; x--){
                arr[x].style.color = "gold";
            }
            for (x = 0; x < i; x++){
                arr[x].style.color = "#212529";
            }
    }
    });
}

document.getElementById("confirm").onclick = () => {
    if (confirm("Are You Sure You Want To Confirm This Order?") == false) {
        Event.preventDefault();
    }
    document.getElementById("confirm").nextElementSibling.children[0].click();

}
document.querySelectorAll("#deleteAccount").forEach(element => {
    element.onclick = () => {
        if (confirm("Are You Sure You Want To Delete Your Account?") == false) {
            Event.preventDefault();
        }
        element.nextElementSibling.click();

    }
});
window.onload = () => {
    if (document.body.id == "store-data") {
        console.log(sessionStorage.getItem("clicked"));
        document.getElementById("clicked-id").value = sessionStorage.getItem("clicked");
        document.getElementById("clicked-id").click;
    }
    if (document.body.id == "product-data") {
        console.log(sessionStorage.getItem("clicked-product"));
        document.getElementById("clicked-parcode").value = sessionStorage.getItem("clicked-product");
        document.getElementById("clicked-parcode").click;
    }
}